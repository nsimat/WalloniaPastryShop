using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.Repository
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
