using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IProductItemService
    {
        bool Create(string itemName, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);
        bool Update(int productItemId, string itemName, int brandId, int categoryId, string picture, int quantity, decimal price, decimal discount);

        List<ProductItem> GetProductItems();

        ProductItem GetProductItemById(int productItemId);
        bool RemoveById(int productItemId);
        List<ProductItem> GetProductItems(string searchStringCategoryName, string searchStringBrandName);   
    }
}
