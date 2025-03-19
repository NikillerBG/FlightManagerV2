using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICrudDb<T, K> where T : class
    {
        public Task CreateAsync(T item);

        public Task<T> ReadAsync(K id, bool useNavigationalProperties = false, bool isReadOnly = false);

        public Task<List<T>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);

        public Task UpdateAsync(T item, bool useNavigationalProperties = false);

        public Task DeleteAsync(K id);

        public Task<List<T>> GetAsync(int skip, int take, bool useNavigationalProperties = false, bool isReadOnly = false);

    }
}
