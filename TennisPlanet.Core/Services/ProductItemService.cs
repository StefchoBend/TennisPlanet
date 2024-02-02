using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Services
{
    public class ProductItemService : IProductItemService
    {
        private readonly ApplicationDbContext _context;
        public ProductItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string itemName, int brandId, int categoryId, string picture, decimal price, decimal discount)
        {
            ProductItem item = new ProductItem
            {
                ItemName = itemName,
                Brand = _context.Brands.Find(brandId),
                Category = _context.Categories.Find(categoryId),

                Picture = picture,
                Price = price,
                Discount = discount
            };
            _context.ProductItems.Add(item);
            return _context.SaveChanges() != 0;
        }

        public ProductItem GetProductItemById(int productItemId)
        {
            return _context.ProductItems.Find(productItemId);
        }

        public List<ProductItem> GetProductItems()
        {
            List<ProductItem> productItems = _context.ProductItems.ToList();
            return productItems;
        }

        public List<ProductItem> GetProductItems(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductItem> productItems = _context.ProductItems.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrandName))
            {
                productItems = productItems.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())
                && x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                productItems = productItems.Where(x => x.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrandName))
            {
                productItems = productItems.Where(x => x.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            return productItems;
        }

        public bool RemoveById(int productItemId)
        {
            var productItem = GetProductItemById(productItemId);
            if (productItem == default(ProductItem))
            {
                return false;
            }
            _context.Remove(productItem);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int productItemId, string itemName, int brandId, int categoryId, string picture, decimal price, decimal discount)
        {
            var productItem = GetProductItemById(productItemId);
            if (productItem == default(ProductItem))
            {
                return false;
            }
            productItem.ItemName = itemName;

            productItem.Brand = _context.Brands.Find(brandId);
            productItem.Category = _context.Categories.Find(categoryId);

            productItem.Picture = picture;
            productItem.Price = price;
            productItem.Discount = discount;
            _context.Update(productItem);
            return _context.SaveChanges() != 0;
        }
    }
}
