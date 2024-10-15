using System;
using System.Collections.Generic;

namespace Tourism_Management_System_API.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? TourId { get; set; }

    public decimal? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual TourPackage? Tour { get; set; }

    public virtual UserManagement? User { get; set; }
}
