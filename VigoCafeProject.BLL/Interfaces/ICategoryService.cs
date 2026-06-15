using System.Collections.Generic;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category GetByIdWithSubcategories(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}