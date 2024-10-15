using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API.Services
{
    public interface IReviewServices
    {
        Task<Review> AddReview(ReviewDTO reviewdto);
        Task<Review> UpdateReview(int id, ReviewDTO reviewDto);
        Task<bool> DeleteReview(int id);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task<IEnumerable<Review>> GetReviewsByTourId(int tourId);
        Task<IEnumerable<Review>> GetReviewsByUserId(int userId);
        IEnumerable<Review> SearchReviews(int? userid, int? tourid, decimal? rating, DateTime? reviewdate);
    }
}
