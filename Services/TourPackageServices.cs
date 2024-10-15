using Microsoft.EntityFrameworkCore;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.DTO.Tourism_Management.DTOs;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management_System_API.Services
{
    public class TourPackageServices : ITourPackageServices
    {
        private readonly TourManagementSystemContext _context;

        public TourPackageServices(TourManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<TourPackage> AddTour(TourPackage tour)
        {
            await _context.TourPackage.AddAsync(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<TourPackage> UpdateTour(int id, TourPackage tour)
        {
            var existingTour = await _context.TourPackage.FindAsync(id);
            if (existingTour == null) throw new Exception("Tour not found.");
            // Update properties
            existingTour.TourName = tour.TourName;
            existingTour.Description = tour.Description;
            existingTour.Price = tour.Price;
            existingTour.Category = tour.Category;
            existingTour.Duration = tour.Duration;
            existingTour.ImageUrl = tour.ImageUrl;
            await _context.SaveChangesAsync();
            return existingTour;
        }

        public async Task<bool> DeleteTour(int id)
        {
            var tour = await _context.TourPackage.FindAsync(id);
            if (tour == null) return false;
            _context.TourPackage.Remove(tour);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TourPackage>> GetAllTours()
        {
            return await _context.TourPackage.ToListAsync();
        }

        public async Task<TourPackage> GetTourById(int id)
        {
            return await _context.TourPackage.FindAsync(id);
        }

        public async Task<IEnumerable<TourPackage>> GetToursByCategory(string category)
        {
            return await _context.TourPackage.Where(t => t.Category == category).ToListAsync();
        }

        public async Task<IEnumerable<TourPackage>> GetToursByLocation(string location)
        {
            return await _context.TourPackage.Where(t => t.Location.Contains(location)).ToListAsync();
        }

        public IEnumerable<TourPackage> SearchTours(string tourname, decimal? price, string category, string location)
        {
            var query = _context.TourPackage.AsQueryable();

            if (!string.IsNullOrEmpty(tourname))
            {
                query = query.Where(t => t.TourName.Contains(tourname));
            }
            if (price.HasValue)
            {
                query = query.Where(b => b.Price == price.Value);
            }
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category == category);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(t => t.Location == location);
            }
            return query.ToList();
        }
    }
}
