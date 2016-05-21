using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using RealmSample.Models;
using Xamarin.Forms;
using Realms;

namespace RealmSample.ViewModels
{
	public class EmployeesViewModel : BaseViewModel
	{
		// NOTE: Cross-thread support/functionality is a real pain in the ass
		Realm _realm;

		public IList<EmployeeViewModel> Employees;
		public EmployeeViewModel SelectedEmployee { get; set; }

		public EmployeesViewModel()
		{  
			Employees = new ObservableCollection<EmployeeViewModel>();
		}

		public async Task LoadEmployees(int? excludedEmployeeID = null)
		{
			try
			{
				_realm = Realm.GetInstance("RealmTest");

				// Basically the equivalent of an IIS Reset bomb drop - when all else fails just refresh :(
				_realm.Refresh();

				var employees = _realm.All<Employee>().ToList();

				if (employees != null)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						Employees.Clear();

						foreach (var employee in employees)
						{
							if (!excludedEmployeeID.HasValue || excludedEmployeeID.Value != employee.EmployeeID)
								Employees.Add(new EmployeeViewModel(employee));
						}
					});
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<bool> RemoveEmployee(EmployeeViewModel employee)
		{
			try
			{
				_realm = Realm.GetInstance("RealmTest");

				_realm.Refresh();

				int employeeID = employee.EmployeeID;
				var originalEmployee = _realm.All<Employee>().First(x => x.EmployeeID == employeeID);

				// NOTE: Cascading deletes are not currently supported with Realm
				using (var trans = _realm.BeginWrite())
				{
					_realm.Remove(originalEmployee);
					trans.Commit();
				}

				Device.BeginInvokeOnMainThread(() =>
				{
					Employees.Remove(employee);
				});
			}
			catch (Exception ex)
			{
				//throw ex;
				return false;
			}

			return true;
		}
	}
}

