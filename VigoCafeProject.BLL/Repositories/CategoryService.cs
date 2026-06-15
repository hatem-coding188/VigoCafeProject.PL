using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
            => _context.Categories.Where(c => c.IsActive).ToList();

        public Category GetById(int id)
            => _context.Categories.FirstOrDefault(c => c.Id == id);

        public Category GetByIdWithSubcategories(int id)
            => _context.Categories
                       .Include(c => c.Subcategories)
                       .FirstOrDefault(c => c.Id == id);

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}