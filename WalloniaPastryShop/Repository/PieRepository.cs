using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.Repository
{
    public class PieRepository : IPieRepository
    {
        private readonly WalloniaPastryShopDbContext _walloniaPastryShopDbContext;

        public PieRepository(WalloniaPastryShopDbContext walloniaPastryShopDbContext)
        {
            _walloniaPastryShopDbContext = walloniaPastryShopDbContext;
        }
        public IEnumerable<Pie> AllPies 
        {
            get
            {
                return _walloniaPastryShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _walloniaPastryShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _walloniaPastryShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
