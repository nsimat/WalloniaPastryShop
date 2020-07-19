using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;
using WalloniaPastryShop.Services;

namespace WalloniaPastryShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WalloniaPastryShopDbContext _walloniaPastryShopDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(WalloniaPastryShopDbContext walloniaPastryShopDbContext, ShoppingCart shoppingCart)
        {
            _walloniaPastryShopDbContext = walloniaPastryShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _walloniaPastryShopDbContext.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach(var shoppingItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingItem.Amount,
                    OrderId = order.OrderId,
                    PieId = shoppingItem.Pie.PieId,
                    Price = shoppingItem.Pie.Price
                };

                _walloniaPastryShopDbContext.OrderDetails.Add(orderDetail);
            }

            _walloniaPastryShopDbContext.SaveChanges();
        }
    }
}
