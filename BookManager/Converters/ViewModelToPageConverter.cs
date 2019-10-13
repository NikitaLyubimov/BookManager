using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using BookManager.ViewModels;
using BookManager.Views;

namespace BookManager.Converters
{
    class ViewModelToPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value.GetType() == typeof(MainPageViewModel))
                return new MainPage();
            else if (value.GetType() == typeof(BookDetailsViewModel))
            {
                BookDetailsPage bookDetails = new BookDetailsPage();
                bookDetails.DataContext = value as BookDetailsViewModel;
                return bookDetails;
            }  
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
