using System.Collections.Generic;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetBySubcategoryId(int subId);
        IEnumerable<Product> GetFeatured();
        IEnumerable<Product> GetLatest();
        IEnumerable<Product> Search(string keyword);
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}