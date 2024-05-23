using AutoMapper;
using Booking_Labb4.Data;
using Booking_Labb4.Data.Dto;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.EntityFrameworkCore;

namespace Booking_Labb4.Repository
{
    public class CompanyRepository : ICompany
    {
        private AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CompanyRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }
        public async Task<Company> Add(Company newEntity)
        {
            var result = await _appDbContext.Companies.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Company> Delete(int id)
        {
            var result = await _appDbContext.Companies.
                FirstOrDefaultAsync(c => c.CompanyId == id);
            if (result != null)
            {
                _appDbContext.Companies.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _appDbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetSingel(int id)
        {
            return await _appDbContext.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public async Task<IEnumerable<Appointment>> Search(DateOnly date)
        {
            //return await _appDbContext.Appointments.Where(d => d.Date == date).ToListAsync();
            IQueryable<Appointment> qury = _appDbContext.Appointments;
            if (!(qury == null))
            {
                qury = qury.Where(d => d.Date == date);
            }
            return await qury.ToListAsync();
        }

        public async Task<Company> Update(Company entity)
        {
            var result = await _appDbContext.Companies.
               FirstOrDefaultAsync(c => c.CompanyId == entity.CompanyId);

            if (result != null)
            {
                result.CompanyName = entity.CompanyName;
                result.PhoneNumber = entity.PhoneNumber;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<Appointment>> SearchByCompanyIdAndMonth(int companyId, int year, int month)
        {

            //return await _appDbContext.Appointments
            //    .Where(a => a.CompanyId == companyId && a.Date.Year == year && a.Date.Month == month)
            //    .ToListAsync();
            return await _appDbContext.Appointments
            .Include(a => a.Customer) 
            .Where(a => a.CompanyId == companyId && a.Date.Year == year && a.Date.Month == month)
            .ToListAsync();
        }

        public async Task<IEnumerable<CompanyAppointmentDto>> Test(int companyId, int year, int month)
        {
            var appointments = await SearchByCompanyIdAndMonth(companyId, year, month);

            if (!appointments.Any())
            {
                return new List<CompanyAppointmentDto>();
            }

            //var appointmentDtos = appointments.Select(a => new CompanyAppointmentDto
            //{
            //    AppointmentId = a.AppointmentId,
            //    CompanyNotes = a.CompanyNotes,
            //    CompanyId = a.CompanyId,
            //    FirstName = a.Customer.FirstName, 
            //    Date = a.Date,
            //    TimeFrom = a.TimeFrom,
            //    TimeTo = a.TimeTo
            //}).ToList();
            var appointmentDtos = _mapper.Map<IEnumerable<CompanyAppointmentDto>>(appointments);

            return appointmentDtos;
        }

        public async Task<Appointment> DeleteCompanyAppointment(int companyId, int appointmentId)
        {
            var appointmentToDelete = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.CompanyId == companyId && a.AppointmentId == appointmentId);
            if (appointmentToDelete != null)
            {
                _appDbContext.Appointments.Remove(appointmentToDelete);
                await _appDbContext.SaveChangesAsync();
                return appointmentToDelete;
            }
            return null;
        }

        public async Task<Appointment> AddCompanyAppointment(int customerid, Appointment newEntity)
        {
            newEntity.CustomerNotes ??= string.Empty;
            var result = await _appDbContext.Appointments.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Appointment> UpdateCompanyAppointment(Appointment updatedEntity)
        {
            _appDbContext.Appointments.Update(updatedEntity);
            await _appDbContext.SaveChangesAsync();
            return updatedEntity;
        }
    }
    
}
