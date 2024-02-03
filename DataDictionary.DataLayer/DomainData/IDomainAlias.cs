using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    /// <summary>
    /// Common Interface for Domain Alias items
    /// </summary>
    public interface IDomainAlias: IAliasKeyName, IScopeKeyName, IScopeKey
    {
    }
}
