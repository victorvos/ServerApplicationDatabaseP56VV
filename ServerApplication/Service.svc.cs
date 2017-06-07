using ServerApplication.Database;
using System.Linq;
using System;
using ServerApplication.Extensions;
using ServerApplication.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace ServerApplication
{
    public class Service : IService
    {
        public bool Login(string username, string password)
        {
            using (var context = new DatabaseContext())
            {
                return context.Users.Any(x => x.UserName == username && x.Password == password);
            }
        }

        public string RegistreerUser(string username)
        {
            using (var context = new DatabaseContext())
            {
                if(context.Users.Any(x => x.UserName == username))
                {
                    return null;
                }

                var password = username.Reverse();
                context.Users.Add(new User
                {
                    UserName = username,
                    Password = password,
                    Balance = 2500
                });
                context.SaveChanges();

                return password;
            }
        }
        public List<ProductDto> GetAvailableProducts()
        {
            using (var context = new DatabaseContext())
            {
                return context
                    .Stores
                    .Include(x => x.InventoryItems)
                    .Include(x => x.InventoryItems.Select(p => p.Product))
                    .Single(x => x.Name == "Standard")
                    .InventoryItems
                    .Select(x => new ProductDto
                    {
                        Name = x.Product.Name,
                        AvailableAmount = x.Amount,
                        Price = x.Product.Price
                    })
                    .ToList();
            }
        }

        public List<OrderDto> GetInventory(string username)
        {
            using (var context = new DatabaseContext())
            {
                return context
                    .Users
                    .Include(x => x.Orders)
                    .Include(x => x.Orders.Select(p => p.Product))
                    .Single(x => x.UserName == username)
                    .Orders
                    .GroupBy(p => p.Product.Name)
                    .Select(x => new OrderDto
                    {
                        ProductName = x.Key,
                        Amount = x.Sum(a => a.Amount)
                    })
                    .ToList();
            }
        }
    }
}
