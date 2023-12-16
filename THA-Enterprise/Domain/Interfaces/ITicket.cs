using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicket
    {
        IQueryable<Product> GetProducts();
        Product? GetProduct(Guid id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(Guid id);

    }
}
