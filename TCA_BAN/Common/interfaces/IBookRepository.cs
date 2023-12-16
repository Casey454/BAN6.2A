using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.interfaces
{
    public interface ICategoryRepository
    {
        void AddBook(Book book);

        Book GetBook(string title, string author);

        IQueryable<Book> GetBooks();
    }
}
