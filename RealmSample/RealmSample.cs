using System;
using RealmSample.Pages;
using Xamarin.Forms;
using RealmSample.ViewModels;

namespace RealmSample
{
	public class App : Application
	{
		public static EmployeesViewModel EmployeesViewModel { get; private set; }

		static App()
		{
			EmployeesViewModel = new EmployeesViewModel();
		}

		public App()
		{
			MainPage = new NavigationPage(new EmployeesPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

