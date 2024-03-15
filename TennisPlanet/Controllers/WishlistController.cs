using Microsoft.AspNetCore.Mvc;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Models.Wishlist;
using TennisPlanet.Core.Contracts;

namespace TennisPlanet.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IProductService _productService;

        public WishlistController(IWishlistService wishlistService, IProductService productService)
        {
            _wishlistService = wishlistService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var wishlistItems = _wishlistService.GetWishlistItems();
            var viewModel = new WishlistIndexVM
            {
                WishlistItems = wishlistItems.ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            var product = _productService.GetProductById(productId);
            _wishlistService.AddToWishlist(product);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int itemId)
        {
            _wishlistService.RemoveFromWishlist(itemId);
            return RedirectToAction("Index");
        }
    }
}
