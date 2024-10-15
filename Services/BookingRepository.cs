using Tourism_Management_System_API.Models;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management_System_API.Services
{
        public class BookingRepository : IBookingRepository
        {
            private readonly TourManagementSystemContext _context;

            public BookingRepository(TourManagementSystemContext context)
            {
                _context = context;
            }

            public void CreateBooking(Booking booking)
            {
                _context.Booking.Add(booking);
                _context.SaveChanges();
            }

            public Booking GetBookingById(int bookingId)
            {
                return _context.Booking.Find(bookingId);
            }

            public IEnumerable<Booking> GetAllBookings()
            {
                return _context.Booking.ToList();
            }

            public IEnumerable<Booking> GetBookingsByUserId(int userId)
            {
                return _context.Booking.Where(b => b.UserId == userId).ToList();
            }

            public IEnumerable<Booking> GetBookingsByTourId(int tourId)
            {
                return _context.Booking.Where(b => b.TourId == tourId).ToList();
            }

            public IEnumerable<Booking> GetBookingsByDate(DateTime date)
            {
                return _context.Booking.Where(b => b.BookingDate.Date == date.Date).ToList();
            }

            public void UpdateBooking(Booking booking)
            {
                _context.Booking.Update(booking);
                _context.SaveChanges();
            }

            public void DeleteBooking(int bookingId)
            {
                var booking = _context.Booking.Find(bookingId);
                if (booking != null)
                {
                    _context.Booking.Remove(booking);
                    _context.SaveChanges();
                }
            }
            public IEnumerable<Booking> SearchBookings(int? userId, int? tourId, DateTime? date)
            {
                var query = _context.Booking.AsQueryable();

                if (userId.HasValue)
                {
                    query = query.Where(b => b.UserId == userId.Value);
                }
                if (tourId.HasValue)
                {
                    query = query.Where(b => b.TourId == tourId.Value);
                }
                if (date.HasValue)
                {
                    query = query.Where(b => b.BookingDate.Date == date.Value.Date);
                }

                return query.ToList();
            }

        }
    
}
