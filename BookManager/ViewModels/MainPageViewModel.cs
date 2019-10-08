using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


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
        private ObservableCollection<Book> _bookList;
        private BookManagerDbEntities _dbContext;
        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            _dbContext = new BookManagerDbEntities();
        }

        #endregion
        #region Public fields
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
        }

        public string Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value); }
        }

        public ObservableCollection<Book> BookList
        {
            get { return _bookList; }
            set { SetProperty(ref _bookList, value); }
        }
        #endregion

        private void Search()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Subject))
                MessageBox.Show("At least one field should be entered");
            else
            {
                if (string.IsNullOrEmpty(Author) && string.IsNullOrEmpty(Subject))

                    BookList = new ObservableCollection<Book>(_dbContext.Book.Where(x => x.Title.Contains(Title)).ToList());
                else if (string.IsNullOrEmpty(Subject))
                    BookList = new ObservableCollection<Book>(_dbContext.Book.Where(x => x.Title.Contains(Title) && x.).ToList());
            }
        }


    }
}
