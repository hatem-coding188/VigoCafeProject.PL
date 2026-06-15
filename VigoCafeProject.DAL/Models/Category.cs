using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}