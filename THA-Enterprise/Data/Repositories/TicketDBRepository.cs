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



            bool IsSeatBooked(int flightId, int row, int column, out string bookingInfo)
            {
                var isBooked = _airlineDBContext.Tickets.Any(t => t.Id == flightId && t.Rows == row && t.Columns == column);

                if (isBooked)
                {
                    
                    var bookedTicket = _airlineDBContext.Tickets.FirstOrDefault(t => t.Id == flightId && t.Rows == row && t.Columns == column);
                    bookingInfo = $"Seat ({row}, {column}) is booked.";
                }
                else
                {
                    
                    bookingInfo = $"Seat ({row}, {column}) is available.";
                }

                return isBooked;
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
