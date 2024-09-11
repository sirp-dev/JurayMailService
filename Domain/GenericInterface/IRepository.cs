using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GenericInterface
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);

        Task<IEnumerable<T>> GetPageAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
    }
}
