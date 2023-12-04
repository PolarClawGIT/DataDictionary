using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Interface that allows a Scope Name to be converted to a Scope Type
    /// </summary>
    public interface IToScopeType
    {
        // C# default implementation within a interface does not support inheritance or override.
        // Each implementation is done with an extension. This just marks a class that implements the interface.
        // public ScopeType ToScopeType() {}
    }

    /// <summary>
    /// Implementation that allows a Scope Name to be converted to a Scope Type.
    /// Default implementation.
    /// </summary>
    public static class ToScopeTypeExtension
    {
        /// <summary>
        /// Scope Name to be converted to a Scope Type.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScopeType ToScopeType(this IToScopeType source) { return ScopeType.Null; }
    }

}
