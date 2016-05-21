using System;
using Realms;

namespace RealmSample.Models
{
	public class Employee : RealmObject
	{
		[Indexed]
		public int EmployeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Title { get; set; }
		public string Name { get { return FirstName + " " + LastName; } }
		public Employee Manager { get; set; }
		public RealmList<Employee> Minions { get; }
	}
}

