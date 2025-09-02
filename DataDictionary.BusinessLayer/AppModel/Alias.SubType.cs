using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Alias Sub Type
    /// </summary>
    public interface IAliasSubType : IBindingPropertyChanged
    {
        /// <inheritdoc cref="IAliasKey.AliasScope"/>
        ScopeType AliasScope { get; set; }

        /// <inheritdoc cref="IAliasKeyName.AliasPath"/>
        PathIndex AliasPath { get; set; }
    }
}
