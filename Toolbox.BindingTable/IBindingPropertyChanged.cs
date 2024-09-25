using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    /// <inheritdoc cref="System.ComponentModel.INotifyPropertyChanged"/>
    /// <remarks>
    /// Extension on the base INotifyPropertyChanged
    /// </remarks>
    public interface IBindingPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventHandler"></param>
        /// <param name="propertyName"></param>
        /// <example>
        /// INotifyPropertyChanged.OnPropertyChanged(this, PropertyChanged, e.Column.ColumnName);
        /// </example>
        static void OnPropertyChanged(Object? sender, PropertyChangedEventHandler? eventHandler, String? propertyName)
        {
            if (eventHandler is PropertyChangedEventHandler handler)
            { handler(sender, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
