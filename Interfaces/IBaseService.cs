using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<List<T>> GetAllAsync(QueryParams queryParams);
        Task<T?> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}