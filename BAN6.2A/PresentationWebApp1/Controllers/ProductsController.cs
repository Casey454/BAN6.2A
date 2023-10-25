using DataAccess.DataContext.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using PresentationWebApp1.Models.ViewModels;

namespace PresentationWebApp1.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository _productsRepository;
        private CategoriesRepository _categoriesRepository;
        public ProductsController(ProductsRepository productsRepository, CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
        }
        public IActionResult Index()
        {
            var list = _productsRepository.GetProducts().OrderBy(x => x.Name).ToList();// there will be ONE database call 

            // transfer from Product>>>>>>>>>> ProductViewModel 

            var result = from p in list
                         select new ListProductsViewModel()
                         {
                             Name = p.Name,
                             Description = p.Description,
                             Id = p.Id,
                             Image = p.Image,
                             Price = p.Price,
                             Stock = p.Stock,
                             Category = p.Category.Name // using the navigational property
                         };
            return View(result);
        }

        public IActionResult Search(string keyword)
        {
            var list = _productsRepository.GetProducts()
                .Where(x => x.Name.StartsWith(keyword) || x.Description.Contains(keyword))
                .OrderBy(x => x.Name).ToList();// there will be ONE database call 

            // transfer from Product>>>>>>>>>> ProductViewModel 

            var result = from p in list
                         select new ListProductsViewModel()
                         {
                             Name = p.Name,
                             Description = p.Description,
                             Id = p.Id,
                             Image = p.Image,
                             Price = p.Price,
                             Stock = p.Stock,
                             Category = p.Category.Name // using the navigational property
                         };
            return View("Index", result);
        }

        //1. runs first and loads the page wiht empty fields to the user
        [HttpGet]
        public IActionResult Create()
        {
            CreateProductViewModel myModel = new CreateProductViewModel();
            //populate the categories list from the db

            myModel.Categories = _categoriesRepository.GetCategories().ToList();

            return View(myModel);


        }

        //.... user inputs the details

        //2. runs secondly with the parameters populated with the data...it saves into the db 

        public IActionResult Create(CreateProductViewModel model)
        {
            //code which will handle the file upload
            //1. save the physical file
            //2. set the path to be stored in th database

            //note:(benefit) we are using an existent instance of productsRepository and not creating a new one
            try
            {
                _productsRepository.AddProduct(new Product()
                {
                    CategoryFK = model.CategoryFK,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    WholesalePrice = model.WholesalePrice,
                    Stock = model.Stock,
                    Supplier = model.Supplier
                });

                TempData["message"] = "Product was saved successfully";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Product was not saved successfully";
                model.Categories = _categoriesRepository.GetCategories().ToList();
                return View(model);
            }

            return View();
        }

        public IActionResult Details(Guid id)
        {
            var product = _productsRepository.GetProduct(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ListProductsViewModel myProduct = new ListProductsViewModel()
                {
                    Category = product.Category.Name,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Id = product.Id,
                    Stock = product.Stock,
                    Image = product.Image,

                };

                return View(myProduct);
            }
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                _productsRepository.DeleteProduct(id);
                TempData["message"] = "Product deleted successfully";


            }

            catch (Exception ex)
            {
                TempData["error"] = "Product was not deleted. Check the input";
            }

            return RedirectToAction("Index");
            //note:return View("index" >>> is going to open directly the html page
            // note: return REdirectToAction ("Index")>>> is going to trigger the action 
        }


    }

}

    


