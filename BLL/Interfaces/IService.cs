using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T>        //generic interface for CRUD operations
    {
        IEnumerable<T> GetItems();
        Task CreateItem(T ItemDTO);
        Task EditItem(T ItemDTO);
        T GetItem(int? id);
        Task DeleteItem(int? id);
    }
}
