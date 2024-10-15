using System.ComponentModel.DataAnnotations;

namespace Tourism_Management_System_API.DTO
{
    public class TourPackageUpdateDTO
    {
        public int? UserId { get; set; }
        public int? TourId { get; set; }
        public decimal? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
