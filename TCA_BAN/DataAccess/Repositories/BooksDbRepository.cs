using Common.interfaces;
using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataAccess.Repositories
{
    public class BooksDbRepository : ICategoryRepository
    {
        private LibraryDbContext _db;
        public BooksDbRepository(LibraryDbContext db)
        {
            _db = db;
        }
        public BooksDbRepository() { }


        private List<Book> books;

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public Book GetBook(string title, string author)
        {
            foreach (Book book in books)
            {
                if (book.Title == title && book.Author == author)
                {
                    return book;
                }
            }

            return null;
        }

        public IQueryable<Book> GetBooks()
        {
            return books.AsQueryable();
        }
    }
}



