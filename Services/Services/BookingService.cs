using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReserveHub.DTO_s;
using ReserveHub.Entities;
using ReserveHub.Filters;
using ReserveHub.Responces;

namespace ReserveHub.Services;

public class BookingService:IBookingService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public BookingService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public PaginationResponse<IEnumerable<BookingReadInfo>> GetBookings(BookingFilter filter)
    {
        IQueryable<Booking> bookings = _context.Booking.Include(b=>b.BusinessOwner);
        if (filter.ClientName!=null)
            bookings = bookings.Where(x => x.Client.Name.Contains(filter.ClientName));            
        if (filter.BookingDate!=null)
            bookings = bookings.Where(b => b.BookingDate == filter.BookingDate);
        if (filter.ClientSurname!=null)
            bookings = bookings.Where(x => x.Client.Surname.Contains(filter.ClientSurname));
        int totalRecords = bookings.Count();
        IQueryable<BookingReadInfo> result = bookings
           .Skip((filter.PageNumber - 1) * filter.PageSize)
           .Take(filter.PageSize)
           .ProjectTo<BookingReadInfo>(_mapper.ConfigurationProvider);
        return PaginationResponse<IEnumerable<BookingReadInfo>>.Create(filter.PageNumber,filter.PageSize,
            totalRecords, result);
    }

    public BookingReadInfo GetBookingById(int bookingId)
    {
        return _context.Booking
           .Where(b => b.Id == bookingId &&!b.IsDeleted)
           .ProjectTo<BookingReadInfo>(_mapper.ConfigurationProvider)
           .FirstOrDefault();
    }

    public bool CreateBooking(BookingCreateInfo booking)
    {
        var dayStart = booking.BookingDate;
        var dayEnd = booking.BookingDate.AddDays(1);
        int MaxBookingsPerDay = 5;

        var dailyBookings = _context.Booking
            .Where(b => b.BusinessOwnerId == booking.BusinessOwnerId
                        && b.BookingDate >= dayStart
                        && b.BookingDate < dayEnd)
            .OrderBy(b => b.BookingDate);
        if (dailyBookings.Count() >= MaxBookingsPerDay)
        {
            return false;
        }
        DateTime? lastBookingEndTime = dailyBookings.LastOrDefault()?.BookingDate.AddHours(1); // Добавим 1 час на примерную длительность приема
        if (lastBookingEndTime.HasValue && booking.BookingDate < lastBookingEndTime.Value)
        {
            return false; 
        }
        var newBooking = _mapper.Map<Booking>(booking);
        _context.Booking.Add(newBooking);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateBooking(BookingUpdateInfo booking)
    {
        var existingBook=_context.Booking.
            FirstOrDefault(x=>x.IsDeleted==false&&
                              x.Id==booking.Id);
        if (existingBook==null) return false;
        _mapper.Map(booking, existingBook);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteBooking(int bookingId)
    {
        var existingBook = _context.Booking.FirstOrDefault(b => b.IsDeleted == false && b.Id == bookingId);
        if (existingBook == null) return false;
        existingBook.IsDeleted = true;
        existingBook.DeletedAt=DateTime.UtcNow;
        _context.SaveChanges();
        return true;
    }
}