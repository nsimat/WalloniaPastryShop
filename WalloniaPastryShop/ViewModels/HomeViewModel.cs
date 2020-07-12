using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
