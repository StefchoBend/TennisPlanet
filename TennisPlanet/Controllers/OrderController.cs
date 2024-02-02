using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Models.Order;

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
            var productItem = this._productService.GetProductById(bindingModel.ProductId);
            if (currentUserId == null || productItem == null || productItem.Quantity < bindingModel.CountOfProducts ||
               productItem.Quantity == 0)
            {
                return RedirectToAction("Denied", "Order");
            }
            if (ModelState.IsValid)
            {
                _orderService.Create(bindingModel.ProductId, currentUserId, bindingModel.CountOfProducts);
            }
            return this.RedirectToAction("Index", "Product");
        }
        // GET: OrderController/Denied
        public ActionResult Denied()  
        {
            return View();
        }
    }
}
