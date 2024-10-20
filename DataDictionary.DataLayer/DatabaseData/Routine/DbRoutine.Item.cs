﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for Database Routine (procedures and functions).
    /// </summary>
    public interface IDbRoutineItem : IDbRoutineKeyName, IDbRoutineKey, IDbCatalogKey, IDbIsSystem, IDbRoutineType, IScopeType
    { }

    /// <summary>
    /// Implementation for Database Routine (procedures and functions).
    /// </summary>
    [Serializable]
    public class DbRoutineItem : BindingTableRow, IDbRoutineItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? RoutineId { get { return GetValue<Guid>(nameof(RoutineId)); } }

        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue(nameof(RoutineName)); } }

        /// <inheritdoc/>
        public bool IsSystem
        {
            get
            {
                return SchemaName is "dbo" &&
                    RoutineName is "sp_creatediagram" or
                    "sp_renamediagram" or
                    "sp_alterdiagram" or
                    "sp_dropdiagram" or
                    "fn_diagramobjects" or
                    "sp_helpdiagrams" or
                    "sp_helpdiagramdefinition" or
                    "sp_upgraddiagrams";
            }
        }

        /// <inheritdoc/>
        public DbRoutineType RoutineType
        {
            get
            {
                String? value = GetValue(nameof(RoutineType));
                if (DbRoutineEnumeration.TryParse(value, null, out DbRoutineEnumeration? result))
                { return result.Value; }
                else { return DbRoutineType.Null; }
            }
        }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (RoutineType)
                {
                    case DbRoutineType.Null: return ScopeType.Null;
                    case DbRoutineType.Function: return ScopeType.DatabaseFunction;
                    case DbRoutineType.Procedure: return ScopeType.DatabaseProcedure;
                    default: return ScopeType.Null;
                }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(RoutineId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(string)){ AllowDBNull = false},
        };

        /// <summary>
        /// Constructor for Database Routine Item.
        /// </summary>
        public DbRoutineItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            if (this.Scope.ToDbLevel() is IDbLevelObjectKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = RoutineName,
                    Level1Type = scopeKey.ObjectScope.ToString(),
                    Level2Name = String.Empty,
                    Level2Type = String.Empty,
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                ex.Data.Add(nameof(RoutineName), RoutineName);
                throw ex;
            }
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Routine Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbRoutineItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbRoutineKeyName(this).ToString(); }
    }
}
