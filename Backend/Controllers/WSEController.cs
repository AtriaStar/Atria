using Backend.AspPlugins;
using Backend.Authentication;
using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace Backend.Controllers;

[ApiController]
[Route("wse")]
public class WseController : AtriaControllerBase {
    private readonly AtriaContext _context;

    public WseController(AtriaContext context) {
        _context = context;
    }

    [HttpGet("{wseId:long}")]
    public async Task<WebserviceEntry> Get([FromDatabase, Include(nameof(WebserviceEntry.Tags))] WebserviceEntry wse) {
        wse.ViewCount++;
        _context.Update(wse);
        await _context.SaveChangesAsync();
        return wse;
    }

    [HttpGet("{wseId:long}/collaborators")]
    public IEnumerable<Collaborator> GetCollaborators([FromDatabase, Include(nameof(WebserviceEntry.Collaborators))] WebserviceEntry wse)
        => wse.Collaborators;

    [HttpGet("{wseId:long}/checks")]
    public async Task<IEnumerable<ApiCheck>> GetChecks(long wseId)
        => (await _context.WebserviceEntries
            .Include(x => x.ApiCheckHistory)
            .FirstAsync(x => x.Id == wseId))
            .ApiCheckHistory;

    [HttpGet("{wseId:long}/checks/latest")]
    public async Task<bool?> GetLastCheck(long wseId)
        => (int?)(await _context.WebserviceEntries
            .Include(x => x.ApiCheckHistory)
            .FirstOrDefaultAsync(x => x.Id == wseId))?.ApiCheckHistory
            .MaxBy(x => x.CheckedAt)
            ?.Status is { } status ? status / 100 == 2 : null;

    [HttpGet("{wseId:long}/question")]
    public IEnumerable<Question> GetQuestions(long wseId, [FromQuery] Pagination pagination)
        => _context.Questions.Where(x => x.WseId == wseId).Paginate(pagination);

    [HttpGet("{wseId:long}/question/count")]
    public long GetQuestionCount(long wseId) 
        => _context.Questions.Where(x => x.WseId == wseId).LongCount();

    [HttpGet("{wseId:long}/question/{questionId:long}")]
    public IQueryable<Answer> GetAnswers(long wseId, long questionId, [FromQuery] Pagination pagination)
        => _context.Answers.Where(x => x.WseId == wseId && x.QuestionId == questionId)
            .Paginate(pagination);

    [HttpGet("{wseId:long}/question/{questionId:long}/count")]
    public long GetAnswerCount(long wseId, long questionId) 
        => _context.Answers.Where(x => x.WseId == wseId && x.QuestionId == questionId).LongCount();

    [HttpGet("{wseId:long}/review")]
    public IEnumerable<Review> GetReviews(long wseId, [FromQuery] Pagination pagination)
        => _context.Reviews.Where(x => x.WseId == wseId).Paginate(pagination);

    [HttpGet("{wseId:long}/review/count")]
    public long GetReviewCount(long wseId)
        => _context.Reviews.Where(x => x.WseId == wseId).LongCount();

    [HttpGet("{wseId:long}/review/average")]
    public double GetReviewAverage(long wseId) {
        var list = _context.Reviews.Where(x => x.WseId == wseId);
        if (list.Any()) {
            return list.Average(review => (int)review.StarCount);
        }
        return 0;
    }

    [HttpGet("{wseId:long}/review/{reviewId:long}")]
    public Review GetReview(long wseId, long reviewId)
        => _context.Reviews.Single(x => x.WseId == wseId && x.Id == reviewId);

    [RequiresAuthentication]
    [HttpPost]
    public async Task<IActionResult> EditWse(WebserviceEntry wse, [FromAuthentication] User user) {
        var existingWse = await _context.WebserviceEntries
            .Include(x => x.Collaborators)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == wse.Id);
        if (existingWse == null) { return NotFound(); }
        var rights = existingWse.Collaborators.FirstOrDefault(x => x.UserId == user.Id)?.Rights;
        if (rights == null) { return Forbidden("User is not a collaborator"); }

        if ((rights & WseRights.EditData) == 0) {
            return Forbidden("Collaborator does not have the right to edit the WSE");
        }

        if (wse.CreatedAt != existingWse.CreatedAt) {
            return BadRequest("Creation timestamp cannot be modified");
        }

        if (wse.ViewCount != existingWse.ViewCount) {
            return BadRequest("View count cannot be modified");
        }

        wse.Collaborators = existingWse.Collaborators;
        wse.ApiCheckHistory = existingWse.ApiCheckHistory;
        wse.Questions = existingWse.Questions;
        wse.Reviews = existingWse.Reviews;
        wse.Tags = (await Task.WhenAll(wse.Tags.Select(async x => await _context.Tags.FindAsync(x.Name) ?? x))).ToHashSet();

        _context.WebserviceEntries.Update(wse);

