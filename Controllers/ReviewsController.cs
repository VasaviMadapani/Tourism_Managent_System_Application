using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API.Services;
using AutoMapper;

namespace Tourism_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewServices _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewServices reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDto)
        {
            try
            {
                // Ensure reviewDto is not null
                if (reviewDto == null)
                {
                    return BadRequest("Review data is required.");
                }

                // Use the service to add the review
                var createdReview = await _reviewService.AddReview(reviewDto);
                var createdReviewDto = _mapper.Map<ReviewDTO>(createdReview);
                return CreatedAtAction(nameof(GetReviewById), new { id = createdReviewDto.ReviewId }, createdReviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDTO reviewDto)
        {
            try
            {
                // Ensure reviewDto is not null
                if (reviewDto == null)
                {
                    return BadRequest("Review data is required.");
                }

                var updatedReview = await _reviewService.UpdateReview(id, reviewDto);
                if (updatedReview == null)
                {
                    return NotFound(new { message = "Review not found." });
                }

                var updatedReviewDto = _mapper.Map<ReviewDTO>(updatedReview);
                return Ok(updatedReviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var result = await _reviewService.DeleteReview(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(new { message = "Review not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviews();
                var reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
                return Ok(reviewsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewById(id);
                if (review == null)
                {
                    return NotFound(new { message = "Review not found." });
                }
                var reviewDto = _mapper.Map<ReviewDTO>(review);
                return Ok(reviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetReviewsByTourId(int tourId)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByTourId(tourId);
                var reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
                return Ok(reviewsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByUserId(userId);
                var reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
                return Ok(reviewsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchReviews([FromQuery] int? userId, [FromQuery] int? tourId, [FromQuery] decimal? rating, [FromQuery] DateTime? reviewDate)
        {
            try
            {
                var reviews = _reviewService.SearchReviews(userId, tourId, rating, reviewDate);
                var reviewsDto = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
                return Ok(reviewsDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching reviews.", error = ex.Message });
            }
        }
    }
}
