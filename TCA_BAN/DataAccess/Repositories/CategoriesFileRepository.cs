using Common.interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesFileRepository
    {

    
        private string _path;
        public CategoriesFileRepository(string path)
        {

            _path = path;

            if (System.IO.File.Exists(path) == false)
            {
                using (var myFile = System.IO.File.Create(path))
                {
                    myFile.Close();
                }
            }

        }

        public void AddCategory(CategoryType category)
        {
            var list = GetCategories().ToList();
            list.Add(category);

            string myCategoriesJsonString = JsonSerializer.Serialize(list);


            System.IO.File.WriteAllText(_path, myCategoriesJsonString);

        }

        public void DeleteCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CategoryType> GetCategories()
        {
            string allCategoriesAsJson = "";
            using (var myFile = System.IO.File.OpenText(_path))
            {
                allCategoriesAsJson = myFile.ReadToEnd();
                myFile.Close();
            }

            if (string.IsNullOrEmpty(allCategoriesAsJson))
            {
                return new List<CategoryType>().AsQueryable();
            }
            else
            {

                List<CategoryType> allCategories = JsonSerializer.Deserialize<List<CategoryType>>(allCategoriesAsJson);
                return allCategories.AsQueryable();
            }

        }
    }
}