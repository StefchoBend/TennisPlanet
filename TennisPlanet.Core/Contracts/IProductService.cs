using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IProductService
    {
        bool Create(int productItemId, int dimensionId, int quantity);
        bool Update(int productId, int productItemId, int dimensionId, int quantity);

        List<Product> GetProducts();

        Product GetProductById(int productId);
        bool RemoveById(int productId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringBrandName);
    }
}
