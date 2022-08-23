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
    public async Task<IActionResult> Get([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse) {
        return Ok(wse);
    }

    [RequiresAuthentication]
    [HttpPost("{wseId:long}")]
    public async Task<IActionResult> EditWse([FromServices] AtriaContext db, WebserviceEntry wse) {
        var existingWse = await db.WebserviceEntries.FirstOrDefaultAsync(x => x.Id == wse.Id);

        if (existingWse == null) return NotFound();
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

        await db.SaveChangesAsync();

        return NoContent();
    }

    [RequiresAuthentication]
    [HttpPost("{wseId:long}/review/{reviewId:long}")]
    public async Task<IActionResult> EditReview([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse,
        Review review) {
        var existingReview = wse.Reviews.FirstOrDefault(x => x.Id == review.Id);

        if (existingReview == null) return NotFound();

        existingReview.CreationTime = review.CreationTime;
        existingReview.Creator = review.Creator;
        existingReview.Description = review.Description;
        existingReview.StarCount = review.StarCount;
        existingReview.Title = review.Title;

        await db.SaveChangesAsync();

        return NoContent();
    }

    [RequiresAuthentication]
    [HttpPut("")]
    public async Task<IActionResult> CreateWse([FromServices] AtriaContext db, WebserviceEntry wse) {
        if (ModelState.IsValid) {
            await db.WebserviceEntries.AddAsync(wse);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { wseId = wse.Id }, wse);
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/question")]
    public async Task<IActionResult> CreateQuestion([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse,
        Question question) {

        if (ModelState.IsValid) {
            wse.Questions.Add(question);
            await db.SaveChangesAsync();
            //Todo: created at action?
            return NoContent();
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/question/{questionId:long}/answer")]
    public async Task<IActionResult> CreateAnswer([FromServices] AtriaContext db, [FromDatabase] Question question,
        Answer answer) {
        if (ModelState.IsValid) {
            question.Answers.Add(answer);
            await db.SaveChangesAsync();
            //Todo: created at action?
            return NoContent();
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [RequiresAuthentication]
    [HttpPut("{wseId:long}/review")]
    public async Task<IActionResult> CreateReview([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse,
        Review review) {
        if (ModelState.IsValid) {
            wse.Reviews.Add(review);
            //Todo: created at action?
            return NoContent();
        }

        return new JsonResult("Something went wrong") { StatusCode = 500 };
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId}")]
    public async Task<IActionResult> DeleteWse([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse) {
        db.WebserviceEntries.Remove(wse);
        await db.SaveChangesAsync();
        return Ok(wse);
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}")]
    public async Task<IActionResult> DeleteQuestion([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse,
        [FromDatabase] Question question) {
        wse.Questions.Remove(question);
        await db.SaveChangesAsync();

        return Ok(question);
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/question/{questionId:long}/answer/{answerId:long}")]
    public async Task<IActionResult> DeleteAnswer([FromServices] AtriaContext db, [FromDatabase] Question question,
        [FromDatabase] Answer answer) {
        question.Answers.Remove(answer);
        await db.SaveChangesAsync();

        return Ok(answer);
    }

    [RequiresAuthentication]
    [HttpDelete("{wseId:long}/review/{reviewId:long}")]
    public async Task<IActionResult> DeleteReview([FromServices] AtriaContext db, [FromDatabase] WebserviceEntry wse,
        [FromDatabase] Review review) {
        wse.Reviews.Remove(review);
        await db.SaveChangesAsync();
        
        return Ok(review);
    }
}