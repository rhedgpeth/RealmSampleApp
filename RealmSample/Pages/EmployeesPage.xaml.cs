using System;
using Xamarin.Forms;
using RealmSample.Models;
using RealmSample.ViewModels;
using System.Threading.Tasks;
using Plugin.Toasts;

namespace RealmSample.Pages
{
	public partial class EmployeesPage : BaseContentPage
	{
		public EmployeesPage()
		{
			InitializeComponent();

			ToolbarItems.Add(new ToolbarItem("Add", null, async () =>
			{
				await Navigation.PushAsync(new EmployeePage());
			}));

			Task.Run(async () =>
			{
				await App.EmployeesViewModel.LoadEmployees();
			});

			BindingContext = App.EmployeesViewModel.Employees;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			employeesList.ItemTapped += Handle_ItemTapped;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			employeesList.ItemTapped -= Handle_ItemTapped;
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new EmployeePage((EmployeeViewModel)e.Item));
		}

		public async void OnDelete(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);

			if (mi != null)
			{
				var employee = (EmployeeViewModel)mi.CommandParameter;

				if (employee != null)
				{
					var result = await DisplayAlert("Delete Employee", string.Format("Are you sure you want to delete {0}?", employee.Name), "Yes", "No");

					if (result == true)
					{
						bool success = await App.EmployeesViewModel.RemoveEmployee(employee);

						if (success)
						{
							// It's not an 'Error', but the toast is red...so.. yea... deal with it, nerd.
							await ShowToast(ToastNotificationType.Error, "Employee Deleted", employee.Name + " has been deleted.");
						}
					}
				}
			}
		}
	}
}

