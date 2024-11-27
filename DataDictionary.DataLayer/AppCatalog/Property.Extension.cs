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
            {ScopeType.DatabaseSchema, new PropertyCatalogLevel(){ CatalogScope = DbLevelCatalogType.Schema} },
            {ScopeType.DatabaseFunction, new PropertyObjectLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function} },
            {ScopeType.DatabaseProcedure, new PropertyObjectLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure} },
            {ScopeType.DatabaseTable, new PropertyObjectLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table} },
            {ScopeType.DatabaseDomain, new PropertyObjectLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Type} },
            {ScopeType.DatabaseView, new PropertyObjectLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View} },
            {ScopeType.DatabaseViewColumn, new PropertyElementLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.View, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableColumn, new PropertyElementLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Column} },
            {ScopeType.DatabaseTableConstraint, new PropertyElementLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Table, ElementScope= DbLevelElementType.Constraint} },
            {ScopeType.DatabaseProcedureParameter, new PropertyElementLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Procedure, ElementScope= DbLevelElementType.Parameter} },
            {ScopeType.DatabaseFunctionParameter, new PropertyElementLevel(){ CatalogScope = DbLevelCatalogType.Schema, ObjectScope = DbLevelObjectType.Function, ElementScope= DbLevelElementType.Parameter} },
        };

        /// <summary>
        /// Attempts to convert a Db Level Key into the Scope Type.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScopeType? ToDbLevel(this IDbLevelKey source)
        {
            if (source is IPropertyElementLevel elementScope)
            {
                PropertyElementLevel elementKey = new PropertyElementLevel(elementScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyElementLevel && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IPropertyObjectLevel objectScope)
            {
                PropertyObjectLevel elementKey = new PropertyObjectLevel(objectScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyObjectLevel && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbLevelKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IPropertyCatalogLevel calalogScope)
            {
                PropertyCatalogLevel elementKey = new PropertyCatalogLevel(calalogScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IPropertyCatalogLevel && elementKey.Equals(w.Value))
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
