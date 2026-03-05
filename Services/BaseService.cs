using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly ILogger<BaseService<T>> _logger;

        protected BaseService(AppDbContext context, ILogger<BaseService<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<T>> GetAllAsync()
        {
            _logger.LogInformation($"{typeof(T).Name} listesi başarıyla getirildi.");
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"{typeof(T).Name} (ID: {id}) bilgisi okundu.");
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[BAŞARILI] Yeni {typeof(T).Name} eklendi.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(T).Name} eklenirken hata!");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{typeof(T).Name} başarıyla güncellendi.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(T).Name} güncellenirken hata oluştu!");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null) return false;

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"{typeof(T).Name} (ID: {id}) başarıyla silindi.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(T).Name} silinirken hata oluştu!");
                return false;
            }
        }
    }
}