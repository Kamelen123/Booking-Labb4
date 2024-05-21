using BookingModels;

namespace Booking_Labb4.Data.Dto
{
    public class CustomerBookingDTO
    {
        public int AppointmentId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string CustomerNotes { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }
    }
}
