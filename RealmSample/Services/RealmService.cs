using System;
using System.Threading.Tasks;
using Realms;

namespace RealmSample.Services
{
	public abstract class RealmService
	{
		Realm realm;

		protected RealmService()
		{
			//realm = Realm.GetInstance();
		}

		public bool Create<T>(T obj) where T : RealmObject, new()
		{
			try
			{
				realm.Write(() =>
				{
					var newObj = realm.CreateObject<T>();
					newObj = obj;
				});
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool Update<T>(T originalObject, T newObject) where T : RealmObject, new()
		{
			try
			{
				using (var trans = realm.BeginWrite())
				{
					originalObject = newObject;
					trans.Commit();
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		public bool Delete<T>(T obj) where T : RealmObject, new()
		{
			try
			{
				using (var trans = realm.BeginWrite())
				{
					realm.Remove(obj);
					trans.Commit();
				}
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}

