using DataAccess.NewFolder;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContext.Repositories
{

    //note: the role of repository classes will be fetch or write data from or the the databse directly
    //      no need to code complex logic here 
    //      just the 4 CRUD ((creation, reading, updating, deleting)operations)

    // note2: Dependency Injection 
    //to make a call to use an instance: constructor injection vs Method Injection 
    /*
     * Checkout
     * 
     * 1. Check wheter i have enough products ins tock. ProductsRepository-> GetProducts(int id)
     * 2.Charge the client's visa. Payment Repostory-> ChargeVise(int userid)
     * 3. Deduct the qty from the stock. ProductsRepository-> UpdateProduct(id, newstock)
     * 4. Place an Order in the Orders table. OrdersRepository-> CreateOrder()
     * 5.Place the subsequent OrderDetails in the OrderDetails table-> CreateOrderDetails(..))
     * ....
     * 
     * in each of the repository classes(3), you will make a different call to the dame class to create different 
     * objects ShoppingCartDbContext()...

     */
    public class ProductsRepository
    {
        private ShoppingCartDbContext _shoppingCartDbContext;

        public ProductsRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            _shoppingCartDbContext = shoppingCartDbContext;
        }


        /*
         * IQueryable vs List 
          
          1. (disadv) it doesn't allow you to debug what's inside it
          2. (Adv) it doesn't open a connection to the database until a ToList() is appplied 
         
        IQuearyable prepares the stament that nees to be executed at some point 

        // var myProducts=GetProducts().Where(x=>x.Name.Contains(variable)).Skip(x).Take(y).OrderBy(x=>.Name); 

        //Select * From Products Where Product.Name like %Sam% order by Product. Name asc
         */
        public IQueryable<Product> GetProducts()
        {
            return _shoppingCartDbContext.Products;
        }

        public Product GetProduct(Guid id)
        {
            return _shoppingCartDbContext.Products.SingleOrDefault(x => x.Id == id);
        }
        public void AddProduct(Product product)
        {
            _shoppingCartDbContext.Products.Add(product);
            _shoppingCartDbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var originalProduct= GetProduct(product.Id);
            if(originalProduct != null)
            {
                originalProduct.Supplier=product.Supplier;
                originalProduct.Name=product.Name;
                originalProduct.Description=product.Description;
                originalProduct.WholesalePrice=product.WholesalePrice;
                originalProduct.Price=product.Price;
                originalProduct.CategoryFK=product.CategoryFK;
                originalProduct.Stock=product.Stock;
                originalProduct.Image=product.Image;
                _shoppingCartDbContext.SaveChanges();
            }
        }

        public void DeleteProduct(Guid id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                _shoppingCartDbContext.Products.Remove(product);

                _shoppingCartDbContext.SaveChanges();
            }
            else throw new Exception("Product not found");

        }

    }
}
        
        
       
    



