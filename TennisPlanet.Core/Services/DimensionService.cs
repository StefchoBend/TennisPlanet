using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Infrastructure.Data;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Services
{
    public class DimensionService : IDimensionService
    {
        private readonly ApplicationDbContext _context;
        public DimensionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Dimension GetDimensionById(int dimensionId)
        {
            return _context.Dimensions.Find(dimensionId);
        }

        public List<Dimension> GetDimensions()
        {
            List<Dimension> dimensions = _context.Dimensions.ToList();
            return dimensions;
        }

        public List<Product> GetProducsByDimension(int dimensionId)
        {
            return _context.Products
               .Where(x => x.DimensionId == dimensionId)
               .ToList();
        }
    }
}
