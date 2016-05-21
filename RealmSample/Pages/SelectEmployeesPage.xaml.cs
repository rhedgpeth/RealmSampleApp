using System;
using System.Collections.Generic;
using RealmSample.Models;
using RealmSample.ViewModels;
using Xamarin.Forms;

namespace RealmSample.Pages
{
	public partial class SelectEmployeesPage : BaseContentPage
	{
		SelectEmployeesViewModel viewModel;

		public Action<EmployeeViewModel> EmployeesSelected { get; set; }

		public SelectEmployeesPage(IList<EmployeeViewModel> employees, int requestingEmployeeID)
		{
			InitializeComponent();
			viewModel = new SelectEmployeesViewModel(employees, requestingEmployeeID);
			BindingContext = viewModel.Employees;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			cancelButton.Clicked += HandleCancel_Clicked;
			okButton.Clicked += HandleOk_Clicked;
			employeesList.ItemTapped += Handle_ItemTapped;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			cancelButton.Clicked -= HandleCancel_Clicked;
			okButton.Clicked -= HandleOk_Clicked;
			employeesList.ItemTapped -= Handle_ItemTapped;
		}

		async void HandleCancel_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void HandleOk_Clicked(object sender, System.EventArgs e)
		{
			// TODO: Get selected employees and call action
			if (EmployeesSelected != null && viewModel.SelectedEmployee != null)
				EmployeesSelected(viewModel.SelectedEmployee);

			await Navigation.PopModalAsync(true);
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var employee = (EmployeeViewModel)e.Item;

			if (employee != null)
			{
				viewModel.SelectedEmployee = employee;
				okButton.IsEnabled = true;
			}
		}
	}
}

