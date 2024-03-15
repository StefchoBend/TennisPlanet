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
    public class WishlistService : IWishlistService
    {
        private List<WishlistItem> _wishlistItems = new List<WishlistItem>();

        public IEnumerable<WishlistItem> GetWishlistItems()
        {
            return _wishlistItems;
        }

        public void AddToWishlist(Product item)
        {
            WishlistItem wishlistItem = new WishlistItem() 
            { 
            Id = item.Id,
            Name = item.ProductItem.ItemName,
            Size = item.Dimension.Size,
            Picture = item.ProductItem.Picture,
            };

            _wishlistItems.Add(wishlistItem);   
        }

        public void RemoveFromWishlist(int itemId)
        {
            var itemToRemove = _wishlistItems.Find(item => item.Id == itemId);
            if (itemToRemove != null)
            {
                _wishlistItems.Remove(itemToRemove);
            }
        }
    }
}
