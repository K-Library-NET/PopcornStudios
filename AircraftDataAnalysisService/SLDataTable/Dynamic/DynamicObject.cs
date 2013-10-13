using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Data
{
	/// <summary>
	/// Dynamic Object Class
	/// </summary>
	public abstract class DynamicObject : INotifyPropertyChanged
	{
		#region Private Members
		private readonly Dictionary<string, object> valuesStorage;
		#endregion

		#region public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangedEventHandler PropertyChanged; 
		#endregion

		#region protected DynamicObject()
		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicObject"/> class.
		/// </summary>
		protected DynamicObject()
		{
			this.valuesStorage = new Dictionary<string, object>();
		} 
		#endregion

		#region protected internal virtual T GetValue<T>(string propertyName)
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		protected internal virtual T GetValue<T>(string propertyName)
		{
			object value;
			if (!this.valuesStorage.TryGetValue(propertyName, out value))
			{
				return default(T);
			}

			return (T)value;
		} 
		#endregion

		#region protected internal virtual void SetValue<T>(string propertyName, T value)
		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="value">The value.</param>
		protected internal virtual void SetValue<T>(string propertyName, T value)
		{
			this.valuesStorage[propertyName] = value;

			this.RaisePropertyChanged(propertyName);
		} 
		#endregion

		#region protected void RaisePropertyChanged(string propertyName)
		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void RaisePropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		} 
		#endregion

		#region protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		/// <summary>
		/// Raises the <see cref="E:PropertyChanged"/> event.
		/// </summary>
		/// <param name="args">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			var hanlder = this.PropertyChanged;
			if (hanlder != null)
			{
				hanlder(this, args);
			}
		} 
		#endregion
	}
}
