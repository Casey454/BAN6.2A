using Data.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
     public class CountryRepository
    {

        private AirlineDBContext _airlineDBContext;

            public CountryRepository(AirlineDBContext airlineDBContext)
            {
                _airlineDBContext= airlineDBContext;
            }

            public IQueryable<Country> GetCountries()
            {
                return _airlineDBContext.Countries;
            }
        }
    }

