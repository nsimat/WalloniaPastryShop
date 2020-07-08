using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalloniaPastryShop.Models;

namespace WalloniaPastryShop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WalloniaPastryShopDbContext _walloniaPastryShopDbContext;

        public CategoryRepository(WalloniaPastryShopDbContext walloniaPastryShopDbContext)
        {
            _walloniaPastryShopDbContext = walloniaPastryShopDbContext;
        }
        public IEnumerable<Category> AllCategories => _walloniaPastryShopDbContext.Categories;
    }
}
