using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealmSample.Models;

namespace RealmSample.ViewModels
{
	public class SelectEmployeesViewModel : EmployeesViewModel
	{
		public SelectEmployeesViewModel(IList<EmployeeViewModel> employees, int requestingEmployeeID)
		{
			Employees = new ObservableCollection<EmployeeViewModel>();

			foreach (var employee in employees)
			{
				if (employee.EmployeeID != requestingEmployeeID)
					Employees.Add(employee);
			}
		}
	}
}

