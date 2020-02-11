using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookInventory.Models;

namespace BookInventory.Repository
{
    public class MockBookRepository : IBookRepository
    {
        private IList<Book> bookList;
        public MockBookRepository()
        {
            bookList = new List<Book> {
                new Book {
                    InventoryId = 0,
                    Author = "J.R.R. Tolkien",
                    Title = "The Silmarillion",
                    Price = 9.99m,
                    Isbn = "1234567890123",
                    Format = BookFormat.MassMarketPaperback,
                    Genre = BookGenre.Fantasy,
                    ImageUri = "Images/silmarillion.png"
                    },
                new Book {
                    InventoryId = 1,
                    Author = "Kaled Hossini",
                    Title = "The Kite Runner",
                    Price = 14.99m,
                    Isbn = "9876543210321",
                    Format = BookFormat.TradePaperback,
                    Genre = BookGenre.Literature,
                    ImageUri = "Images/kite_runner.png"
                    },
                new Book {
                    InventoryId = 2,
                    Author = "Orson Scott Card",
                    Title = "Ender's Game",
                    Price = 19.99m,
                    Isbn = "0123456789321",
                    Format = BookFormat.DigitalAudio,
                    Genre = BookGenre.SciFi,
                    ImageUri = "Images/enders_game.png"
                    },
                new Book {
                    InventoryId = 3,
                    Author = "Orson Scott Card",
                    Title = "Ender's Game",
                    Price = 1.99m,
                    Isbn = "3210123456789",
                    Format = BookFormat.DigitalText,
                    Genre = BookGenre.SciFi,
                    ImageUri = "Images/enders_game_digital.png"
                    },
                new Book {
                    InventoryId = 4,
                    Author = "J.K. Rowling",
                    Title = "Harry Potter and the Philosopher's stone",
                    Price = 9.99m,
                    Isbn = "9999999999999",
                    Format = BookFormat.MassMarketPaperback,
                    Genre = BookGenre.Fantasy,
                    ImageUri = "Images/harry_potter.png"
                    },
                new Book {
                    InventoryId = 5,
                    Author = "Robert Galbraith",
                    Title = "Harry Potter and the Philosopher's stone",
                    Price = 9.99m,
                    Isbn = "8888888888888",
                    Format = BookFormat.MassMarketPaperback,
                    Genre = BookGenre.Fantasy,
                    ImageUri = "Images/harry_potter.png"
                    }
            };
        }

        public IList<BookFormat> GetAllFormats()
        {
            return new List<BookFormat>
            {
                BookFormat.None,
                BookFormat.DigitalAudio,
                BookFormat.DigitalText,
                BookFormat.Hardcover,
                BookFormat.MassMarketPaperback,
                BookFormat.TradePaperback
            };
        }

        public IList<BookGenre> GetAllGenres()
        {
            return new List<BookGenre>
            {
                BookGenre.None,
                BookGenre.Fantasy,
                BookGenre.Geography,
                BookGenre.Literature,
                BookGenre.Mystery,
                BookGenre.Romance,
                BookGenre.SciFi
            };
        }

        public IList<Book> GetAllBooks()
        {
            return bookList;
        }

        public IList<Book> GetFilteredBooks(string author, string title, BookGenre? genre, BookFormat? format)
        {
            IList<Book> filteredBooks = new List<Book>(this.bookList);
            if(author != null)
            {
                filteredBooks = filterBookListByAuthor(filteredBooks, author);
            }
            if (title != null)
            {
                filteredBooks = filterBookListByTitle(filteredBooks, title);
            }
            if (genre != null && genre != BookGenre.None)
            {
                filteredBooks = filterBookListByGenre(filteredBooks, genre);
            }
            if (format != null && format != BookFormat.None)
            {
                filteredBooks = filterBookListByFormat(filteredBooks, format);
            }
            return filteredBooks;
        }
        private IList<Book> filterBookListByAuthor(IList<Book> books, string author)
        {
            var filteredBooks = new List<Book>();
            foreach(Book book in books)
            {
                if (author.Equals(book.Author))
                {
                    filteredBooks.Add(book);
                }
            }
            return filteredBooks;
        }
        private IList<Book> filterBookListByTitle(IList<Book> books, string title)
        {
            var filteredBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (title.Equals(book.Title))
                {
                    filteredBooks.Add(book);
                }
            }
            return filteredBooks;
        }
        private IList<Book> filterBookListByGenre(IList<Book> books, BookGenre? genre)
        {
            var filteredBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (genre.Value.Equals(book.Genre))
                {
                    filteredBooks.Add(book);
                }
            }
            return filteredBooks;
        }
        private IList<Book> filterBookListByFormat(IList<Book> books, BookFormat? format)
        {
            var filteredBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (format.Value.Equals(book.Format))
                {
                    filteredBooks.Add(book);
                }
            }
            return filteredBooks;
        }

        public IList<string> GetAllAuthors()
        {
            IList<string> authors = new List<string>();
            foreach(var book in this.bookList)
            {
                if (!authors.Contains(book.Author))
                {
                    authors.Add(book.Author);
                }
            }
            return authors;
        }

        public IList<string> GetAllTitles()
        {
            IList<string> titles = new List<string>();
            foreach (var book in this.bookList)
            {
                if (!titles.Contains(book.Title))
                {
                    titles.Add(book.Title);
                }
            }
            return titles;
        }
    }
}
