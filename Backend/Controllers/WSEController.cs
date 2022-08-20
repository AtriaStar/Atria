using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("wse")]
public class WSEController : ControllerBase {

    [HttpGet("{wseId:long}")]
    public WebserviceEntry Get(
        // TODO: Using Include should NOT be necessary here, yet it is apparently for stupid validation reasons..
        [FromDatabase, Include(nameof(WebserviceEntry.ContactPerson))] WebserviceEntry wse) {
        return wse;
    }

    [HttpPost("{wseId:long}")]
    public void EditWse(WebserviceEntry wse) { }

    [HttpPost("{wseId}/review/{reviewId}")]
    public void EditReview(Review review) { }

    [HttpPut("")]
    public int CreateWse(WebserviceEntry wse) => 0;

    [HttpPut("{wseId}/question")]
    public int CreateQuestion(Question question) => 0;

    [HttpPut("{wseId}/answer")]
    public int CreateAnswer(Answer answer) => 0;

    [HttpPut("{wseId}/review")]
    public int CreateReview(Review review) => 0;

    [HttpDelete("{wseId}")]
    public void DeleteWse(int wseId) { }

    [HttpDelete("{wseId}/question/{questionId}")]
    public void DeleteQuestion(int questionId) { }

    [HttpDelete("{wseId}/answer/{answerId}")]
    public void DeleteAnswer(int answerId) { }

    [HttpDelete("{wseId}/review/{reviewId}")]
    public void DeleteReview(int reviewId) { }

}
