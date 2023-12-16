using Data.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TicketDBRepository:ITicket
    {
       
            private AirlineDBContext _airlineDBContext;
            public TicketDBRepository(AirlineDBContext airlineDBContext)
            {
                _airlineDBContext = airlineDBContext;
            }

            
            public IQueryable<Ticket> GetTickets()
            {
                return _airlineDBContext.Tickets;
            }

            public Ticket? GetTicket(int id)
            {
                return _airlineDBContext.Tickets.SingleOrDefault(x => x.Id == id);
            }

        public void BookTicket(Ticket ticket)
        {

            _airlineDBContext.Tickets.Add(ticket);
            _airlineDBContext.SaveChanges();


            var originalTicket = GetTicket(ticket.Id);
            if (originalTicket != null)
            {
                originalTicket.Rows = ticket.Rows;
                originalTicket.Columns = ticket.Columns;
                originalTicket.Passport = ticket.Passport;
                originalTicket.PricePaid = ticket.PricePaid;
                originalTicket.FlightIdFK = ticket.FlightIdFK;
                originalTicket.Cancelled = ticket.Cancelled;

                _airlineDBContext.SaveChanges();
            }

            bool IsSeatBooked(int row, int column)
            {

                return _airlineDBContext.Tickets.Any(t => t.Rows == row && t.Columns == column);
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

        public IQueryable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Product? GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
    }
}
}
