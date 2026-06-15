using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly AppDbContext _context;

        public SubcategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Subcategory> GetAll()
            => _context.Subcategories.Where(s => s.IsActive).ToList();

        public IEnumerable<Subcategory> GetByCategoryId(int categoryId)
            => _context.Subcategories
                       .Where(s => s.CategoryId == categoryId && s.IsActive)
                       .ToList();

        public Subcategory GetById(int id)
            => _context.Subcategories.FirstOrDefault(s => s.Id == id);

        public Subcategory GetByIdWithProducts(int id)
            => _context.Subcategories
                       .Include(s => s.Products)
                       .FirstOrDefault(s => s.Id == id);

        public void Add(Subcategory subcategory)
        {
            _context.Subcategories.Add(subcategory);
            _context.SaveChanges();
        }

        public void Update(Subcategory subcategory)
        {
            _context.Subcategories.Update(subcategory);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sub = GetById(id);
            if (sub != null)
            {
                _context.Subcategories.Remove(sub);
                _context.SaveChanges();
            }
        }
    }
}