using System;
using System.Globalization;
using Xamarin.Forms;

namespace RealmSample.Converters
{
	public class EmptyStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrEmpty((value as string).Trim()) ? parameter : value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

