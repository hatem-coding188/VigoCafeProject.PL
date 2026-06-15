using System;
using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using DocumentFormat.OpenXml.Math;


namespace BLL.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public Customer Register(string fullName, string email, string password, string phone, string address)
        {
            if (EmailExists(email))
                throw new Exception("Email already registered.");

            var customer = new Customer
            {
                FullName = fullName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                PhoneNumber = phone,
                Address = address,
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Create empty cart
            _context.Carts.Add(new Cart { CustomerId = customer.Id });
            _context.SaveChanges();

            return customer;
        }

        public Customer Login(string email, string password)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            if (customer == null) return null;
            return BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash) ? customer : null;
        }

        public bool EmailExists(string email)
            => _context.Customers.Any(c => c.Email == email);

        public Customer GetById(int id)
            => _context.Customers.FirstOrDefault(c => c.Id == id);

        public void UpdateProfile(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
    }
}