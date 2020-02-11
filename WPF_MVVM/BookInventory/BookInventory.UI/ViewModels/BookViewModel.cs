using BookInventory.Models;
using BookInventory.Repository;
using BookInventory.UI.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace BookInventory.UI.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private Book blankBook;
        private readonly IBookRepository _bookRepository;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand filterBooksCommand { get; set; }
        public ICommand resetFilterCommand { get; set; }

        private Book _bookSelected { get; set; }
        public Book BookSelected {
            get { return _bookSelected; }
            set { _bookSelected = value;
                OnPropertyChanged("BookSelected");
            } }
        public BookGenre? GenreSelected { get; set; }
        public BookFormat? FormatSelected { get; set; }
        public string AuthorSelected { get; set; }
        public string TitleSelected { get; set; }

        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<BookGenre> genres { get; set; }
        public ObservableCollection<BookFormat> formats { get; set; }
        public ObservableCollection<string> authors { get; set; }
        public ObservableCollection<string> titles { get; set; }
        public bool filtered { get; set; }

        public BookViewModel()
        {
            _bookRepository = new MockBookRepository();
            books = new ObservableCollection<Book>();
            genres = new ObservableCollection<BookGenre>();
            formats = new ObservableCollection<BookFormat>();
            authors = new ObservableCollection<string>();
            titles = new ObservableCollection<string>();

            AuthorSelected = "Orson Scott Card";
            TitleSelected = null;
            GenreSelected = null;
            FormatSelected = null;

            filtered = false;
            
            filterBooksCommand = new RelayCommand<string>(FilterBooks, CanFilterBooks);
            resetFilterCommand = new RelayCommand<string>(ResetFilter, CanResetFilter);

            foreach (var genre in _bookRepository.GetAllGenres())
            {
                genres.Add(genre);
            }
            foreach (var format in _bookRepository.GetAllFormats())
            {
                formats.Add(format);
            }
            foreach (var author in _bookRepository.GetAllAuthors())
            {
                authors.Add(author);
            }
            foreach (var title in _bookRepository.GetAllTitles())
            {
                titles.Add(title);
            }
            foreach (var book in _bookRepository.GetAllBooks())
            {
                books.Add(book);
            }
            blankBook = new Book { InventoryId = -1, Title = "None Selected", Author = "", ImageUri = "Images/books.png", Price = 0.00m, Isbn = "", Format = BookFormat.None, Genre = BookGenre.None };
            BookSelected = blankBook;
        }

        public BookViewModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            if (_bookRepository == null) throw new ArgumentException();
            
            filterBooksCommand = new RelayCommand<string>(FilterBooks, CanFilterBooks);
        }

        private bool CanSelectBook(string obj)
        {
            return true;
        }

        private bool CanFilterBooks(string obj)
        {
            return true;
            //return (!filtered && (AuthorSelected != null || TitleSelected != null || GenreSelected != null || FormatSelected != null));
        }

        private void FilterBooks(string obj)
        {
            books.Clear();
            BookSelected = blankBook;
            var filterBooks = _bookRepository.GetFilteredBooks(AuthorSelected, TitleSelected, GenreSelected, FormatSelected);
            foreach(Book book in filterBooks)
            {
                books.Add(book);
            }
            filtered = true;
        }

        private bool CanResetFilter(string obj)
        {
            return true;
            //return filtered;
        }

        private void ResetFilter(string obj)
        {
            books.Clear();
            AuthorSelected = null;
            TitleSelected = null;
            GenreSelected = null;
            FormatSelected = null;
            BookSelected = blankBook;
            filtered = false;

            foreach (var book in _bookRepository.GetAllBooks())
            {
                books.Add(book);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
