using Microsoft.AspNetCore.Mvc;
using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;
using ReserveHub.Services;

namespace ReserveHub.Controllers;


[ApiController]
[Route("api/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult CreateBooking([FromBody] BookingCreateInfo createInfo)
    {
        
        bool result = _bookingService.CreateBooking(createInfo);
        return result
            ? Ok(ApiResponse<bool>.Success(null, result))
            : BadRequest(ApiResponse<bool>.Fail(null, result));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBooking([FromQuery] BookingFilter filter)
    {
        var result = _bookingService.GetBookings(filter);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{bookingId}")]
    public IActionResult GetBookingById(int bookingId)
    {
        var booking = _bookingService.GetBookingById(bookingId);
        if (booking == null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{bookingId}")]
    public IActionResult UpdateBooking(int bookingId, [FromBody] BookingUpdateInfo updateInfo)
    {
        bool result = _bookingService.UpdateBooking(updateInfo);
        return result
           ? Ok(ApiResponse<bool>.Success(null, result))
            : NotFound(ApiResponse<bool>.Fail(null, result));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{bookingId}")]
    public IActionResult DeleteBooking(int bookingId)
    {
        bool deleted = _bookingService.DeleteBooking(bookingId);
        return deleted
           ? Ok(ApiResponse<bool>.Success(null, deleted))
            : NotFound(ApiResponse<bool>.Fail(null, deleted));  
    }
}