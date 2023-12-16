using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class LibraryDbContext:DbContext
    { 
     
            public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
               : base(options)
            {
            }

            public DbSet<Book> Books { get; set; }
            public DbSet<CategoryType> Categories { get; set; }

        }
    }
