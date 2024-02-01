using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandId);
        List<ProductItem> GetProductItemsByBrand(int brandId);
    }
}
