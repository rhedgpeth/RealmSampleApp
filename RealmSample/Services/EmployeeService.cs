using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;
using RealmSample.Models;

namespace RealmSample.Services
{
	public class EmployeeService
	{
		static readonly Lazy<EmployeeService> lazy = new Lazy<EmployeeService>(() => new EmployeeService());
		public static EmployeeService Instance { get { return lazy.Value; } }

		//Realm realm;

		EmployeeService()
		{
			//realm = Realm.GetInstance();
		}

		public async Task<List<Employee>> GetAll()
		{
			var realm = Realm.GetInstance("RealmTest");

			// Has to be called
			// 
            realm.Refresh();

			return realm.All<Employee>().ToList();
		}

		/*
		public bool Save(Employee employee)
		{
			var realm = Realm.GetInstance();

			try
			{
				if (employee.EmployeeID == 0)
				{
					int count = realm.All<Employee>().Count();
					employee.EmployeeID = count + 1;

					realm.Write(() =>
					{
						var newObj = realm.CreateObject<Employee>();
						newObj.EmployeeID = employee.EmployeeID;
						newObj.FirstName = employee.FirstName;
						newObj.LastName = employee.LastName;
					});
				}
				else
				{
					var originalEmployee = realm.All<Employee>().First(x => x.EmployeeID == employee.EmployeeID);

					Employee manager = null;
					if (employee.Manager != null)
						manager = realm.All<Employee>().First(x => x.EmployeeID == employee.Manager.EmployeeID);

					using (var trans = realm.BeginWrite())
					{
						originalEmployee.FirstName = employee.FirstName;
						originalEmployee.LastName = employee.LastName;
						originalEmployee.Manager = manager;

						trans.Commit();
					}
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		bool Delete(Employee employee)
		{
			var realm = Realm.GetInstance();

			try
			{
				var originalEmployee = realm.All<Employee>().First(x => x.EmployeeID == employee.EmployeeID);

				using (var trans = realm.BeginWrite())
				{
					realm.Remove(originalEmployee);
					trans.Commit();
				}
			}
			catch
			{
				return false;
			}

			return true;
		} */
	}
}

