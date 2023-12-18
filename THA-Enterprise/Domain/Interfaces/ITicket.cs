using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicket
    {
        IQueryable<Ticket> GetTickets();
        Ticket? GetTicket(Guid id);
        void BookTicket(Ticket ticket);
        void CancelTicket(Ticket ticket);
                     
    }
}
