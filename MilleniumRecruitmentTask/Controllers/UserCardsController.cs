using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilleniumRecruitmentTask.Model.Exceptions;
using MilleniumRecruitmentTask.Services.Abstract;
using MilleniumRecruitmentTask.Services.Concrete;

namespace MilleniumRecruitmentTask.Api.Controllers
{
    [Route("api/users/{userId}/cards/{cardNumber}")]
    [ApiController]
    public class UserCardsController(ICardService cardService) : ControllerBase
    {

  
        [HttpGet("actions")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllowedActions(
            [FromRoute] string userId,
            [FromRoute] string cardNumber,
            [FromHeader(Name = "X-API-KEY")] string apiKey)
        {
            var actions = await cardService.GetAllowedActions(userId, cardNumber);
            return Ok(actions);
        }
    }
}

