using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AthlosifyMobileApp.ViewModels
{
	public class ObservableObject : INotifyPropertyChanged

	{

		//[NotifyPropertyChangedInvocator]
		#region INotifyPropertyChanged       
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		protected void NotifyPropertyChanged()
		{
			OnPropertyChanged(null);
		}

		protected virtual bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "", Action onChanged = null, 
			Func<T, T, bool> validateValue = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			if (validateValue != null && (validateValue(backingStore, value)))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);

			return true;

		}
	}
}
