namespace Tourism_Management_System_API.DTO
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}
