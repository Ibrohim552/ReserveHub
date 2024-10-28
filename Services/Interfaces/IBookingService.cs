using ReserveHub.DTO_s;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services;

public interface IBookingService
{
    PaginationResponse<IEnumerable<BookingReadInfo>> GetBookings(BookingFilter filter);
    BookingReadInfo GetBookingById(int bookingId);
    bool CreateBooking(BookingCreateInfo booking);
    bool UpdateBooking(BookingUpdateInfo booking);
    bool DeleteBooking(int bookingId);
}