namespace Booking_Labb4.Data.Dto
{
    public class CompanyAppointmentDto
    {
        public int AppointmentId { get; set; }
        public string CompanyNotes { get; set; }
        public string FirstName { get; set; }
        public int CompanyId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }
    }
}
