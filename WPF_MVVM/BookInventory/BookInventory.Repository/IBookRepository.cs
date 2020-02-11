using BookInventory;
using BookInventory.Models;
using System.Collections.Generic;

namespace BookInventory.Repository
{
    public interface IBookRepository
    {
        IList<BookGenre> GetAllGenres();
        IList<BookFormat> GetAllFormats();
        IList<Book> GetAllBooks();
        IList<string> GetAllAuthors();
        IList<string> GetAllTitles();
        IList<Book> GetFilteredBooks(string author, string title, BookGenre? genre, BookFormat? format);
    }
}
