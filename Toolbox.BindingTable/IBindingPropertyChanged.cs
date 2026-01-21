using System.ComponentModel;

namespace Toolbox.BindingTable
{
    /// <inheritdoc cref="System.ComponentModel.INotifyPropertyChanged"/>
    /// <remarks>
    /// Helper interface for INotifyPropertyChanged.<br/>
    /// Child classes MUST override the PropertyChanged event to reference it. CS0070
    /// </remarks>
    /// <example>
    /// public virtual event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
    /// </example>
    public interface IBindingPropertyChanged : INotifyPropertyChanged
    { }

    /// <summary>
    /// Helper Class for INotifyPropertyChanged
    /// </summary>
    /// <example>
    /// class SomeClass : IBindingPropertyChanged
    /// {
    ///     public String SomeProperty
    ///     {
    ///         get;
    ///         set { field = value; this.OnPropertyChanged(PropertyChanged, nameof(SomeProperty)); }
    ///     }
    /// 
    ///     public virtual event PropertyChangedEventHandler? PropertyChanged;
    /// }
    /// 
    /// --- or ---
    /// 
    /// class SomeClass : IBindingPropertyChanged
    /// {
    ///     public String SomeProperty
    ///     {
    ///         get;
    ///         set { field = value; OnPropertyChanged(nameof(SomeProperty)); }
    ///     }
    ///     
    ///     public virtual event PropertyChangedEventHandler? PropertyChanged;
    ///     
    ///     protected virtual void OnPropertyChanged(String propertyName)
    ///     { this.OnPropertyChanged(PropertyChanged, nameof(propertyName)); }
    /// }
    /// </example>
    public static class BindingPropertyChanged
    {
        static SynchronizationContext? syncContext;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property on the given sender object.
        /// </summary>
        /// <remarks>
        /// This method is intended to simplify raising the PropertyChanged event in
        /// implementations of INotifyPropertyChanged. It ensures that the event is raised on the appropriate
        /// synchronization context if one is present.
        /// </remarks>
        /// <param name="sender">The object that is the source of the property change notification. Typically, this is the object whose
        /// property value has changed.</param>
        /// <param name="eventHandler">The event handler to invoke for the PropertyChanged event. If null, the event is not raised.</param>
        /// <param name="propertyName">The name of the property that changed, or null to indicate that all properties have changed.</param>
        public static void OnPropertyChanged(this IBindingPropertyChanged sender, PropertyChangedEventHandler? eventHandler, String? propertyName)
        {
            if (eventHandler is PropertyChangedEventHandler handler)
            {
                if (syncContext is null)
                {
                    try
                    { handler(sender, new PropertyChangedEventArgs(propertyName)); }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Class", nameof(BindingPropertyChanged));
                        ex.Data.Add("Method", nameof(OnPropertyChanged));
                        ex.Data.Add(nameof(propertyName), propertyName);
                        throw;
                    }
                }
                else
                {
                    syncContext.Post(state =>
                    {
                        try
                        { handler(sender, new PropertyChangedEventArgs(propertyName)); }
                        catch (Exception ex)
                        {
                            ex.Data.Add("Class", nameof(BindingPropertyChanged));
                            ex.Data.Add("Method", nameof(OnPropertyChanged));
                            ex.Data.Add(nameof(propertyName), propertyName);
                            throw;
                        }
                    }, null);
                }
            }
        }

        /// <summary>
        /// Validates the Initialize the static class on the correct thread.
        /// </summary>
        /// <remarks>Verifies that the Constructor was called and initialized as expected.</remarks>
        public static void ValidateInit()
        {
            // The constructor will fire first.

            // Catch if this object was not initialized as expected;
            if(syncContext is null)
            { throw new ArgumentNullException(nameof(syncContext)); }
        }

        /// <summary>
        /// Sets up the SynchronizationContext. Assumed to be executed on main thread.
        /// </summary>
        static BindingPropertyChanged()
        { syncContext = SynchronizationContext.Current; }
    }
}
