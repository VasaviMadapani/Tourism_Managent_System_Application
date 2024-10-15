namespace Tourism_Management_System_API.DTO
{
    public class ReviewSearchDTO
    {
        public int? UserId { get; set; }
        public int? TourId { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? ReviewDate { get; set; }
    }
}
