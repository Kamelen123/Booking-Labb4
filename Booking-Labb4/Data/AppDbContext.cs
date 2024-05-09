using BookingModels;
using Microsoft.EntityFrameworkCore;

namespace Booking_Labb4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // History Table (Please Work)
            modelBuilder.Entity<Appointment>().ToTable("Appointment", options =>
            {
                options.IsTemporal();
            });

            //Seed Data Company
            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 1,
                CompanyName = "Lundgrens",
                PhoneNumber = "1234567890",
            });
            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 2,
                CompanyName = "Swedish Match",
                PhoneNumber = "2345678901",
            });


            //Seed Data Customer
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 1,
                FirstName = "Torbjörn",
                LastName = "Röd",
                Address = "Annebergsvägen 4",
                PhoneNumber = "0765421578",

            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 2,
                FirstName = "Albin",
                LastName = "Blå",
                Address = "Västra vallgatan 5",
                PhoneNumber = "0761237578",

            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 3,
                FirstName = "Daniel",
                LastName = "Gul",
                Address = "Storgatan 1a",
                PhoneNumber = "0765486754",

            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 4,
                FirstName = "Anton",
                LastName = "Grön",
                Address = "Smågatan 2b",
                PhoneNumber = "0734576878",

            });


            //Seed Data Appointment
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 1,
                CompanyNotes = "Inquire about sponsorship",
                CustomerNotes = "Meeting with Lundgrens",
                CompanyId = 1,
                CustomerId = 1, 
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(9, 0),
                TimeTo = new TimeOnly(10, 0)
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 2,
                CompanyNotes = "Remember the snus.",
                CustomerNotes ="Ask about the lingon snus.",
                CompanyId = 1,
                CustomerId = 2,
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(11, 0),
                TimeTo = new TimeOnly(13, 0)
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 3,
                CompanyNotes = "Wow, such anger.",
                CustomerNotes = "Stay Calm",
                CompanyId = 2,
                CustomerId = 3,
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(10, 0),
                TimeTo = new TimeOnly(14, 0)
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 4,
                CompanyNotes = "Great Guy",
                CustomerNotes = "",
                CompanyId = 1,
                CustomerId = 4,
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(9, 0),
                TimeTo = new TimeOnly(12, 0)
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 5,
                CompanyNotes = "",
                CustomerNotes = "",
                CompanyId = 2,
                CustomerId = 1,
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(13, 0),
                TimeTo = new TimeOnly(16, 0)
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 6,
                CompanyNotes = "",
                CustomerNotes = "",
                CompanyId = 1,
                CustomerId = 3,
                Date = new DateOnly(2024, 5, 9),
                TimeFrom = new TimeOnly(13, 0),
                TimeTo = new TimeOnly(16, 0)
            });
        }
    }
}
