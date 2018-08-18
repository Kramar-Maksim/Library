using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IService<T>        //generic interface for CRUD operations
    {
        IEnumerable<T> GetItems(); 
        void CreateItem(T ItemDTO); 
        void EditItem(T ItemDTO); 
        T GetItem(int? id); 
        void DeleteItem(int? id);
    }
}
