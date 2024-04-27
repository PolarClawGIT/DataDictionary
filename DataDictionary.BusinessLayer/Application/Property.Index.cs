using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IPropertyIndex: IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey, IPropertyIndex
    {
        /// <inheritdoc cref="PropertyKey(IPropertyKey)"/>
=======
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey
    {
        /// <inheritdoc cref="PropertyKey.PropertyKey(IPropertyKey)"/>
>>>>>>> RenameIndexValue
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }
}
