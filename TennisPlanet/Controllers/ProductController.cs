﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Core.Services;
using TennisPlanet.Infrastructure.Data.Domain;
using TennisPlanet.Models.Brand;
using TennisPlanet.Models.Category;
using TennisPlanet.Models.Dimension;
using TennisPlanet.Models.Product;
using TennisPlanet.Models.ProductItem;

namespace TennisPlanet.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductItemService _productItemService;
        private readonly IDimensionService _dimensionService;
        private readonly IProductService _productService;
        // private readonly ICategoryService _categoryService;
        // private readonly IBrandService _brandService;

        public ProductController(IProductItemService productItemService, IDimensionService dimensionService, IProductService productService)
        {
            _productItemService = productItemService;
            _productService = productService;
            _dimensionService = dimensionService;
        }


        // GET: ProductController
        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringBrandName)
                .Select(products => new ProductIndexVM
                {
                    Id = products.Id,
                    ItemName = products.ProductItem.ItemName,
                    BrandId = products.ProductItem.BrandId,
                    BrandName = products.ProductItem.Brand.BrandName,
                    CategoryId = products.ProductItem.CategoryId,
                    CategoryName = products.ProductItem.Category.CategoryName,
                    Picture = products.ProductItem.Picture,
                    Description = products.ProductItem.Description,
                    Size = products.Dimension.Size,
                    QuantityInStock = products.QuantityInStock,
                    Price = products.ProductItem.Price,
                    Discount = products.ProductItem.Discount,
                }).ToList();
            return this.View(products);
        }

        // GET: ProductController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ItemName = item.ProductItem.ItemName,
                BrandId = item.ProductItem.BrandId,
                BrandName = item.ProductItem.Brand.BrandName,
                CategoryId = item.Id,
                CategoryName = item.ProductItem.Category.CategoryName,
                Size = item.Dimension.Size,
                Picture = item.ProductItem.Picture,
                Description = item.ProductItem.Description,
                QuantityInStock = item.QuantityInStock,
                Price = item.ProductItem.Price,
                Discount = item.ProductItem.Discount
            };
            return View(product);
        }

        // GET: ProductController/Create
        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var product = new ProductCreateVM();
            product.ProductItems = _productItemService.GetProductItems()
                .Select(x => new ProductItemPairVM()
                {
                    Id = x.Id,
                    ProductItemName = x.ItemName
                }).ToList();
            product.Dimensions = _dimensionService.GetDimensions()
                .Select(x => new DimensionPairVM()
                {
                    Id = x.Id,
                    Size = x.Size
                }).ToList();
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productService.Create(product.ProductItemId, product.DimensionId, product.Quantity);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ProductController/Edit/5
        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
               ProductItemId=product.ProductItemId,
               DimensionId=product.DimensionId,
               Quantity=product.QuantityInStock,
            };
            updatedProduct.ProductItems = _productItemService.GetProductItems()
                .Select(b => new ProductItemPairVM()
                {
                    Id = b.Id,
                    ProductItemName = b.ItemName
                }).ToList();
            updatedProduct.Dimensions = _dimensionService.GetDimensions()
                .Select(c => new DimensionPairVM()
                {
                    Id = c.Id,
                    Size = c.Size
                }).ToList();
            return View(updatedProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.Update(id, product.ProductItemId, product.DimensionId, product.Quantity);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(product);
        }


        // GET: ProductController/Delete/5
        [AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ItemName = item.ProductItem.ItemName,
                BrandId = item.ProductItem.BrandId,
                BrandName = item.ProductItem.Brand.BrandName,
                CategoryId = item.Id,
                CategoryName = item.ProductItem.Category.CategoryName,
                DimensionId = item.Dimension.Id,
                Size = item.Dimension.Size,
                Picture = item.ProductItem.Picture,
                Description = item.ProductItem.Description,
                Quantity = item.QuantityInStock,
                Price = item.ProductItem.Price,
                Discount = item.ProductItem.Discount
            };
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            var deleted = _productService.RemoveById(id);

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

// GET: ProductController/Edit/5
/*   [AllowAnonymous]
   [Authorize(Roles = "Administrator")]
   public ActionResult Edit(int id)
   {
       Product product = _productService.GetProductById(id);
       if (product == null)
       {
           return NotFound();
       }

       ProductEditVM updatedProduct = new ProductEditVM()
       {
           Id = product.Id,
           ProductItemId = product.ProductItemId,

           DimensionId = product.DimensionId,
           Quantity = product.QuantityInStock
       };
       updatedProduct.Dimensions = _dimensionService.GetDimensions()
          .Select(b => new DimensionPairVM()
           {
               Id = b.Id,
               Size = b.Size
           }).ToList();
     updatedProduct.ProductItems = _productItemService.GetProductItems()
           .Select(c => new ProductItemPairVM()
           {
               Id = c.Id,
               ProductItemName = c.ItemName,
           }).ToList();
       return View(updatedProduct);
   }

   // POST: ProductController/Edit/5
   [HttpPost]
   [ValidateAntiForgeryToken]
   [Authorize(Roles = "Administrator")]
   public ActionResult Edit(int id, ProductEditVM product)
   {
       if (ModelState.IsValid)
       {
           var createdId = _productService.Update(id, product.ProductItemId, product.DimensionId, product.Quantity); 
           if (createdId)
           {
               return RedirectToAction(nameof(Index));
           }
       }
       return View();
   }
*/