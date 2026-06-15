using System.Collections.Generic;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface ISubcategoryService
    {
        IEnumerable<Subcategory> GetAll();
        IEnumerable<Subcategory> GetByCategoryId(int categoryId);
        Subcategory GetById(int id);
        Subcategory GetByIdWithProducts(int id);
        void Add(Subcategory subcategory);
        void Update(Subcategory subcategory);
        void Delete(int id);
    }
}