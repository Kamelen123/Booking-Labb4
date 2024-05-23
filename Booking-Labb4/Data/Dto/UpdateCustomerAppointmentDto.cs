

namespace Booking_Labb4.Data.Dto
{
    public class UpdateCustomerAppointmentDto
    {
        public string CustomerNotes { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }
    }
}
