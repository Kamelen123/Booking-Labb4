using AutoMapper;
using Booking_Labb4.Data;
using Booking_Labb4.Data.Dto;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Booking_Labb4.Repository
{
    public class CustomerRepository : ICustomer
    {
        private AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CustomerRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var result = await _appDbContext.Customers.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<IEnumerable<CustomerBookingDTO>> CustomerAppointmentInfo(int customerId)
        {
            var customerinfo = await GetAppointment(customerId);
            if (!customerinfo.Any())
            {
                return new List<CustomerBookingDTO>();
            }
            var customerAppointmentDtos = _mapper.Map<IEnumerable<CustomerBookingDTO>>(customerinfo);
            return customerAppointmentDtos;

        }

        public async Task<Customer> Delete(int id)
        {
            var result = await _appDbContext.Customers.
                FirstOrDefaultAsync(c => c.CustomerId == id);
            if (result != null)
            {
                _appDbContext.Customers.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _appDbContext.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointment(int customerId)
        {
            return await _appDbContext.Appointments
            .Include(a => a.Company)
            .Include(a => a.Customer)
            .Where(a => a.CustomerId == customerId)
            .ToListAsync();
        }

        public async Task<Customer> GetSingel(int id)
        {
            return await _appDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Update(Customer entity)
        {
            var result = await _appDbContext.Customers.
               FirstOrDefaultAsync(c => c.CustomerId == entity.CustomerId);

            if (result != null)
            {
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.Address = entity.Address;
                result.PhoneNumber = entity.PhoneNumber;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<CustomerBookingDTO>> SearchByMonth(int year, int month)
        {
            var customerAppointments = await _appDbContext.Appointments
            .Include(a => a.Company)
            .Include(a => a.Customer)
            .Where(a => a.Date.Year == year && a.Date.Month == month)
            .ToListAsync();

            if (!customerAppointments.Any())
            {
                return new List<CustomerBookingDTO>();
            }

            var customerBookingDtos = _mapper.Map<IEnumerable<CustomerBookingDTO>>(customerAppointments);

            return customerBookingDtos;
        }
        public async Task<double> GetCustomerHours(int customerId, int year, int month)
        {
            
            var customerBookingDtos = await SearchByMonth(year, month);

            var filteredAppointments = customerBookingDtos.Where(a => a.CustomerId == customerId);
            
            double totalHours = filteredAppointments.Sum(a => (a.TimeTo - a.TimeFrom).TotalHours);

            return totalHours;
        }

        public async Task<Appointment> DeleteCustomerAppointment(int customerId, int appointmentId)
        {
            var appointmentToDelete = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.CustomerId == customerId && a.AppointmentId == appointmentId);
            if (appointmentToDelete != null)
            {
                _appDbContext.Appointments.Remove(appointmentToDelete);
                await _appDbContext.SaveChangesAsync();
                return appointmentToDelete;
            }
            return null;
        }

        public async Task<Appointment> AddCustomerAppointment(int customerid, Appointment newEntity)
        {
            newEntity.CompanyNotes ??= string.Empty;
            var result = await _appDbContext.Appointments.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
