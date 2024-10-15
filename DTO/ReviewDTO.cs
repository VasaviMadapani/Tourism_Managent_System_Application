namespace Tourism_Management_System_API.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }


    }
}
