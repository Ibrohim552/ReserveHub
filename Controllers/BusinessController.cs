using Microsoft.AspNetCore.Mvc;
using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;
using ReserveHub.Services;

namespace ReserveHub.Controllers
{
    [ApiController]
    [Route("api/owner-business")]
    public class BusinessOwnerController : ControllerBase
    {
        private readonly IBusinessOwnerService _businessOwnerService;

        public BusinessOwnerController(IBusinessOwnerService businessOwnerService)
        {
            _businessOwnerService = businessOwnerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBusinessOwners([FromQuery] BusinessOwnerFilter filter)
        {
            var result = _businessOwnerService.GetBusinessOwners(filter);
            return Ok(result);
        }

        [HttpGet("{businessOwnerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBusinessOwnerById(int businessOwnerId)
        {
            var businessOwner = _businessOwnerService.GetBusinessOwnerById(businessOwnerId);
            if (businessOwner == null)
                return NotFound();

            return Ok(businessOwner);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateBusinessOwner([FromBody] BusinessOwnerCreateInfo businessOwner)
        {
            bool result = _businessOwnerService.CreateBusinessOwner(businessOwner);
            return result
                ? Ok(ApiResponse<bool>.Success(null, result))
                : BadRequest(ApiResponse<bool>.Fail(null, result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBusinessOwner([FromBody] BusinessOwnerUpdateInfo businessOwner)
        {
            bool result = _businessOwnerService.UpdateBusinessOwner(businessOwner);
            return result
                ? Ok(ApiResponse<bool>.Success(null, result))
                : NotFound(ApiResponse<bool>.Fail(null, result));
        }

        [HttpDelete("{businessOwnerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBusinessOwner(int businessOwnerId)
        {
            bool deleted = _businessOwnerService.DeleteBusinessOwner(businessOwnerId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
