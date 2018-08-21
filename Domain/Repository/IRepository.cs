using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T GetByIDstr(string id);
        void Create(T item);
        void Update(T item);
        void Delete(int id); 
        IEnumerable<T> Find(Func<T, Boolean> predicate);
    }
}
