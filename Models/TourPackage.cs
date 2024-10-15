using System;
using System.Collections.Generic;

namespace Tourism_Management_System_API.Models;

public partial class TourPackage
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Category { get; set; }

    public int Duration { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Rating { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
