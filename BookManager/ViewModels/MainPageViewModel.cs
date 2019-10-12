using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool _isSearchEnabled;
        private bool _isLeftBookEnabled;
        private bool _isRightBookEnabled;
        private int _startIndex;

        private AsyncCommand _searchCommand;
        private AsyncCommand _rightBookCommand;
        private AsyncCommand _leftBookCommand;
        private List<BookAuthor> _bookList;
        private List<BookAuthor> _fiftyBookList;
        private BookManagerDbEntities _dbContext;
        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            _dbContext = new BookManagerDbEntities();
            _startIndex = 0;

            _bookList = _dbContext.BookAuthor.ToList();
            SetBookRightList();
            ButtonContent = "Search";
            IsSearchEnabled = true;

            
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
        public List<BookAuthor> FiftyBookList
        {
            get { return _fiftyBookList; }
            set { SetProperty(ref _fiftyBookList, value); }
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
        /// Is search button enabled
        /// </summary>
        public bool IsSearchEnabled
        {
            get{ return _isSearchEnabled; }
            set { SetProperty(ref _isSearchEnabled, value); }
        }

        /// <summary>
        /// Is left button for sliding books is enabled
        /// </summary>
        public bool IsRightBookEnabled
        {
            get { return _isRightBookEnabled; }
            set { SetProperty(ref _isRightBookEnabled, value); }
        }

        /// <summary>
        /// Is right button for sliding books is enabled
        /// </summary>
        public bool IsLeftBookEnabled
        {
            get { return _isLeftBookEnabled; }
            set { SetProperty(ref _isLeftBookEnabled, value); }
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

        /// <summary>
        /// Command for sliding to the right
        /// </summary>
        public ICommand RightBookCommand
        {
            get
            {
                if (_rightBookCommand == null)
                    _rightBookCommand = new AsyncCommand(RightBooks);
                return _rightBookCommand;
            }
        }

        /// <summary>
        /// Command for sliding to the left
        /// </summary>
        public ICommand LeftBookCommand
        {
            get
            {
                if (_leftBookCommand == null)
                    _leftBookCommand = new AsyncCommand(LeftBooks);

                return _leftBookCommand;
            }
        }
        #endregion

        #region Click Methods
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
                    IsSearchEnabled = false;

                    await Task.Factory.StartNew(() => 
                    {
                        _bookList = SearchBooksTools.Search(Title, Author, Subject);
                        _startIndex = 0;
                        SetBookRightList();
                    });
                   
                }
                finally
                {
                    ButtonContent = "Search";
                    IsSearchEnabled = true;
                }

            }
        }

        /// <summary>
        /// Invokes when you click on left arrow
        /// </summary>
        /// <returns></returns>
        private async Task LeftBooks()
        {
            await Task.Factory.StartNew(() =>
            {
                SetBookLeftList();
            });
        }

        /// <summary>
        /// Invokes when you click on right arrow
        /// </summary>
        /// <returns></returns>
        private async Task RightBooks()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {

                    SetBookRightList();
                });
            }
            catch (IndexOutOfRangeException exc)
            {
                await Task.Factory.StartNew(() => FiftyBookList = _bookList.GetRange(_startIndex, _bookList.Count - FiftyBookList.Count));
            }
        }

        #endregion


        /// <summary>
        /// Sets books while you sliding to the right
        /// </summary>
        private void SetBookRightList()
        {
            if (_bookList.Count > 50)
            {
                if(_startIndex + 50 > _bookList.Count)
                {
                    FiftyBookList = _bookList.GetRange(_startIndex, _bookList.Count - _startIndex);
                    SetButtons();
                }
                else if(_startIndex == 0)
                {
                    
                    FiftyBookList = _bookList.GetRange(_startIndex, 50);
                    SetButtons();
                    _startIndex += 50;
                }
                else
                {
                    _startIndex = _startIndex == 0 ? _startIndex += 50 : _startIndex;
                    
                    FiftyBookList = _bookList.GetRange(_startIndex, 50);
                    _startIndex += 50;
                    SetButtons();
                }

            }
            else
            {
                FiftyBookList = _bookList;
                SetButtons();
            }
                
        }

        /// <summary>
        /// Sets books while you sliding to the left
        /// </summary>
        private void SetBookLeftList()
        {
            if(_startIndex - 100 < 0)
            {
                _startIndex -= 50;
                
                FiftyBookList = _bookList.GetRange(_startIndex, 50);
                SetButtons();

                _startIndex = _startIndex == 0 ? _startIndex += 50 : _startIndex;
            }
            else
            {
                _startIndex -= 100;

                FiftyBookList = _bookList.GetRange(_startIndex, 50);
                SetButtons();

                _startIndex = _startIndex == 0 ? _startIndex += 50 : _startIndex;

            }
        }

        /// <summary>
        /// Enable or disable buttons
        /// </summary>
        private void SetButtons()
        {
            IsRightBookEnabled = _startIndex + 50 > _bookList.Count || _startIndex == _bookList.Count ? false : true;
            IsLeftBookEnabled = _startIndex - 50 < 0 ? false : true;
        }

    }
}
