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
                Bill b when ((b.PayDate != null) && (b.PayDate <= DateTime.Today)) => Color.FromHex("C2FCC0"),
                Bill b when ((b.PayDate != null) && (b.PayDate > DateTime.Today)) => Color.FromHex("C0F6FC"),
                Bill b when (b.DueDate.Date < DateTime.Today.Date) => Color.FromHex("FCC5C2"),
                DateTime d => Color.Black,
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
