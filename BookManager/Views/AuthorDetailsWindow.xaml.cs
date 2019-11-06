using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BookManager.Models;
using BookManager.ViewModels;

namespace BookManager.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorDetailsWindow.xaml
    /// </summary>
    public partial class AuthorDetailsWindow : Window
    {
        public AuthorDetailsWindow(List<BookAuthor> books)
        {
            InitializeComponent();

            this.DataContext = new AuthorDetailsViewModel(this, books);
        }
    }
}
