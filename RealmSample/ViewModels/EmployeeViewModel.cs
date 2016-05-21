using System;
using System.Linq;
using System.Threading.Tasks;
using RealmSample.Models;
using Realms;
using System.Collections.Generic;

namespace RealmSample.ViewModels
{
	public class EmployeeViewModel : BaseViewModel
	{
		Employee _employee; 

		bool _canSave = true;
		public bool CanSave
		{
			get
			{
				return _canSave;
			}
			set
			{
				_canSave = value;
				OnPropertyChanged();
			}
		}

		public int EmployeeID
		{
			get
			{
				return _employee != null ? _employee.EmployeeID : 0;
			}
		}

		string _firstName;
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Name));
			}
		}

		string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Name));
			}
		}

		string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		EmployeeViewModel _manager;
		EmployeeViewModel Manager
		{
			get
			{
				return _manager;
			}
			set
			{
				_manager = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ManagerName));
			}
		}

		public string ManagerName
		{
			get
			{
				return Manager != null ? Manager.Name : string.Empty;
			}
		}

		List<EmployeeViewModel> _minions;
		public List<EmployeeViewModel> Minions
		{
			get
			{
				if (_minions == null) 
				{
					_minions = new List<EmployeeViewModel>();

					if (_employee?.Minions != null)
					{
						foreach (var employee in _employee?.Minions)
							_minions.Add(new EmployeeViewModel(employee));
					}
				}

				return _minions;
			}
		}

		public EmployeeViewModel(Employee employee = null)
		{
			if (employee != null)
			{
				_employee = employee;
				_firstName = employee.FirstName;
				_lastName = employee.LastName;
				_title = employee.Title;
				_manager = new EmployeeViewModel(employee.Manager);
			}
			else
				_employee = new Employee();
		}

		public bool Save()
		{
			//if (!CanSave || IsBusy)
				//return false;

			try
			{
				// TODO: Attempt to reconcile multiple threads for realms - so far..not great
				var realm = Realm.GetInstance("RealmTest");

				// For some reason things bomb when _employee.EmployeeID is used directly within the following Linq query
				// So, I've had to pull set the id explicity to be used int he query..that's pretty weak sauce.
				int employeeID = _employee.EmployeeID;

				if (_employee.EmployeeID == 0)
				{
					int count = realm.All<Employee>().Count();

					// Create and save a new RealmObject
					realm.Write(() =>
					{
						_employee = realm.CreateObject<Employee>();
						_employee.EmployeeID = count + 1;
						_employee.FirstName = FirstName;
						_employee.LastName = LastName;
						_employee.Title = Title;

						if (Manager != null)
						{
							int managerID = Manager.EmployeeID;
							var manager = realm.All<Employee>().First(x => x.EmployeeID == managerID);

							_employee.Manager = manager;

							manager.Minions.Add(_employee);
						}
					});
				}
				else
				{
					// Doing the following results in this: The rhs of the binary operator 'Equal' should be a constant or closure variable expression
					//var originalEmployee = realm.All<Employee>().First(x => x.EmployeeID == _employee.EmployeeID);

					// Have to use this instead
					var originalEmployee = realm.All<Employee>().First(x => x.EmployeeID == employeeID);

					using (var trans = realm.BeginWrite())
					{
						originalEmployee.FirstName = FirstName;
						originalEmployee.LastName = LastName;
						originalEmployee.Title = Title;

						if (Manager == null || Manager.EmployeeID == 0)
							originalEmployee.Manager = null;
						else if (Manager?.EmployeeID != originalEmployee.Manager?.EmployeeID)
						{
							int managerEmployeeID = Manager.EmployeeID;
							var manager = realm.All<Employee>().First(x => x.EmployeeID == managerEmployeeID);

							originalEmployee.Manager = manager;

							manager.Minions.Add(originalEmployee);
						}

						// This should prevent other realms from having to call an explicit '.Refresh()', but 
						// unfortunately that's not always the case ¯\_(ツ)_/¯
						trans.Commit();
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void UpdateManager(EmployeeViewModel employee)
		{
			Manager = employee;
		}
	}
}

