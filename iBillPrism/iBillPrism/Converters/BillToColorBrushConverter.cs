using iBillPrism.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace iBillPrism.Converters
{
    public class BillToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {          
            Color bgcolor = value switch
            {
                // <type> <optional name> <optional when> (condition) => return type
                Bill b when (b.PayDate == DateTime.Today) => Color.Green,
                Bill b when (b.DueDate.Date == DateTime.Today.Date) => Color.LightYellow,
                _ => Color.Transparent
            };
            return bgcolor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
