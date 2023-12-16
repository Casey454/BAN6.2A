using Common.interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesDbRepository:ICategoryRepository
    {
        private LibraryDbContext _libraryDbContext;
        public CategoriesDbRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public class CategoryDb
        {
            private List<CategoryType> categories;

           
            public CategoryDb()
            {
                categories = new List<CategoryType>();
            }

            public void AddCategory(string name)
            {
                
                if (categories.Exists(c => c.Name == name))
                {
                    throw new ArgumentException("Category name already exists.");
                }

                CategoryType category = new CategoryType(Name);
                categories.Add(category);
            }

            public IEnumerable<CategoryType> GetCategories()
            {
               
                return categories.OrderBy(c => c.Name);
            }

            
            public void DeleteCategory()
            {
                if (Book.Count >0)
                {
                    throw new InvalidOperationException("Cannot delete category with attached products.");
                }

            }
        }
    }
}