        // Ugh, the alternative would be an actual entity to link WSE to tags that cascades deletion, might be worth considering.
        foreach (var tag in _context.Tags.Include(x => x.WebserviceEntries)
                     .Where(x => x.WebserviceEntries.Contains(wse) && !wse.Tags.Contains(x))) {
            tag.WebserviceEntries.Remove(wse);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [RequiresWseRights(WseRights.EditCollaborators)]
    [HttpPost("{wseId:long}/collaborators")]
    public async Task<IActionResult> EditCollaborators([FromDatabase, Include(nameof(WebserviceEntry.Collaborators))] WebserviceEntry wse,
        CollaboratorDto[] collaborators, [FromAuthentication] User user) {
        if (collaborators.Length == 0) {
            return BadRequest("Collaborator list cannot be empty");
        }

        wse.Collaborators = collaborators.Select(x => new Collaborator {
            WseId = wse.Id,
            UserId = x.UserId,
            Rights = x.Rights,
        }).ToList();

        _context.Update(wse);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("{wseId:long}/leave")]
    public async Task Leave([FromDatabase, Include(nameof(WebserviceEntry.Collaborators))] WebserviceEntry wse,
        [FromAuthentication] User user) {
        wse.Collaborators.Remove(wse.Collaborators.First(x => x.UserId == user.Id));
        _context.Update(wse);
        await _context.SaveChangesAsync();
    }

    [RequiresAuthentication]
    [HttpPost("review")]
    public async Task<IActionResult> EditReview(Review review, [FromAuthentication] User user) {
        var existingReview = await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == review.Id && x.WseId == review.WseId);
        if (existingReview == null) { return NotFound(); }

        if (existingReview.CreatorId != user.Id) {
            return Forbidden("Only the creator of a review can edit it");
        }

        if (review.CreatorId != user.Id) {
            return BadRequest("The creator of a review cannot be changed");
        }

        if (existingReview.CreationTime != review.CreationTime) {
            return BadRequest("The creation timestamp cannot be modified");
        }

        review.Wse = existingReview.Wse;

        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpPut]
    public async Task<IActionResult> CreateWse(WebserviceEntry wse, [FromAuthentication] User user) {
        wse.Id = 0;
        wse.CreatedAt = DateTimeOffset.UtcNow;
        wse.Collaborators = new List<Collaborator> { new() { User = user, Rights = WseRights.Owner } };
        wse.Tags = (await Task.WhenAll(wse.Tags.Select(async x => await _context.Tags.FindAsync(x.Name) ?? x))).ToHashSet();
        await _context.WebserviceEntries.AddAsync(wse);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateWse), new { wseId = wse.Id }, wse);
    }

    [RequiresAuthentication]
    [HttpPut("question")]
    public async Task<IActionResult> CreateQuestion(Question question,
        [FromAuthentication] User user) {
        question.Id = 0;
        question.CreationTime = DateTimeOffset.UtcNow;
        question.Creator = user;
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateQuestion), new { wseId = question.WseId, questionId = question.Id }, question);
    }

    [RequiresAuthentication]
    [HttpPut("answer")]
    public async Task<IActionResult> CreateAnswer(Answer answer,
        [FromAuthentication] User user) {
        answer.Id = 0;
        answer.CreationTime = DateTimeOffset.UtcNow;
        answer.Creator = user;
        await _context.Answers.AddAsync(answer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateAnswer), new { wseId = answer.WseId, questionId = answer.QuestionId, answerId = answer.Id }, answer);
    }

    [RequiresAuthentication]
    [HttpPut("review")]
    public async Task<IActionResult> CreateReview(Review review, [FromAuthentication] User user) {
        review.Id = 0;
        review.CreationTime = DateTimeOffset.UtcNow;
        review.Creator = user;
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateReview), new { wseId = review.WseId, reviewId = review.Id }, review);
    }

    [RequiresAuthentication]
    [RequiresWseRights(WseRights.DeleteWse)]
    [HttpDelete("{wseId}")]
    public async Task<IActionResult> DeleteWse([FromDatabase, Include(nameof(WebserviceEntry.Collaborators)), Include(nameof(WebserviceEntry.ApiCheckHistory))]
        WebserviceEntry wse, [FromAuthentication] User _) {
        _context.RemoveRange(wse.ApiCheckHistory);
        _context.WebserviceEntries.Remove(wse);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}")]
    public async Task<IActionResult> DeleteQuestion(long wseId, long questionId, [FromAuthentication] User user) {
        var question = await _context.Questions.FindAsync(wseId, questionId);
        if (question == null) {
            return NotFound();
        }

        if (question.Creator != user) {
            return Forbidden("Only the creator can delete a question");
        }

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}/answer/{answerId:long}")]
    public async Task<IActionResult> DeleteAnswer(long wseId, long questionId,
        long answerId, [FromAuthentication] User user) {
        var answer = await _context.Answers.FindAsync(wseId, questionId, answerId);
        if (answer == null) {
            return NotFound();
        }

        if (answer.Creator != user) {
            return Forbidden("Only the creator can delete an answer");
        }

        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/review/{reviewId:long}")]
    public async Task<IActionResult> DeleteReview(long wseId, long reviewId, [FromAuthentication] User user) {
        var review = await _context.Reviews.FindAsync(wseId, reviewId);
        if (review == null) {
            return NotFound();
        }

        if (review.Creator != user) {
            return Forbidden("Only the creator can delete a review");
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
