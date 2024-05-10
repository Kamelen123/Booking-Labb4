namespace Booking_Labb4.Services
{
    public interface IBooking<T>
    {
        
        Task<IEnumerable<T>> GetAll();

        Task<T> GetSingel(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    
    }
}
