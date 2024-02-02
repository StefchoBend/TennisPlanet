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
                ProductItemId = productId,
                UserId = userId,
                Quantity = quantity,
                Price = product.ProductItem.Price,
                Discount = product.ProductItem.Discount,
            };

            product.Quantity -= quantity;

            this._context.Products.Update(product);
            this._context.Orders.Add(item);

            return _context.SaveChanges() != 0;
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
