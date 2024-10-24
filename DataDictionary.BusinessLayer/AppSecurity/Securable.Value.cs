// Ignore Spelling: Securable

using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface ISecurableValue: ISecurableItem,
        ISecurableIndex, ISecurableIndexName
    { }

    /// <inheritdoc/>
    public class SecurableValue : SecurableItem, ISecurableValue
    { }
}
