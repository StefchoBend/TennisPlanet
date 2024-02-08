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
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(int productItemId, int dimensionId, int quantity)
        {
            Product item = new Product
            {
                ProductItemId = productItemId,
                DimensionId = dimensionId,
                QuantityInStock = quantity
            };
            _context.Products.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName)
        {
            List<Product> products = _context.Products.ToList();
            if (!String.IsNullOrEmpty(searchStringCategoryName) && !String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(x => x.ProductItem.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())
                && x.ProductItem.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringCategoryName))
            {
                products = products.Where(x => x.ProductItem.Category.CategoryName.ToLower().Contains(searchStringCategoryName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBrandName))
            {
                products = products.Where(x => x.ProductItem.Brand.BrandName.ToLower().Contains(searchStringBrandName.ToLower())).ToList();
            }
            return products;
        }

        public bool RemoveById(int productId)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int productId, int productItemId, int dimensionId, int quantity)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            {
                return false;
            }
            product.Id = productId;
            product.ProductItemId = productItemId;
            product.DimensionId = dimensionId;
            product.QuantityInStock = quantity;
            return _context.SaveChanges() != 0;
        }
    }
}
