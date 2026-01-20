using System.ComponentModel;

namespace Toolbox.BindingTable
{
    /// <inheritdoc cref="System.ComponentModel.INotifyPropertyChanged"/>
    /// <remarks>
    /// Helper interface for INotifyPropertyChanged
    /// </remarks>
    public interface IBindingPropertyChanged : INotifyPropertyChanged
    { }

    /// <summary>
    /// Helper Class for INotifyPropertyChanged
    /// </summary>
    public static class BindingPropertyChanged
    {
        static SynchronizationContext? syncContext;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property on the given sender object.
        /// </summary>
        /// <remarks>This method is intended to simplify raising the PropertyChanged event in
        /// implementations of INotifyPropertyChanged. It ensures that the event is raised on the appropriate
        /// synchronization context if one is present.</remarks>
        /// <param name="sender">The object that is the source of the property change notification. Typically, this is the object whose
        /// property value has changed.</param>
        /// <param name="eventHandler">The event handler to invoke for the PropertyChanged event. If null, the event is not raised.</param>
        /// <param name="propertyName">The name of the property that changed, or null to indicate that all properties have changed.</param>
        public static void OnPropertyChanged(this IBindingPropertyChanged sender, PropertyChangedEventHandler? eventHandler, String? propertyName)
        {
            if (eventHandler is PropertyChangedEventHandler handler)
            {
                if (syncContext is null)
                { handler(sender, new PropertyChangedEventArgs(propertyName)); }
                else
                { syncContext.Post(state => { handler(sender, new PropertyChangedEventArgs(propertyName)); }, null); }
            }
        }

        /// <summary>
        /// Initialize the static class on the correct thread, if not already Initialize.
        /// </summary>
        public static void Init() { } // All this is really for is to control when the constructor is run.

        /// <summary>
        /// Sets up the SynchronizationContext. Assumed to be executed on main thread.
        /// </summary>
        static BindingPropertyChanged()
        { syncContext = SynchronizationContext.Current; }
    }
}
