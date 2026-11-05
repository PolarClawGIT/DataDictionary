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
    /// 
    /// protected virtual void OnPropertyChanged(String propertyName)
    /// { this.OnPropertyChanged(PropertyChanged, propertyName); }
    /// </example>
    public interface IBindingPropertyChanged : INotifyPropertyChanged
    {   // This is a simple wrapper interface for INotifyPropertyChanged.
        // It was intended to implement thing like OnPropertyChanged but language restriction make it so that it does not work.
        // IBindingPropertyChanged is not required for BindingPropertyChanged.
        // This interface can be deleted. For now it is just here to help locate places that need INotifyPropertyChanged.

        /// <summary>
        /// Method to call the PropertyChanged event.
        /// </summary>
        /// <remarks>This does not work. The method in the class would still be Public and that is not the intent.</remarks>
        /// <param name="propertyName"></param>
        //protected abstract void OnPropertyChanged(String propertyName);
    }

    /// <summary>
    /// Helper Class for INotifyPropertyChanged
    /// </summary>
    /// <example>
    /// class SomeClass : INotifyPropertyChanged
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
    /// class SomeClass : INotifyPropertyChanged
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
    ///     { this.OnPropertyChanged(PropertyChanged, propertyName); }
    /// }
    /// </example>
    public static class BindingPropertyChanged
    {
        static SynchronizationContext? syncContext;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property on the given sender object.<br/>
        /// Contains Validation logic.
        /// </summary>
        /// <remarks>
        /// This method is intended to simplify raising the PropertyChanged event in implementations of INotifyPropertyChanged.<br/>
        /// Provides common base logic for checking for SynchronizationContext and Validation.
        /// </remarks>
        /// <param name="sender">The object that is the source of the property change notification. Typically, this is the object whose
        /// property value has changed.</param>
        /// <param name="eventHandler">The event handler to invoke for the PropertyChanged event. If null, the event is not raised.<br/>
        /// CS0079: The EventHandler needs to be passed into the method.</param>
        /// <param name="propertyName">The name of the property that changed, or null to indicate that all properties have changed.</param>
        public static void OnPropertyChanged(this INotifyPropertyChanged sender, PropertyChangedEventHandler? eventHandler, String? propertyName)
        {
            if (eventHandler is PropertyChangedEventHandler handler)
            {
                if (syncContext is null)
                { TryCatchEvent(); }
                else
                { syncContext.Post(state => { TryCatchEvent(); }, null); }
            }
            else { } // No events wired up to the PropertyChanged.

            void TryCatchEvent()
            {
                var properties = sender.GetType().GetProperties().ToList();
                var objectName = sender.GetType().FullName;

                // Validation Logic
                if (propertyName is String && !properties.Any(w => propertyName.Equals(w.Name)))
                {   // Calling OnPropertyChanged on a property this is not in the object causes errors in FindGoodRow.
                    Exception ex = new IndexOutOfRangeException("Property not found");

                    ex.Data.Add("Class", objectName);
                    ex.Data.Add(nameof(propertyName), propertyName);
                    throw ex;
                }
                else // Event Handler
                {
                    try
                    { handler(sender, new PropertyChangedEventArgs(propertyName)); }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Class", objectName);
                        ex.Data.Add(nameof(propertyName), propertyName);
                        throw;
                    }
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
            if (syncContext is null)
            { throw new ArgumentNullException(nameof(syncContext)); }
        }

        /// <summary>
        /// Sets up the SynchronizationContext. Assumed to be executed on main thread.
        /// </summary>
        static BindingPropertyChanged()
        { syncContext = SynchronizationContext.Current; }
    }
}
