using Microsoft.EntityFrameworkCore;
using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API_Project_.Data
{
    public class TourManagementSystemContext:DbContext
    {
        public TourManagementSystemContext(DbContextOptions<TourManagementSystemContext> options) : base(options) { }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<TourPackage> TourPackage { get; set; }
        public DbSet<UserManagement> UserManagement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.BookingId).HasColumnName("BookingID");
                entity.Property(e => e.BookingDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.TourId).HasColumnName("TourID");
                entity.HasOne(d => d.Tour).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Bookings_TourID");
                entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Bookings_UserId");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId);
                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
                entity.Property(e => e.Comment).IsUnicode(false);
                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
                entity.Property(e => e.ReviewDate).HasColumnType("datetime");
                entity.Property(e => e.TourId).HasColumnName("TourID");
                //entity.HasOne(d => d.Tour).WithMany(p => p.ReviewsNavigation)
                //    .HasForeignKey(d => d.TourId)
                //    .HasConstraintName("FK_Reviews_TourID");
                entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reviews_UserId");
            });

            modelBuilder.Entity<TourPackage>(entity =>
            {
                entity.HasKey(e => e.TourId);
                entity.Property(e => e.TourId).HasColumnName("TourID");
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ImageURL");
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
                entity.Property(e => e.TourName).HasMaxLength(100);
            });

            modelBuilder.Entity<UserManagement>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("UserManagement");
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Role).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

