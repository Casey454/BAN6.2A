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

       
    

        public Ticket? GetTicket(int id)
        {
            return _airlineDBContext.Tickets.SingleOrDefault(x => x.Id == id);
        }

        public void BookTicket(Ticket ticket)
        {

            _airlineDBContext.Tickets.Add(ticket);
            _airlineDBContext.SaveChanges();



            bool IsSeatBooked(int row, int column)
            {

                return _airlineDBContext.Tickets.Any(t => t.Rows == row && t.Columns == column);
            }
        }

        public void CancelTicket(Ticket ticket)
        {

            var originalTicket = GetTicket(ticket.Id);
            if (originalTicket != null)
            {
                originalTicket.Rows = ticket.Rows;
                originalTicket.Columns = ticket.Columns;
                originalTicket.Passport = ticket.Passport;
                originalTicket.PricePaid = ticket.PricePaid;
                originalTicket.FlightIdFK = ticket.FlightIdFK;
                ticket.Cancelled = true;

                _airlineDBContext.SaveChanges();
            }
        }


        public IQueryable<Ticket> GetTickets()
        {
            return _airlineDBContext.Tickets;
        }

        public Ticket? GetTicket(Guid id)
        {
            throw new NotImplementedException();
        }
    }
    
}
