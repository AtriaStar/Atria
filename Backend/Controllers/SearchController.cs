﻿using Backend.AspPlugins;
using Backend.Authentication;
using Backend.ParameterHelpers;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("search")]
public class SearchController : AtriaControllerBase {
    private readonly AtriaContext _context;
    private readonly BackendSettings _options;
    private readonly FuzzingService _fuzzer;

    public SearchController(AtriaContext context, BackendSettings opt, FuzzingService fuzzer) {
        _context = context;
        _options = opt;
        _fuzzer = fuzzer;
    }

    private IEnumerable<WebserviceEntry> GetBaseWse(WseSearchParameters parameters, User? user) {
        IQueryable<WebserviceEntry> query = _context.WebserviceEntries
            .Include(x => x.ApiCheckHistory);

        if ((int)parameters.MinReviewAvg > 1) {
            query = query.Include(x => x.Reviews)
                .Where(x => x.Reviews.Any() && x.Reviews.Average(y => (int)y.StarCount) >= (int)parameters.MinReviewAvg);
        }

        if (parameters.Tags != null) {
            query = query.Where(x => x.Tags.Count(y => parameters.Tags.Contains(y.Name)) == parameters.Tags.Length);
        }

        if (parameters.HasBookmark != null && user != null) {
            query = query.Where(x => user.Bookmarks.Contains(x) == parameters.HasBookmark);
        }

        var enumerable = query.AsEnumerable();

        if (parameters.IsOnline != null) {
            enumerable = enumerable.Where(x =>
                (int?) x.ApiCheckHistory.MaxBy(y => y.CheckedAt)?.Status is { } status &&
                 ((status / 100 == 2) == parameters.IsOnline));
        }

        var scoredEnum = string.IsNullOrEmpty(parameters.Query)
            ? enumerable.Select(x => (wse: x, score: 1.0))
            : enumerable.Select(x => (wse: x, score: _fuzzer.CalculateScore(parameters.Query, x)));
        
        return scoredEnum
            .Where(x => x.score >= _options.MinimumWseScore)
            .Select(x => x with {score = x.score * parameters.Order.GetMapper().Invoke(x.wse)})
            .OrderBy(x => parameters.Ascending ? x.score : -x.score)
            .Select(x => x.wse);
    }

    [HttpGet("wse")]
    public IEnumerable<WebserviceEntry> GetWseList([FromQuery] WseSearchParameters parameters, [FromQuery] Pagination pagination,
        [FromAuthentication, Include(nameof(Models.User.Bookmarks))] User? user)
        => GetBaseWse(parameters, user).Paginate(pagination);

    [HttpGet("wse/count")]
    public long GetWseCount([FromQuery] WseSearchParameters parameters, [FromAuthentication, Include(nameof(Models.User.Bookmarks))] User? user)
        => GetBaseWse(parameters, user).LongCount();

    private IEnumerable<User> GetBaseUsers(string query)
        => _context.Users
            .AsEnumerable()
            .Select(x => (user: x, score: _fuzzer.CalculateScore(query, x)))
            .OrderByDescending(x => x.score)
            .TakeWhile(x => x.score >= _options.MinimumUserScore)
            .Select(x => x.user);

    [HttpGet("user")]
    public IActionResult GetUserList(string query, [FromQuery] Pagination pagination)
        => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).Paginate(pagination));

    [HttpGet("user/count")]
    public IActionResult GetUserCount(string query) => string.IsNullOrEmpty(query) ? BadRequest() : Ok(GetBaseUsers(query).LongCount());
}
