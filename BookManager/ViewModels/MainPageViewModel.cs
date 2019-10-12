using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookManager.Models;

namespace BookManager.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        #region Private fields
        private string _title;
        private string _author;
        private string _subject;
        private RelayCommand _searchCommand;
        private List<BookAuthor> _bookList;
        private BookManagerDbEntities _dbContext;
        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            _dbContext = new BookManagerDbEntities();

            BookList = _dbContext.BookAuthor.ToList();
        }

        #endregion
        #region Public fields
        /// <summary>
        /// Field fore searching book
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Field for searching book
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
        }

        /// <summary>
        /// Field for searching book
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value); }
        }

        /// <summary>
        /// List with books 
        /// </summary>
        public List<BookAuthor> BookList
        {
            get { return _bookList; }
            set { SetProperty(ref _bookList, value); }
        }

        /// <summary>
        /// Command for searching
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new RelayCommand(Search);
                return _searchCommand;
            }
        }
        #endregion

        /// <summary>
        /// Method which search books depending on user request
        /// </summary>
        private void Search()
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Author) && string.IsNullOrEmpty(Subject))
                MessageBox.Show("At least one field should be entered");
            else
            {
                if (string.IsNullOrEmpty(Author) && string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Title))
                    BookList = _dbContext.BookAuthor.Where(x => x.Book.Title.Contains(Title)).ToList();

                else if (string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Title))
                    BookList = _dbContext.BookAuthor.Where(x => x.Book.Title.Contains(Title) && x.Author.Name.Contains(Author)).ToList();
                else if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Author))
                {
                    BookList = _dbContext.BookSubject.Where(x => x.Book.Title.Contains(Title) && x.Subject.Contains(Subject))
                        .Select(x => x.Book)
                        .Select(x => x.BookAuthor) as List<BookAuthor>;
                }
                else if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Author))
                {
                    BookList = _dbContext.BookSubject.Where(x => x.Book.Title.Contains(Title) && x.Subject.Contains(Subject)
                    && x.Book.BookAuthor.FirstOrDefault(y => y.Author.Name.Contains(Author)) != null)
                        .Select(x => x.Book)
                        .Select(x => x.BookAuthor) as List<BookAuthor>;
                }
                else if (!string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Title))
                {
                    BookList = _dbContext.BookSubject.Where(x => x.Book.BookAuthor.FirstOrDefault(y => y.Author.Name.Contains(Author)) != null && x.Subject.Contains(Subject))
                        .Select(x => x.Book)
                        .Select(x => x.BookAuthor) as List<BookAuthor>;
                }


            }
        }




    }
}
