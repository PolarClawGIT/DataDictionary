using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// Interface for Database Scope
    /// </summary>
    public interface IDbScopeType : IToScopeType
    {
        /// <summary>
        /// The Database Scope Name. This describes the Scope/Level of an Object within database.
        /// </summary>
        string? ScopeName { get; }
    }

    /// <summary>
    /// Interface for Database Scope
    /// </summary>
    public static class DbScopeType
    {
        /// <summary>
        /// Returns the Scope Type for the Database Scope.
        /// </summary>
        /// <returns></returns>
        public static ScopeType ToScopeType(this IDbScopeType source)
        { return ScopeTypeExtension.ToScopeType(source.ScopeName); }
    }
}
