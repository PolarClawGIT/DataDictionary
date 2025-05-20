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
    public interface IAliasSubType : IAliasKey, IBindingPropertyChanged
    {
        String? IAliasKeyName.AliasPath { get { return AliasPath.MemberFullPath; } }
        ScopeType IAliasKey.AliasScope { get { return AliasScope; } }

        /// <inheritdoc cref="IAliasKeyName.AliasPath"/>
        new PathIndex AliasPath { get; set; }

        /// <inheritdoc cref="IAliasKey.AliasScope"/>
        new ScopeType AliasScope { get; set; }
    }
}
