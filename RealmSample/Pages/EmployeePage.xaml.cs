using System;
using Xamarin.Forms;
using RealmSample.ViewModels;
using Plugin.Toasts;

namespace RealmSample.Pages
{
	public partial class EmployeePage : BaseContentPage
	{
		public Action ReloadEmployees { get; set; }
		readonly EmployeeViewModel _viewModel;

		public EmployeePage(EmployeeViewModel viewModel = null)
		{
			InitializeComponent();

			ToolbarItems.Add(new ToolbarItem("Save", null, async () =>
			{
				var success = _viewModel.Save();

				if (success)
				{
					await ShowToast(ToastNotificationType.Success, "Employee Saved", _viewModel.Name + " has been saved successfully!");
						
					// Refresh employees on backstacked page
					await App.EmployeesViewModel.LoadEmployees();
				}
				else
				{
					await ShowToast(ToastNotificationType.Error, "Error", "Error attempting to save employee.");
				}
			}));

			if (viewModel == null)
				viewModel = new EmployeeViewModel();
			
			BindingContext = _viewModel = viewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			managerButton.Clicked += Handle_Clicked;
			minionsList.ItemTapped += Handle_ItemTapped;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			managerButton.Clicked -= Handle_Clicked;
			minionsList.ItemTapped -= Handle_ItemTapped;
		}

		async void Handle_Clicked(object sender, EventArgs e)
		{
			var selectEmployeesPage = new SelectEmployeesPage(App.EmployeesViewModel.Employees, _viewModel.EmployeeID);
			selectEmployeesPage.EmployeesSelected = _viewModel.UpdateManager;
			await Navigation.PushModalAsync(selectEmployeesPage);
		}

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new EmployeePage((EmployeeViewModel)e.Item));
		}
	}
}

