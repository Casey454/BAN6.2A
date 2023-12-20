using Data.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        public TicketDBRepository TK { get; set; }

        private ITicket _ticketRepository;
        private FlightDBRepository _flightRepository;
        private CountryRepository _countryRepository;


        public TicketsController(ITicket ticketRepository, FlightDBRepository flightRepository, CountryRepository countryRepository)
        {
            _flightRepository = flightRepository;
            _ticketRepository = ticketRepository;
            _countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var list = _flightRepository.GetFlights().ToList();

            var fixForCountries = _countryRepository.GetCountries().ToList();


            var result = from f in list
                         select new FlightViewModel
                         {
                             Rows = f.Rows,
                             Columns = f.Columns,
                             DepartureDate = f.DepartureDate,
                             ArrivalDate = f.ArrivalDate,
                             CountryFrom = fixForCountries.SingleOrDefault(x => x.Id.ToString() == f.CountryFromFK.ToString())?.Name,
                             CountryTo = fixForCountries.SingleOrDefault(x => x.Id.ToString() == f.CountryToFK.ToString())?.Name,
                             WholesalePrice = f.WholesalePrice,
                             CommissionRate = f.CommissionRate
                         };


            return View(Index);
        }
    

           //1. runs first and loads the page with empty fields to the user
        [HttpGet]
        public IActionResult Create()
        {
            FlightViewModel myModel = new FlightViewModel();
            //populate the Categories list from the db
            myModel.Countries = _countryRepository.GetCountries().ToList();

            return View(myModel);

        }




        [HttpPost]
        public IActionResult Create(CreateflightViewModel model, [FromServices] IWebHostEnvironment host)
        {

            
              _flightRepository.AddFlight(new Flight()
                {
                    CountryFromFK = model.CountryFromFk,
                    Rows = model.Rows,
                    Description = model.Description,
                    Price = model.Price,
                    WholesalePrice = model.WholesalePrice,
                    Stock = model.Stock,
                    Supplier = model.Supplier,
                    Image = relativePath
                });

                if (relativePath == "")
                {
                    TempData["message"] = "No Image was uploaded but product was saved successfully";
                }
                else TempData["message"] = "Product together with image was saved successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = "Product was not saved successfully";
                model.Categories = _flightRepository.GetCategories().ToList();
                return View(model);
            }

        }

        public IActionResult Details(Guid id)
        {
            var product = _productsRepository.GetProduct(id); //fetched the product from the db
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //
                ListProductsViewModel myProduct = new ListProductsViewModel()
                {
                    Category = product.Category.Name,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Id = product.Id
                      ,
                    Stock = product.Stock
                      ,
                    Image = product.Image
                };

                return View(myProduct);
            }
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                var product = _productsRepository.GetProduct(id);
                if (product == null) TempData["error"] = "product was not found";
                else
                    _productsRepository.DeleteProduct(id);

                TempData["message"] = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Product was not deleted. Check the input";

            }

            return RedirectToAction("Index");

            //note: return View("Index") >>> is going to open directly the html page
            //note: return RedirectToAction("Index") >>> is going to trigger the action
        }


        //to load the page with textboxes where the user can type in the new details
        //overwriting the old details...therefore we need to show the user also the old details
        public IActionResult Edit(Guid id)
        {

            var originalProduct = _productsRepository.GetProduct(id);

            //to pass details to/from the pages/views we use viewmodels

            EditProductViewModel myModel = new EditProductViewModel();
            myModel.Categories = _flightRepository.GetCategories().ToList();


            myModel.Supplier = originalProduct.Supplier;
            myModel.WholesalePrice = originalProduct.WholesalePrice;
            myModel.Price = originalProduct.Price;
            myModel.Name = originalProduct.Name;
            myModel.CategoryFK = originalProduct.CategoryFK;
            myModel.Description = originalProduct.Description;
            myModel.Stock = originalProduct.Stock;
            myModel.Image = originalProduct.Image;
            myModel.Id = originalProduct.Id; // don't forget this!!! because when the View
                                             // together with the edited is going to be resubmitted, 
                                             //we need the id again to identify which
                                             //product we have to update/overwrite

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel model, [FromServices] IWebHostEnvironment host)
        {
            //note: (benefit) we are using an existent instance of productsRepository and not creating a new one!
            try
            {
                //code which will handle file upload
                //1. save the phyiscal file
                string relativePath = "";
                string oldRelativePath = _productsRepository.GetProduct(model.Id).Image; //images/1eac8e0b-8786-4692-8a7c-eb898bc0eb1d.jpg
                if (model.ImageFile != null) //the user decided to overwrite image
                {
                    //1a. generation of a UNIQUE filename for our image
                    string newFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(model.ImageFile.FileName);

                    //1b. absolute path (where to save the file) e.g. C:\Users\attar\source\repos\EP2023_BAN2A\BANSolution\PresentationWebApp\wwwroot\images\nameOfTheFile.jpg
                    //IWebHostEnvironment
                    //esacape characters \" \r \n \t ....
                    string absolutePath = host.WebRootPath + "\\images\\" + newFilename;

                    //1c. relative path (to save into the db) e.g. \images\nameOfTheFile.jpg
                    relativePath = "/images/" + newFilename;

                    //1d. save the actual file using the absolute path

                    try
                    {
                        using (FileStream fs = new FileStream(absolutePath, FileMode.OpenOrCreate))
                        {
                            model.ImageFile.CopyTo(fs);
                            fs.Flush();
                        } //closing this bracket will close the filestream. if you don't close the filestream, you might get an error telling you that the File is being used by another process

                        //after the new image is saved, we can delete the old one

                        //get the old path and delete

                        var oldAbsolutePath = host.WebRootPath + "\\images\\" + System.IO.Path.GetFileName(oldRelativePath);

                        System.IO.File.Delete(oldAbsolutePath);

                    }
                    catch (Exception)
                    {
                        //log the error
                    }
                }
                else
                {
                    relativePath = oldRelativePath;
                }

                //2. set the path to be stored in the database
                _productsRepository.UpdateProduct(new Product()
                {
                    CategoryFK = model.CategoryFK,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    WholesalePrice = model.WholesalePrice,
                    Stock = model.Stock,
                    Supplier = model.Supplier,
                    Image = relativePath,
                    Id = model.Id //<<<<<<<<<< very important in the Update/edit context. it will help the code identify which product to edit
                });

                if (relativePath == "")
                {
                    TempData["message"] = "No Image was uploaded but product was updated successfully";
                }
                else TempData["message"] = "Product together with image was updated successfully";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = "Product was not saved successfully";
                model.Categories = _flightRepository.GetCategories().ToList();
                return View(model);
            }
        }
    }
}
