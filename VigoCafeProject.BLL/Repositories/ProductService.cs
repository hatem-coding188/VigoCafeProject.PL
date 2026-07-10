using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;

namespace BLL.Repositories
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
            => _context.Products.Where(p => p.IsAvailable).ToList();

        public IEnumerable<Product> GetAllForAdmin()
            => _context.Products.ToList();

        public IEnumerable<Product> GetBySubcategoryId(int subId)
            => _context.Products
                       .Where(p => p.SubcategoryId == subId && p.IsAvailable)
                       .ToList();

        public IEnumerable<Product> GetFeatured()
            => _context.Products
                       .Where(p => p.IsFeatured && p.IsAvailable)
                       .ToList();

        public IEnumerable<Product> GetLatest()
            => _context.Products
                       .Where(p => p.IsAvailable)
                       .OrderByDescending(p => p.Id)
                       .Take(4)
                       .ToList();

        public IEnumerable<Product> Search(string keyword)
            => _context.Products
                       .Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword))
                       .ToList();

        public Product GetById(int id)
            => _context.Products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // أولاً احذف CartItems المرتبطة
            var cartItems = _context.CartItems.Where(ci => ci.ProductId == id).ToList();
            if (cartItems.Any())
            {
                _context.CartItems.RemoveRange(cartItems);
            }

            // ثم احذف المنتج
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            _context.SaveChanges();
        }
    }
}