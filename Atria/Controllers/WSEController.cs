using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("wse")]
public class WSEController : ControllerBase {

    [HttpGet("{wseId}")]
    public WebserviceEntry Get(int wseId) => null!;

    [HttpPost("{wseId}")]
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
