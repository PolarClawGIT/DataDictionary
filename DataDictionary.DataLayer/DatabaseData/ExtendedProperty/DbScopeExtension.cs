using DataDictionary.DataLayer.ApplicationData.Scope;
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
    public interface IDbScopeKey : IKey
    { }

    /// <summary>
    /// Extensions to cross walk a DbCatalogScope, DbObjectScope and DbElementScope to a ScopeType and back.
    /// </summary>
    public static class DbScopeExtension
    {
        static Dictionary<ScopeType, IDbScopeKey> scopeCrossWalk = new Dictionary<ScopeType, IDbScopeKey>()
        {
            {ScopeType.DatabaseSchema, new DbCatalogScopeKey(){ CatalogScope = DbCatalogScope.Schema} },
            {ScopeType.DatabaseSchemaFunction, new DbObjectScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Function} },
            {ScopeType.DatabaseSchemaProcedure, new DbObjectScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Procedure} },
            {ScopeType.DatabaseSchemaTable, new DbObjectScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Table} },
            {ScopeType.DatabaseSchemaType, new DbObjectScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Type} },
            {ScopeType.DatabaseSchemaView, new DbObjectScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.View} },
            {ScopeType.DatabaseSchemaViewColumn, new DbElementScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.View, ElementScope= DbElementScope.Column} },
            {ScopeType.DatabaseSchemaTableColumn, new DbElementScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Table, ElementScope= DbElementScope.Column} },
            {ScopeType.DatabaseSchemaTableConstraint, new DbElementScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Table, ElementScope= DbElementScope.Constraint} },
            {ScopeType.DatabaseSchemaProcedureParameter, new DbElementScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Procedure, ElementScope= DbElementScope.Parameter} },
            {ScopeType.DatabaseSchemaFunctionParameter, new DbElementScopeKey(){ CatalogScope = DbCatalogScope.Schema, ObjectScope = DbObjectScope.Function, ElementScope= DbElementScope.Parameter} },
        };

        /// <summary>
        /// Attempts to convert a Db Scope Key into the Scope Type.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScopeType? TryScope(this IDbScopeKey source)
        {
            if (source is IDbElementScopeKey elementScope)
            {
                DbElementScopeKey elementKey = new DbElementScopeKey(elementScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbElementScopeKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbScopeKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IDbObjectScopeKey objectScope)
            {
                DbObjectScopeKey elementKey = new DbObjectScopeKey(objectScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbObjectScopeKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbScopeKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else if (source is IDbCatalogScopeKey calalogScope)
            {
                DbCatalogScopeKey elementKey = new DbCatalogScopeKey(calalogScope);

                if (scopeCrossWalk.FirstOrDefault(w =>
                        w.Value is IDbCatalogScopeKey && elementKey.Equals(w.Value))
                    is KeyValuePair<ScopeType, IDbScopeKey> scope
                    && scope.Key != ScopeType.Null)
                { return scope.Key; }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// Attempts to convert a Scope Type into Db Scope Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDbScopeKey? TryScope(this ScopeType source)
        {
            if (scopeCrossWalk.ContainsKey(source))
            { return scopeCrossWalk[source]; }
            else { return null; }
        }

        /// <summary>
        /// Attempts to convert a Scope Type into Db Scope Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDbScopeKey? TryScope(this IScopeKey source)
        {
            if (scopeCrossWalk.ContainsKey(source.ScopeId))
            { return scopeCrossWalk[source.ScopeId]; }
            else { return null; }
        }

        /// <summary>
        /// Attempts to convert a Scope Type into Db Scope Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDbScopeKey? TryScope(this IScopeKeyName source)
        {
            if (scopeCrossWalk.ContainsKey(source.ToScopeType()))
            { return scopeCrossWalk[source.ToScopeType()]; }
            else { return null; }
        }
    }
}
