using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Implements a Database Scope Key
    /// </summary>
    public interface IDbLevelKey : IKey
    { }

    /// <summary>
    /// Extensions to cross walk a DbCatalogScope, DbObjectScope and DbElementScope to a ScopeType and back.
    /// </summary>
    public static class DbLevelExtension
    {
        static Dictionary<ScopeType, IDbLevelKey> scopeCrossWalk = new Dictionary<ScopeType, IDbLevelKey>()
        {
            {ScopeType.DatabaseSchema, new DbLevelCatalogKey(){ CatalogScope = DbLevelCatalogType.Schema} },
            {ScopeType.DatabaseFunction, new DbLevelObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function} },
            {ScopeType.DatabaseProcedure, new DbLevelObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure} },
            {ScopeType.DatabaseTable, new DbLevelObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table} },
            {ScopeType.DatabaseDomain, new DbLevelObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Type} },
            {ScopeType.DatabaseView, new DbLevelObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View} },
            {ScopeType.DatabaseViewColumn, new DbLevelElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableColumn, new DbLevelElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableConstraint, new DbLevelElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Constraint} },
            {ScopeType.DatabaseProcedureParameter, new DbLevelElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure, ElementScope= DbLevelElementType.Parameter} },
            {ScopeType.DatabaseFunctionParameter, new DbLevelElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function, ElementScope= DbLevelElementType.Parameter} },
        };

        /// <summary>
        /// Attempts to convert a Db Level Key into the Scope Type.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScopeType? ToDbLevel(this IDbLevelKey source)
        {
            if (source is IDbLevelElementKey elementScope)
            {
                DbLevelElementKey elementKey = new DbLevelElementKey(elementScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbLevelElementKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IDbLevelObjectKey objectScope)
            {
                DbLevelObjectKey elementKey = new DbLevelObjectKey(objectScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbLevelObjectKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IDbLevelCatalogKey calalogScope)
            {
                DbLevelCatalogKey elementKey = new DbLevelCatalogKey(calalogScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbLevelCatalogKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// Attempts to convert a Scope Type into Db Level Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDbLevelKey? ToDbLevel(this ScopeType source)
        {
            if (scopeCrossWalk.ContainsKey(source))
            { return scopeCrossWalk[source]; }
            else { return null; }
        }
    }
}
