using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace RealmSample.Converters
{
	public class IsListVisibleConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value as IList).Count > 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

