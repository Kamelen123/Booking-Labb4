using Booking_Labb4.Data;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.EntityFrameworkCore;

namespace Booking_Labb4.Repository
{
    public class CustomerRepository : ICustomer
    {
        private AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var result = await _appDbContext.Customers.AddAsync(newEntity);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;

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
    }
}
