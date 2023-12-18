using Data.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FlightDBRepository
    {

        private AirlineDBContext _airlineDbContext; 

        public FlightDBRepository(AirlineDBContext airlineDbContext)
        {
            _airlineDbContext = airlineDbContext;
        }

       /* public IQueryable<Flight>GetFlight(int id)
        {

        }*/
        public IQueryable<Flight> GetFlights()
        {
            return _airlineDbContext.Flights;
        }
        public Flight GetFlight(int Id)
        {
            return _airlineDbContext.Flights.FirstOrDefault(f => f.Id == Id);
        }
    }
}
