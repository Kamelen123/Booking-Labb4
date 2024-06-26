﻿using Booking_Labb4.Data.Dto;
using BookingModels;

namespace Booking_Labb4.Services
{
    public interface ICustomer : IBooking<Customer>
    {
        Task<IEnumerable<Appointment>> GetAppointment(int customerId);
        Task<IEnumerable<CustomerBookingDTO>> CustomerAppointmentInfo(int customerId);
        Task<IEnumerable<CustomerBookingDTO>> SearchByMonth(int year, int month);
        Task<double> GetCustomerHours(int customerId, int year, int month);
        Task <Appointment> DeleteCustomerAppointment(int customerId, int appointmentId);
        Task<Appointment> AddCustomerAppointment(int customerid, Appointment newEntity);
        Task<Appointment> UpdateCustomerAppointment(Appointment updatedEntity);
    }
}
