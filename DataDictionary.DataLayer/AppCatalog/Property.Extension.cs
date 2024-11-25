using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppCatalog
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
            {ScopeType.DatabaseSchema, new PropertyCatalogKey(){ CatalogScope = DbLevelCatalogType.Schema} },
            {ScopeType.DatabaseFunction, new PropertyObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function} },
            {ScopeType.DatabaseProcedure, new PropertyObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure} },
            {ScopeType.DatabaseTable, new PropertyObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table} },
            {ScopeType.DatabaseDomain, new PropertyObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Type} },
            {ScopeType.DatabaseView, new PropertyObjectKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View} },
            {ScopeType.DatabaseViewColumn, new PropertyElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableColumn, new PropertyElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableConstraint, new PropertyElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Constraint} },
            {ScopeType.DatabaseProcedureParameter, new PropertyElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure, ElementScope= DbLevelElementType.Parameter} },
            {ScopeType.DatabaseFunctionParameter, new PropertyElementKey(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function, ElementScope= DbLevelElementType.Parameter} },
        };

        /// <summary>
        /// Attempts to convert a Db Level Key into the Scope Type.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScopeType? ToDbLevel(this IDbLevelKey source)
        {
            if (source is IPropertyElementKey elementScope)
            {
                PropertyElementKey elementKey = new PropertyElementKey(elementScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyElementKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IPropertyObjectKey objectScope)
            {
                PropertyObjectKey elementKey = new PropertyObjectKey(objectScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyObjectKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IPropertyCatalogKey calalogScope)
            {
                PropertyCatalogKey elementKey = new PropertyCatalogKey(calalogScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyCatalogKey && elementKey.Equals(w.Value))
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
