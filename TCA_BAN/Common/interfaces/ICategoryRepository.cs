using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.interfaces
{
    public interface ICategoryRepository
    {
        void AddCategory(string name);

        IEnumerable<CategoryType> GetCategories();

        void DeleteCategory();



    }
}
