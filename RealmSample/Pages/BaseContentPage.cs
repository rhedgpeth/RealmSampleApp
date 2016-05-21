using System;
using Xamarin.Forms;
using Plugin.Toasts;
using System.Threading.Tasks;

namespace RealmSample.Pages
{
	public abstract class BaseContentPage : ContentPage
	{
		IToastNotificator _notificator;

		public BaseContentPage()
		{
			_notificator = DependencyService.Get<IToastNotificator>();
		}

		protected Task<bool> ShowToast(ToastNotificationType type, string title, string description)
		{
			return _notificator.Notify(type, title, description, TimeSpan.FromSeconds(2));
		}
	}
}


