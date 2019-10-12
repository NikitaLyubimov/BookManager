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
        private string _buttonContent;
        private AsyncCommand _searchCommand;
        private List<BookAuthor> _bookList;
        private BookManagerDbEntities _dbContext;
        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            _dbContext = new BookManagerDbEntities();

            BookList = _dbContext.BookAuthor.ToList();
            ButtonContent = "Search";
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
        /// Button content, which changes from 'Search' to 'Loading... while loading request data
        /// </summary>
        public string ButtonContent
        {
            get { return _buttonContent; }
            set { SetProperty(ref _buttonContent, value); }
        }

        /// <summary>
        /// Command for searching
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new AsyncCommand(Search);
                return _searchCommand;
            }
        }
        #endregion

        /// <summary>
        /// Method which search books depending on user request
        /// </summary>
        private async Task Search()
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Author) && string.IsNullOrEmpty(Subject))
                MessageBox.Show("At least one field should be entered");
            else
            {
                try
                {
                    ButtonContent = "Loading...";

                    await Task.Factory.StartNew(() => BookList = SearchBooksTools.Search(Title, Author, Subject));
                   
                }
                finally
                {
                    ButtonContent = "Search";
                }


            }
        }




    }
}
