using DataAccess.NewFolder;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContext.Repositories
{
    public class CategoriesRepository
    {
        private ShoppingCartDbContext _shoppingCartDbContext;

        public CategoriesRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            _shoppingCartDbContext = shoppingCartDbContext;
        }

        public IQueryable <Category> GetCategories()
        {
            return _shoppingCartDbContext.Categories;
        }
    }
}
