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
                if (context.Users.Any(x => x.UserName == username))
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

        public void BuyProduct(long userId, long productId, int amount)
        {
            using (var context = new DatabaseContext())
            {
                var inventoryItem = context
                    .InventoryItem
                    .FirstOrDefault(i => i.Product.Id == productId);

                if (inventoryItem == null || inventoryItem.Amount < amount)
                {
                    throw new Exception("Product is not available or out of stock");
                }

                inventoryItem.Amount = inventoryItem.Amount - amount;

                var user = context.Users.Single(x => x.Id == userId);
                var product = context.Producten.Single(x => x.Id == productId);
                var totalPrice = product.Price * amount;

                context.Orders.Add(new Order
                {
                    User = user,
                    Product = product,
                    Amount = amount,
                    TotalPrice = totalPrice
                });

                SetBalance(user.UserName, totalPrice);

                context.SaveChanges();
            }
        }

        public UserDto GetUserID(string username)
        {
            using (var context = new DatabaseContext())
            {
                var user = context
                    .Users
                    .First(x => x.UserName == username);

                return new UserDto
                {
                    UserID = user.Id
                };
            }
        }

        public ProductDto GetProductID(string productName)
        {
            using (var context = new DatabaseContext())
            {
                var product = context
                    .Producten
                    .First(x => x.Name == productName);

                return new ProductDto
                {
                    ProductID = product.Id
                };
            }
        }
        public User GetUser(long user_Id)
        {
            using (var context = new DatabaseContext())
            {
                var user = context
                    .Users
                    .First(x => x.Id == user_Id);

                return new User
                {
                    Balance = user.Balance,
                    Id = user.Id,
                    Orders = user.Orders,
                    Password = user.Password,
                    UserName = user.UserName
                };
            }
        }

        public Product GetProduct(long product_Id)
        {
            using (var context = new DatabaseContext())
            {
                var product = context
                    .Producten
                    .First(x => x.Id == product_Id);

                return new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
            }
        }

        public UserDto GetBalance(string username)
        {
            using (var context = new DatabaseContext())
            {
                var user = context
                    .Users
                    .First(x => x.UserName == username);

                return new UserDto
                {
                    Balance = user.Balance
                };
            }
        }

        public void SetBalance(string username, int totalPrice)
        {
            using (var context = new DatabaseContext())
            {
                var user = context
                    .Users
                    .First(x => x.UserName == username);

                user.Balance -= totalPrice;

                context.SaveChanges();
            }
        }
    }
}
