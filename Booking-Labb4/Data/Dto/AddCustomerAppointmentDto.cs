namespace Booking_Labb4.Data.Dto
{
    public class AddCustomerAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string CustomerNotes { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }
    }
}
