using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API.Services;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism_Management_System_API.DTO.Tourism_Management.DTOs;

namespace Tourism_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourPackageController : ControllerBase
    {
        private readonly ITourPackageServices _tourService;
        private readonly IMapper _mapper;

        public TourPackageController(ITourPackageServices tourService, IMapper mapper)
        {
            _tourService = tourService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddTour([FromBody] TourPackageDTO tourPackageDTO)
        {
            try
            {
                if (tourPackageDTO == null)
                {
                    return BadRequest("Tour package data is null");
                }
                var tourPackage = _mapper.Map<TourPackage>(tourPackageDTO);
                var createdTour = await _tourService.AddTour(tourPackage);
                var createdTourDto = _mapper.Map<TourPackageDTO>(createdTour);
                return CreatedAtAction(nameof(GetTourById), new { id = createdTour.TourId }, createdTourDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddTour: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTour(int id, [FromBody] TourPackageDTO tourPackageDTO)
        {
            try
            {
                var tourPackage = _mapper.Map<TourPackage>(tourPackageDTO);
                var updatedTour = await _tourService.UpdateTour(id, tourPackage);
                var updatedTourDto = _mapper.Map<TourPackageDTO>(updatedTour);
                return Ok(updatedTourDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            try
            {
                var result = await _tourService.DeleteTour(id);
                return result ? NoContent() : NotFound(new { message = "Tour not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTours()
        {
            try
            {
                var tours = await _tourService.GetAllTours();
                var toursDto = _mapper.Map<IEnumerable<TourPackageDTO>>(tours);
                return Ok(toursDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int id)
        {
            try
            {
                var tour = await _tourService.GetTourById(id);
                if (tour == null)
                {
                    return NotFound(new { message = "Tour not found." });
                }
                var tourDto = _mapper.Map<TourPackageDTO>(tour);
                return Ok(tourDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetToursByCategory(string category)
        {
            try
            {
                var tours = await _tourService.GetToursByCategory(category);
                var toursDto = _mapper.Map<IEnumerable<TourPackageDTO>>(tours);
                return Ok(toursDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("location/{location}")]
        public async Task<IActionResult> GetToursByLocation(string location)
        {
            try
            {
                var tours = await _tourService.GetToursByLocation(location);
                var toursDto = _mapper.Map<IEnumerable<TourPackageDTO>>(tours);
                return Ok(toursDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult SearchTours([FromQuery] TourPackageSearchDTO searchDto)
        {
            try
            {
                var tourPackages = _tourService.SearchTours(searchDto.TourName, searchDto.Price, searchDto.Category, searchDto.Location);
                var tourDtos = _mapper.Map<IEnumerable<TourPackageDTO>>(tourPackages);
                return Ok(tourDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
