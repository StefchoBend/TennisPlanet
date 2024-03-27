using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(int productId, string userId, int quantity)
        {
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return false;
            }

            Order item = new Order
            {
                OrderDate = DateTime.Now,
                ProductId = productId,
                UserId = userId,
                Quantity = quantity,
                Price = product.ProductItem.Price,
                Discount = product.ProductItem.Discount,
            };

            product.QuantityInStock -= quantity;

            this._context.Products.Update(product);
            this._context.Orders.Add(item);

                return _context.SaveChanges() != 0;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId)
               .OrderByDescending(x => x.OrderDate)
               .ToList();
        }

        public bool RemoveById(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order == default(Order))
            {
                return false;
            }

            var product = _context.Products.FirstOrDefault(x => x.Id == order.ProductId);
            if (product == null) { return false; }

            product.QuantityInStock += order.Quantity;

            _context.Products.Update(product);
            _context.Remove(order);
            return _context.SaveChanges() != 0;
        }


    }
}
