using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Frontend.Pages.User;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Runtime.Intrinsics.X86;

namespace Backend.Controllers;

[ApiController]
[Route("wse")]
public class WSEController : ControllerBase {
    private readonly AtriaContext _context;

    public WSEController(AtriaContext context) {
        _context = context;
    }


    [HttpGet("{wseId:long}")]
    public WebserviceEntry Get(
        // TODO: Using Include should NOT be necessary here, yet it is apparently for stupid validation reasons..
        [FromDatabase, Include(nameof(WebserviceEntry.ContactPerson))] WebserviceEntry wse) {
        return wse;
    }

    [HttpPost("{wseId:long}")]
    public void EditWse(WebserviceEntry wse) { }


    [HttpPost("{wseId}")]
    public async Task<IActionResult> EditWse(long wseId, WebserviceEntry wse) {
        if (wseId != wse.Id) {
            return BadRequest();
        }

        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return NotFound();
        }
        existingWse.Changelog = wse.Changelog;
        existingWse.Collaborators = wse.Collaborators;
        existingWse.ContactPerson = wse.ContactPerson;
        existingWse.CreatedAt = wse.CreatedAt;
        existingWse.DocumentationLink = wse.DocumentationLink;
        existingWse.FullDescription = wse.FullDescription;
        existingWse.Link = wse.Link;
        existingWse.Name = wse.Name;
        existingWse.Reviews = wse.Reviews;
        existingWse.Questions = wse.Questions;
        existingWse.ShortDescription = wse.ShortDescription;
        existingWse.Tags = wse.Tags;
        existingWse.ViewCount = wse.ViewCount;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPost("{wseId}/review/{reviewId}")]
    public async Task<IActionResult> EditReview(long wseId, long reviewID, Review review) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return NotFound();
        }

        var existingReview = existingWse.Reviews.FirstOrDefault(x => x.Id == reviewID);

        if (existingReview == null) {
            return NotFound();
        }

        existingReview.CreationTime = review.CreationTime;
        existingReview.Creator = review.Creator;
        existingReview.Description = review.Description;
        existingReview.StarCount = review.StarCount;
        existingReview.Title = review.Title;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("")]
    public async Task<IActionResult> CreateWse(WebserviceEntry wse) {
        if(ModelState.IsValid)
        {
            await _context.WebserviceEntries.AddAsync(wse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(WSEController.Get), new { wseId = wse.Id }, wse);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{wseId}/question")]
    public async Task<IActionResult> CreateQuestion(long wseId, Question question) {

        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null) {
            return BadRequest();
        }

        if (ModelState.IsValid) {

            existingWse.Questions.Add(question);
            await _context.WebserviceEntries.AddAsync(existingWse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }


    [HttpPut("{wseId}/question/{questionId}/answer")]
    public async Task<IActionResult> CreateAnswer(long wseId, long questionId, Answer answer) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        var existingQuestion = existingWse.Questions.FirstOrDefault(x => x.Id == questionId);

        if (existingQuestion == null) {
            return BadRequest();
        }

        if (ModelState.IsValid) {
            
            existingWse.Questions.FirstOrDefault(x => x.Id == questionId).Answers.Add(answer);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpPut("{wseId}/review")]
    public async Task<IActionResult> CreateReview(long wseId, Review review) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            existingWse.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [HttpDelete("{wseId}")]
    public async Task<IActionResult> DeleteWse(long wseId) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        _context.WebserviceEntries.Remove(existingWse);
        await _context.SaveChangesAsync();

        return Ok(existingWse);
    }

    [HttpDelete("{wseId}/question/{questionId}")]
    public async Task<IActionResult> DeleteQuestion(long wseId, long questionId) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        var existingQuestion = existingWse.Questions.FirstOrDefault(x => x.Id == questionId);

        if (existingQuestion == null) 
        {
            return BadRequest();
        }

        existingWse.Questions.Remove(existingQuestion);
        await _context.SaveChangesAsync();

        return Ok(existingQuestion);
    }

    [HttpDelete("{wseId}/question/{questionId}/answer/{answerId}")]
    public async Task<IActionResult> DeleteAnswer(long wseId, long questionId, long answerId) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        var existingQuestion = existingWse.Questions.FirstOrDefault(x => x.Id == questionId);

        if (existingQuestion == null)
        {
            return BadRequest();
        }

        var existingAnswer = existingQuestion.Answers.FirstOrDefault(x => x.Id == answerId);

        if (existingAnswer == null) {
            return BadRequest();
        }

        existingQuestion.Answers.Remove(existingAnswer);
        await _context.SaveChangesAsync();

        return Ok(existingAnswer);
    }

    [HttpDelete("{wseId}/review/{reviewId}")]
    public async Task<IActionResult> DeleteReview(long wseId, long reviewId) {
        var existingWse = await _context.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wseId);

        if (existingWse == null)
        {
            return BadRequest();
        }

        var existingReview = existingWse.Questions.FirstOrDefault(x => x.Id == reviewId);

        if (existingReview == null)
        {
            return BadRequest();
        }

        existingWse.Questions.Remove(existingReview);
        await _context.SaveChangesAsync();

        return Ok(existingReview);
    }
}
