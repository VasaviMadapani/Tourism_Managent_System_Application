using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API.Services
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking);
        Booking GetBookingById(int bookingId);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetBookingsByUserId(int userId);
        IEnumerable<Booking> GetBookingsByTourId(int tourId);
        IEnumerable<Booking> GetBookingsByDate(DateTime date);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int bookingId);
        IEnumerable<Booking> SearchBookings(int? userId, int? tourId, DateTime? date);
    }
}
