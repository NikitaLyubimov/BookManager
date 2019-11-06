using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BookManager.Views;
using BookManager.Models;

namespace BookManager.ViewModels
{
    class BookDetailsViewModel : BaseViewModel
    {
        #region Private fields
        
        private List<BookAuthor> _books;

        private string _title;
        private string _autor;
        private string _firstPublished;
        private string _description;
        private string _subjects;

        private RelayCommand _authorCommand;

        #endregion

        #region Public fields

        /// <summary>
        /// Title of the book
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Author of the book
        /// </summary>
        public string Author
        {
            get { return _autor; }
            set { SetProperty(ref _autor, value); }
        }

        /// <summary>
        /// First publish date of the book
        /// </summary>
        public string FirstPublished
        {
            get { return _firstPublished; }
            set { SetProperty(ref _firstPublished, value); }
        }

        /// <summary>
        /// Description of the book
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// Subjects of the book
        /// </summary>
        public string Subjects
        {
            get { return _subjects; }
            set { SetProperty(ref _subjects, value); }
        }

        #endregion

        #region Constructor
        public BookDetailsViewModel(List<BookAuthor> books)
        {
            _books = books;
            Subjects = string.Empty;
            SetInfo();
        }

        public ICommand AuthorCommand
        {

            get
            {
                if (_authorCommand == null)
                    _authorCommand = new RelayCommand(AuthorAction);

                return _authorCommand;
            }
        }
        #endregion

        private void AuthorAction()
        {
            AuthorDetailsWindow authorDetails = new AuthorDetailsWindow(_books);
            authorDetails.ShowDialog();
        }

        #region Help methods

        private void SetInfo()
        {

            if (_books.Count() > 1)
                Author = "More about authors";
            else
                Author = _books[0].Author.Name;

            Title = _books[0].Book.Title;
            FirstPublished = _books[0].Book.FirstPublishDate;
            Description = _books[0].Book.Description;

            foreach(BookSubject subject in _books[0].Book.BookSubject)
            {
                Subjects += $"{subject.Subject}, ";  
            }

        }

        #endregion
    }
}
