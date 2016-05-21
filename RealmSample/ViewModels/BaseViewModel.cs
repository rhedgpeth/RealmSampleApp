using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RealmSample.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected bool IsBusy { get; set; }
		protected bool IsDirty { get; set; }

		public void OnPropertyChanged([CallerMemberNameAttribute]string propertyName = null)
		{
			var handler = PropertyChanged;

			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		public virtual Task InitAsync()
		{
			return new Task(() => { });
		}
	}
}

