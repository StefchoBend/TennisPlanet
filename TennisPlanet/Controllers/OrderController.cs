using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Models.Order;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductItemService _productItemService;
        private readonly IOrderService _orderService;

        public OrderController(IProductService productService, IOrderService orderService, IProductItemService productItemService)
        {
            _productService = productService;
            _orderService = orderService;
            _productItemService = productItemService;
        }

        //GET: OrderController
        [Authorize(Roles ="Administrator")]
        public ActionResult Index()
        {
             List<OrderIndexVM> orders = _orderService.GetOrders()
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductItem.ItemName,
                    Picture = x.Product.ProductItem.Picture,
                    Description = x.Product.ProductItem.Description,
                    Size = x.Product.Dimension.Size,
                    CountOfProducts = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,
                }).ToList();
            return View(orders);
        }

        // GET: OrderController/Create
        public ActionResult Create(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            OrderCreateVM order = new OrderCreateVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductItem.ItemName,
                QuantityInStock = product.QuantityInStock,
                Description = product.ProductItem.Description,
                Size = product.Dimension.Size,
                Price = product.ProductItem.Price,
                Discount = product.ProductItem.Discount,
                Picture = product.ProductItem.Picture,
            };
            return View(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = this._productService.GetProductById(bindingModel.ProductId);
            if (currentUserId == null || product == null || product.QuantityInStock < bindingModel.CountOfProducts ||
               product.QuantityInStock == 0)
            {
                return RedirectToAction("Denied", "Order");
            }
            if (ModelState.IsValid)
            {
                _orderService.Create(bindingModel.ProductId, currentUserId, bindingModel.CountOfProducts);
            }
            return this.RedirectToAction("Index", "Product");
        }
        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId)
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductItem.ItemName,
                    Picture = x.Product.ProductItem.Picture,
                    Description = x.Product.ProductItem.Description,
                    Size = x.Product.Dimension.Size,
                    CountOfProducts = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,
                }).ToList();
            return View(orders);
        }

        // GET: OrderController/Denied
        public ActionResult Denied()  
        {
            return View();
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            Order item = _orderService.GetOrderById(id);
            if (item == null)
            {
                return NotFound();
            }
            OrderDeleteVM order = new OrderDeleteVM()
            {
                Id = item.Id,
                ItemName = item.Product.ProductItem.ItemName,
                Size = item.Product.Dimension.Size,
                CountOfProducts = item.Quantity,
                Picture = item.Product.ProductItem.Picture,
                Description = item.Product.ProductItem.Description,
                Price = item.Price,
                Discount = item.Discount,
                TotalPrice = item.TotalPrice,
            };
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _orderService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
