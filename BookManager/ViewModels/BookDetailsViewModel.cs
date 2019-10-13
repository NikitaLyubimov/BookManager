using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookManager.Models;

namespace BookManager.ViewModels
{
    class BookDetailsViewModel : BaseViewModel
    {
        #region Private fields

        private BookAuthor _book;

        private string _title;
        private string _autor;
        private string _firstPublished;
        private string _description;
        private string _subjects;

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
        public BookDetailsViewModel(BookAuthor book)
        {
            _book = book;
            Subjects = string.Empty;
            SetInfo();
        }
        #endregion


        #region Help methods

        private void SetInfo()
        {
            Title = _book.Book.Title;
            Author = _book.Author.Name;
            FirstPublished = _book.Book.FirstPublishDate;
            Description = _book.Book.Description;

            foreach(BookSubject subject in _book.Book.BookSubject)
            {
                Subjects += $"{subject.Subject}, ";  
            }

        }

        #endregion
    }
}
