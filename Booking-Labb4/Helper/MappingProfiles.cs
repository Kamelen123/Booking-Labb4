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
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<AddCustomerDto, Customer>();
            CreateMap<Customer, AddCustomerDto>();
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<Appointment,CompanyAppointmentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName));
            
        }
    }
}
