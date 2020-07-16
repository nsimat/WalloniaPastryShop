using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.Services
{
    public class ShoppingCart
    {
        private readonly WalloniaPastryShopDbContext _walloniaPastryShopDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(WalloniaPastryShopDbContext walloniaPastryShopDbContext)
        {
            _walloniaPastryShopDbContext = walloniaPastryShopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<WalloniaPastryShopDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddCart(Pie pie, int amount)
        {
            var shoppingCartItem = _walloniaPastryShopDbContext.ShoppingCartItems
                                                               .SingleOrDefault(s => s.Pie.PieId == pie.PieId 
                                                                                && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Pie = pie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = amount
                };

                _walloniaPastryShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _walloniaPastryShopDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _walloniaPastryShopDbContext.ShoppingCartItems
                                                               .SingleOrDefault(s => s.Pie.PieId == pie.PieId 
                                                                                && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _walloniaPastryShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _walloniaPastryShopDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _walloniaPastryShopDbContext.ShoppingCartItems
                                                                                         .Where(s => s.ShoppingCartId == ShoppingCartId)
                                                                                         .Include(s => s.Pie)
                                                                                         .ToList());
        }

        public void ClearCart()
        {
            var carItems = _walloniaPastryShopDbContext.ShoppingCartItems
                                                       .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _walloniaPastryShopDbContext.ShoppingCartItems.RemoveRange(carItems);

            _walloniaPastryShopDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _walloniaPastryShopDbContext.ShoppingCartItems
                                                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                                                    .Select(c => c.Amount * c.Pie.Price)
                                                    .Sum();

            return total;
        }
    }
}
