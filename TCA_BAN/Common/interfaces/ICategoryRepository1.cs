using Common.Models;

namespace Common.interfaces
{
    public interface ICategoryRepository1
    {
        void AddBook(Book book);
        void AddCategory(string name);
        void DeleteCategory();
        Book GetBook(string title, string author);
        IQueryable<Book> GetBooks();
        IEnumerable<CategoryType> GetCategories();
    }
}