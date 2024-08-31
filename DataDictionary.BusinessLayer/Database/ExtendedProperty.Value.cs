using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyValue : IDbExtendedPropertyItem,
        ICatalogIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyValue : DbExtendedPropertyItem, IExtendedPropertyValue
    { }
}
