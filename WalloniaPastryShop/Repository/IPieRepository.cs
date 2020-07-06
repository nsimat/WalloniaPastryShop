using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.Repository
{
    public interface IPieRepository
    {
        public IEnumerable<Pie> AllPies { get; }
        public IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
    }
}
