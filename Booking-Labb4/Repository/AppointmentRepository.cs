using AutoMapper;
using Booking_Labb4.Data;
using Booking_Labb4.Data.Dto;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Booking_Labb4.Repository
{
    public class AppointmentRepository : IAppointment
    {
        private AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public AppointmentRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Appointment> Add(Appointment newEntity)
        {
            var result = await _appDbContext.Appointments.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Appointment> Delete(int id)
        {
            var result = await _appDbContext.Appointments.
                FirstOrDefaultAsync(c => c.AppointmentId == id);
            if (result != null)
            {
                _appDbContext.Appointments.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _appDbContext.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetSingel(int id)
        {
            return await _appDbContext.Appointments.FirstOrDefaultAsync(c => c.AppointmentId == id);
        }

        public async Task<Appointment> Update(Appointment entity)
        {
            var result = await _appDbContext.Appointments.
               FirstOrDefaultAsync(c => c.AppointmentId == entity.AppointmentId);

            if (result != null)
            {
                result.CompanyNotes = entity.CompanyNotes;
                result.CustomerNotes = entity.CustomerNotes;
                result.Date = entity.Date;
                result.TimeFrom = entity.TimeFrom;
                result.TimeTo = entity.TimeTo;

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<Appointment>> SearchByMonth(int year, int month)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Date.Year == year && a.Date.Month == month)
                .ToListAsync();
        }
        public async Task<IEnumerable<AppointmentHistoryDto>> GetAppointmentChanges(/*int appointmentId*/)
        {
            var history = await _appDbContext
                .Appointments
                .TemporalAll()
                //.Where(a => a.AppointmentId == appointmentId)
                .OrderBy(a => EF.Property<DateTime>(a, "PeriodStart"))
                .ToListAsync();

            return _mapper.Map<List<AppointmentHistoryDto>>(history);
        }

    }
}
