using Booking_Labb4.Data.Dto;
using Booking_Labb4.Repository;
using BookingModels;

namespace Booking_Labb4.Services
{
    public interface IAppointment : IBooking<Appointment>
    {
        public Task<IEnumerable<AppointmentHistoryDto>> GetAppointmentChanges(int appointmentId);
        
    }
}
