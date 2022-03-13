using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ThisIsNotArt.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Properties

        #region Functions
        /// <summary>
        /// Triggeres the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that's changed</param>
        public void Notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Triggeres the PropertyChanged event
        /// </summary>
        /// <param name="property">Lambda expression returning property name</param>
		public void Notify<T>(Expression<Func<T>> property)
        {
            Notify((property.Body as MemberExpression).Member.Name);
        }
        #endregion Functions
    }
}
