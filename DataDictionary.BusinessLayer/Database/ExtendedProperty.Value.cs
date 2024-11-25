using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.DataLayer.AppCatalog;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyValue : IPropertyItem,
        ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyValue : PropertyItem, IExtendedPropertyValue
    { }
}
