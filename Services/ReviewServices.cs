using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management_System_API.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly TourManagementSystemContext _context;
        private readonly IMapper _mapper;

        public ReviewServices(TourManagementSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Review> AddReview(ReviewDTO reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            await _context.Review.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> UpdateReview(int id, ReviewDTO reviewDto)
        {
            var existingReview = await _context.Review.FindAsync(id);
            if (existingReview == null) throw new Exception("Review not found.");

            _mapper.Map(reviewDto, existingReview);
            await _context.SaveChangesAsync();
            return existingReview;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null) return false;

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<Review> GetReviewById(int id)
        {
            return await _context.Review.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByTourId(int tourId)
        {
            return await _context.Review.Where(r => r.TourId == tourId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserId(int userId)
        {
            return await _context.Review.Where(r => r.UserId == userId).ToListAsync();
        }
        public IEnumerable<Review> SearchReviews(int? userid, int? tourid, decimal? rating, DateTime? reviewdate)
        {
            var query = _context.Review.AsQueryable();
            if (userid.HasValue)
            {
                query = query.Where(b => b.UserId == userid.Value);
            }

            if (tourid.HasValue)
            {
                query = query.Where(b => b.TourId == tourid.Value);
            }
            if (rating.HasValue)
            {
                query = query.Where(r => r.Rating == rating.Value);
            }

            if (reviewdate.HasValue)
            {
                query = query.Where(r => r.ReviewDate.Value.Date == reviewdate.Value.Date);
            }
            return query.ToList();
        }
    }
}
