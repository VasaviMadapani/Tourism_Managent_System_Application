using AutoMapper;
using Tourism_Management_System_API.DTO.Tourism_Management.DTOs;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API_Project_.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                // Booking Mappings
                CreateMap<Booking, UpdateBookingDto>().ReverseMap();
                CreateMap<Booking, CreateBookingDto>().ReverseMap();
                CreateMap<Booking, BookingDto>().ReverseMap();
                CreateMap<Booking, UpdateBookingDto>().ReverseMap();

            // UserManagement Mappings
            CreateMap<UserManagement, UserSearchDto>().ReverseMap();
                CreateMap<UserManagement, UserDTO>().ReverseMap();
                CreateMap<UserManagement, UserProfileDTO>().ReverseMap();
                CreateMap<UserManagement, UserRegistrationDTO>().ReverseMap();
                CreateMap<UserManagement, LoginDto>().ReverseMap();

            // TourPackage Mappings
                CreateMap<TourPackage, TourPackageUpdateDTO>().ReverseMap();
                CreateMap<TourPackage, TourPackageDTO>().ReverseMap();
                CreateMap<TourPackage, TourPackageSearchDTO>().ReverseMap();

            // Review Mappings
                CreateMap<Review, ReviewSearchDTO>().ReverseMap();
                CreateMap<Review, ReviewDTO>().ReverseMap();
            }
        }
    }
