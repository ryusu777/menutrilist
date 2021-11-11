using System.Threading.Tasks;
using Menutrilist.Contracts.V1.Responses;
using Menutrilist.Domain.FatSecret;
using Menutrilist.Services;
using Microsoft.AspNetCore.Mvc;

namespace Menutrilist.Controllers.V1
{

    [Route("api/v1/food")]
    [ApiController]
    public class FatSecretController : ControllerBase
    {
        private readonly IFatSecretService _fatSecretService;
        public FatSecretController(IFatSecretService fatSecretService)
        {
            _fatSecretService = fatSecretService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodResponse<dynamic>>> GetFood(int id)
        {
            var response = await _fatSecretService.GetFood(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<FoodSearchResponse<dynamic>>> SearchFood(string searchWord, int maxResults, int pageNumber)
        {
            var response = await _fatSecretService.SearchFood(searchWord, maxResults, pageNumber);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}