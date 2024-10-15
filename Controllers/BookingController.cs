using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API.Services;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism_Management_System_API_Project_.DTO;

namespace Tourism_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] CreateBookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            _bookingRepository.CreateBooking(booking);
            return Ok("Booking created successfully.");
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }
            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Ok(bookingDto);
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] UpdateBookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            booking.BookingId = id; // Ensure the ID is set
            _bookingRepository.UpdateBooking(booking);
            return Ok("Booking updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            _bookingRepository.DeleteBooking(id);
            return Ok("Booking deleted successfully.");
        }

        [HttpGet("search")]
        public IActionResult SearchBookings([FromQuery] BookingSearchDTO searchDto)
        {
            var bookings = _bookingRepository.SearchBookings(searchDto.UserId, searchDto.TourId, searchDto.Date);
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return Ok(bookingDtos);
        }
    }
}
