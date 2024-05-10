using Booking_Labb4.Data;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.EntityFrameworkCore;

namespace Booking_Labb4.Repository
{
    public class CompanyRepository : ICompany
    {
        private AppDbContext _appDbContext;
        public CompanyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
    }
}
