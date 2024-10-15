using System;
using System.Collections.Generic;

namespace Tourism_Management_System_API.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? TourId { get; set; }

    public DateTime BookingDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual TourPackage? Tour { get; set; }

    public virtual UserManagement? User { get; set; }
}
