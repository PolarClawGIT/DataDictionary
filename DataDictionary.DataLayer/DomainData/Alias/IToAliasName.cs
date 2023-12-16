using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Interface that allows a Db Item or Library Item into an Alias Name
    /// </summary>
    public interface IToAliasName
    {
        // C# default implementation within a interface does not support inheritance or override.
        // Each implementation is done with an extension. This just marks a class that implements the interface.
        // public virtual String ToAliasName()
    }

    /// <summary>
    /// Implementation for IToAliasName
    /// Default implementation.
    /// </summary>
    public static class ToAliasNameExtension
    {
        //public static String ToAliasName(this IToAliasName source)
        //{ return AliasExtension.FormatName(String.Empty); }
    }
}
