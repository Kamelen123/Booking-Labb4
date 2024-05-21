using AutoMapper;
using Booking_Labb4.Data.Dto;
using BookingModels;
using Microsoft.EntityFrameworkCore;

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
            CreateMap<Appointment, CustomerBookingDTO>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest => dest.CompanyPhoneNumber, opt => opt.MapFrom(src => src.Company.PhoneNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address));
            CreateMap<Appointment, AppointmentHistoryDto>();
                //.ForMember(dest => dest.PeriodStart, opt => opt.MapFrom(src => EF.Property<DateTime>(src, "PeriodStart")))
                //.ForMember(dest => dest.PeriodEnd, opt => opt.MapFrom(src => EF.Property<DateTime>(src, "PeriodEnd")));
            //CreateMap<AppointmentHistoryDto,Appointment>();


        }
    }
}
