using Microsoft.EntityFrameworkCore;
using System.Reflection;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.DTOs;

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

        // --- GÜNCELLENEN METOT ---
        public virtual async Task<List<T>> GetAllAsync(QueryParams @params)
        {
            _logger.LogInformation($"{typeof(T).Name} listesi filtreli/sayfalı getiriliyor.");

            IQueryable<T> query = _context.Set<T>();

            // 1. FİLTRELEME (Reflection Kullanarak)
            if (!string.IsNullOrEmpty(@params.FilterField) && !string.IsNullOrEmpty(@params.Keyword))
            {
                // T (Entity) içindeki ilgili alanı buluyoruz
                var prop = typeof(T).GetProperty(@params.FilterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null)
                {
                    // Bellekte değil, veritabanı sorgusunda filtrelemek için basit bir string araması
                    
                    query = query.AsEnumerable()
                                 .Where(x => prop.GetValue(x, null)?.ToString()?
                                 .Contains(@params.Keyword, StringComparison.OrdinalIgnoreCase) ?? false)
                                 .AsQueryable();
                }
            }

            // 2. SIRALAMA (Reflection Kullanarak)
            if (!string.IsNullOrEmpty(@params.SortField))
            {
                var prop = typeof(T).GetProperty(@params.SortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    query = query.OrderBy(x => prop.GetValue(x, null));
                }
            }

            // 3. SAYFALAMA (Pagination)
            if (@params.IsPaginationEnabled)
            {
                int skip = (@params.PageNumber - 1) * @params.PageSize;
                query = query.Skip(skip).Take(@params.PageSize);
            }

            return await query.ToListAsync();
        }

      

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
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
                var updatedDateProp = typeof(T).GetProperty("UpdatedDate");
                if (updatedDateProp != null)
                {
                    updatedDateProp.SetValue(entity, DateTime.UtcNow);
                }

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
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