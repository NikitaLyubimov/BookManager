using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookManager.Models;

namespace BookManager.ViewModels
{
    class AuthorDetailsViewModel : BaseViewModel
    {
        #region Private fields
        private Visibility _comboboxVisibiliity;
        private Visibility _textVisibility;
        private string _birthDate;
        private string _deathDate;
        private string _bio;
        private string _wikipedeaLink;
        private string _authorName;
        private int _selectedAuthorIndex;
        private Window _currentWindow;
        private List<BookAuthor> _books;
        private RelayCommand _wikipediaCommand;
        private RelayCommand _closeCommand;
        #endregion

        #region Public fields

        public Visibility ComboboxVisibility
        {
            get { return _comboboxVisibiliity; }
            set { SetProperty(ref _comboboxVisibiliity, value); }
        }

        public Visibility TextVisibility
        {
            get { return _textVisibility; }
            set { SetProperty(ref _textVisibility, value); }
        }

        public string AuthorName
        {
            get { return _authorName; }
            set { SetProperty(ref _authorName, value); }
        }


        public string BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }
        }


        public string DeathDate
        {
            get { return _deathDate; }
            set { SetProperty(ref _deathDate, value); }
        }


        public string Bio
        {
            get { return _bio; }
            set { SetProperty(ref _bio, value); }
        }

        public List<BookAuthor> Books
        {
            get { return _books; }
            set { SetProperty(ref _books, value); }
        }

        public int SelectedAuthorIndex
        {
            get { return _selectedAuthorIndex; }
            set
            {
                SetProperty(ref _selectedAuthorIndex, value);

                OnIndexChanged();

            }
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(CloseAction);

                return _closeCommand;
            }
        }

        public ICommand WikipediaCommand
        {
            get
            {
                if (_wikipediaCommand == null)
                    _wikipediaCommand = new RelayCommand(WikipediaAction);

                return _wikipediaCommand;
            }
        }


        #endregion


        public AuthorDetailsViewModel(Window current, List<BookAuthor> books)
        {
            _books = books;
            _currentWindow = current;

            SetContent();

            
        }

        private void CloseAction()
        {
            _currentWindow.Close();
        }

        private void WikipediaAction()
        {
            if (_wikipedeaLink == null)
                return;
            Process.Start(_wikipedeaLink);
        }


        private void SetContent()
        {
            if (_books.Count() > 1)
            {
                TextVisibility = Visibility.Collapsed;
                BirthDate = $"{_books[0].Author.BirthDate} - ";
                DeathDate = _books[0].Author.DeathDate;
                
            }
                

            else
            {
                ComboboxVisibility = Visibility.Collapsed;
                AuthorName = _books[0].Author.Name;
            }

            Bio = _books[0].Author.Bio;
            _wikipedeaLink = _books[0].Author.Wikipedia;


        }

        private void AuthorChangedContent()
        {
            BirthDate = $"{_books[_selectedAuthorIndex].Author.BirthDate} - ";
            DeathDate = _books[_selectedAuthorIndex].Author.DeathDate;
            Bio = _books[_selectedAuthorIndex].Author.Bio;
            _wikipedeaLink = _books[_selectedAuthorIndex].Author.Wikipedia;
        }

        public event PropertyChangedEventHandler IndexChanged;

        protected void OnIndexChanged([CallerMemberName] string caller = "")
        {
            AuthorChangedContent();
            IndexChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

    }
}
