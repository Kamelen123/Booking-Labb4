using Booking_Labb4.Data.Dto;
using BookingModels;

namespace Booking_Labb4.Services
{
    public interface ICompany : IBooking<Company>
    {
        Task<IEnumerable<Appointment>> Search(DateOnly date);
        Task<IEnumerable<Appointment>> SearchByCompanyIdAndMonth(int companyId, int year, int month);

        Task<IEnumerable<CompanyAppointmentDto>> Test(int companyId, int year, int month);
        Task<Appointment> DeleteCompanyAppointment(int companyId, int appointmentId);
    }
}
