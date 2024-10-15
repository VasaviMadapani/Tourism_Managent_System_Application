using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.DTO.Tourism_Management.DTOs;
using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API.Services
{
    public interface ITourPackageServices
    {
        Task<TourPackage> AddTour(TourPackage tour);
        Task<TourPackage> UpdateTour(int id, TourPackage tour);
        Task<bool> DeleteTour(int id);
        Task<IEnumerable<TourPackage>> GetAllTours();
        Task<TourPackage> GetTourById(int id);
        Task<IEnumerable<TourPackage>> GetToursByCategory(string category);
        Task<IEnumerable<TourPackage>> GetToursByLocation(string location);
        IEnumerable<TourPackage> SearchTours(string tourname,decimal? price,string category,string location);
    }
}
