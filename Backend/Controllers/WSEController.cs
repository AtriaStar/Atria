using Backend.Authentication;
using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;
//TODO: add wse specific authorization
[ApiController]
[Route("wse")]
public class WSEController : ControllerBase {
    [HttpGet("{wseId:long}")]
    public WebserviceEntry Get([FromServices] AtriaContext _, [FromDatabase] WebserviceEntry wse) => wse;

    [RequiresAuthentication]
    [HttpPost("{wseId:long}")]
    public async Task<IActionResult> EditWse([FromServices] AtriaContext db, WebserviceEntry wse, [FromAuthentication] User user) {
        var existingWse = await db.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wse.Id);
        if (existingWse == null) { return NotFound(); }
        var rights = existingWse.Collaborators.FirstOrDefault(x => x.UserId == user.Id)?.Rights;
        if (rights == null) { return Forbid("User is not a collaborator"); }

        if ((rights & WseRights.EditCollaborators) == 0) {
            return Forbid("Collaborator does not have the right to edit the WSE");
        }

        if (!wse.Collaborators.SequenceEqual(existingWse.Collaborators) && (rights & WseRights.EditCollaborators) == 0) {
            return Forbid("Collaborator does not have the right to edit collaborators");
        }

        if (wse.Collaborators.FirstOrDefault(x => x.UserId == user.Id)?.Rights != rights) {
            return BadRequest("You cannot modify your own rights");
        }

        if (wse.CreatedAt != existingWse.CreatedAt) {
            return BadRequest("The creation timestamp cannot be modified");
        }

        db.WebserviceEntries.Update(wse);
        await db.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("{wseId:long}/review/{reviewId:long}")]
    public async Task<IActionResult> EditReview([FromServices] AtriaContext db, long wseId, Review review, [FromAuthentication] User user) {
        var existingReview = await db.Reviews.FirstOrDefaultAsync(x => x.Id == review.Id && x.WseId == review.WseId);
        if (existingReview == null) { return NotFound(); }

        if (existingReview.Creator.Id != user.Id) {
            return Forbid("Only the creator of a review can edit it");
        }

        if (review.Creator.Id != user.Id) {
            return BadRequest("The creator of a review cannot be changed");
        }

        if (existingReview.CreationTime != review.CreationTime) {
            return BadRequest("The creation timestamp cannot be modified");
        }

        db.Reviews.Update(review);
        await db.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpPut("")]
    public async Task<IActionResult> CreateWse([FromServices] AtriaContext db, WebserviceEntry wse, [FromAuthentication] User user) {
        wse.Id = 0;
        wse.CreatedAt = DateTimeOffset.UtcNow;
        wse.Collaborators = new List<Collaborator> { new() { User = user, Rights = WseRights.Owner } };
        await db.WebserviceEntries.AddAsync(wse);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateWse), new { wseId = wse.Id }, wse);
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/question")]
    public async Task<IActionResult> CreateQuestion([FromServices] AtriaContext db, long wseId, Question question,
        [FromAuthentication] User user) {
        question.Id = 0;
        question.WseId = wseId;
        question.CreationTime = DateTimeOffset.UtcNow;
        question.Creator = user;
        await db.Questions.AddAsync(question);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateQuestion), new { wseId, questionId = question.Id }, question);
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/question/{questionId:long}/answer")]
    public async Task<IActionResult> CreateAnswer([FromServices] AtriaContext db, long wseId, long questionId, Answer answer,
        [FromAuthentication] User user) {
        answer.Id = 0;
        answer.WseId = wseId;
        answer.QuestionId = questionId;
        answer.CreationTime = DateTimeOffset.UtcNow;
        answer.Creator = user;
        await db.Answers.AddAsync(answer);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateAnswer), new { wseId, questionId, answerId = answer.Id }, answer);
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/review")]
    public async Task<IActionResult> CreateReview([FromServices] AtriaContext db, long wseId, Review review, [FromAuthentication] User user) {
        review.Id = 0;
        review.WseId = wseId;
        review.CreationTime = DateTimeOffset.UtcNow;
        review.Creator = user;
        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateReview), new { wseId, reviewId = review.Id }, review);
    }

    [RequiresAuthentication]
    [RequiresWseRights(WseRights.DeleteWse)]
    [HttpDelete("{wseId}")]
    public async Task<IActionResult> DeleteWse([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse) {
        db.WebserviceEntries.Remove(wse);
        await db.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}")]
    public async Task<IActionResult> DeleteQuestion([FromServices] AtriaContext db, long wseId,
        long questionId, [FromAuthentication] User user) {
        var question = await db.Questions.FirstOrDefaultAsync(x => x.WseId == wseId && x.Id == questionId);
        if (question == null) {
            return NotFound();
        }

        if (question.Creator != user) {
            return Forbid("Only the creator can delete a question");
        }

        db.Questions.Remove(question);
        await db.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}/answer/{answerId:long}")]
    public async Task<IActionResult> DeleteAnswer([FromServices] AtriaContext db, long wseId, long questionId,
        long answerId, [FromAuthentication] User user) {
        var answer = await db.Answers.FirstOrDefaultAsync(x => x.WseId == wseId && x.QuestionId == questionId && x.Id == answerId);
        if (answer == null) {
            return NotFound();
        }

        if (answer.Creator != user) {
            return Forbid("Only the creator can delete an answer");
        }

        db.Answers.Remove(answer);
        await db.SaveChangesAsync();

        return Ok();
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/review/{reviewId:long}")]
    public async Task<IActionResult> DeleteReview([FromServices] AtriaContext db, long wseId, long reviewId, [FromAuthentication] User user) {
        var review = await db.Reviews.FirstOrDefaultAsync(x => x.WseId == wseId && x.Id == reviewId);
        if (review == null) {
            return NotFound();
        }

        if (review.Creator != user) {
            return Forbid("Only the creator can delete a review");
        }

        db.Reviews.Remove(review);
        await db.SaveChangesAsync();

        return Ok();
    }
}
