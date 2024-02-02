using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IDimensionService
    {
        List<Dimension> GetDimensions();
        Dimension GetDimensionById(int dimensionId);
        List<Product> GetProducsByDimension(int dimensionId);
    }
}
