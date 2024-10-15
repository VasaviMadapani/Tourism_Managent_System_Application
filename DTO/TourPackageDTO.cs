namespace Tourism_Management_System_API.DTO
{
    namespace Tourism_Management.DTOs
    {
        public class TourPackageDTO
        {
            public string TourName { get; set; }
            public string Description { get; set; }
            public decimal? Price { get; set; }
            public string Category { get; set; }
            public int Duration { get; set; }
            public string ImageUrl { get; set; }
            public decimal Rating { get; set; }
            public string Location { get; set; }
        }
    }

}
