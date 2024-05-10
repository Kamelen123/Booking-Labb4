using AutoMapper;
using Booking_Labb4.Data.Dto;
using BookingModels;

namespace Booking_Labb4.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
        }
    }
}
