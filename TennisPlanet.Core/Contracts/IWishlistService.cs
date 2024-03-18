using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Core.Contracts
{
    public interface IWishlistService
    {
        IEnumerable<WishlistItem> GetWishlistItems();
        void AddToWishlist(Product item);
        void RemoveFromWishlist(int itemId);
    }
}
