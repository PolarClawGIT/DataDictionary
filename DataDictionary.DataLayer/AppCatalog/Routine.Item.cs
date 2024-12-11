using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for Database Routine (procedures and functions).
    /// </summary>
    public interface IRoutineItem : IRoutine, 
        IRoutineKey, ICatalogKey, 
        IDbIsSystem, IDbRoutineType, ITemporalItem
    {
        /// <inheritdoc cref="IDbRoutineType.RoutineType"/>
        new DbRoutineType RoutineType { get; }
    }

    /// <summary>
    /// Implementation for Database Routine (procedures and functions).
    /// </summary>
    [Serializable]
    public class RoutineItem : BindingTableRow, IRoutineItem, ISerializable,
        IInfomationSchemaItem<IRoutine, RoutineItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? RoutineId
        {
            get { return GetValue<Guid>(nameof(RoutineId)); }
            private init { SetValue<Guid>(nameof(RoutineId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? SchemaName
        {
            get { return GetValue(nameof(SchemaName)); }
            init { SetValue(nameof(SchemaName), value); }
        }

        /// <inheritdoc/>
        public String? RoutineName
        {
            get { return GetValue(nameof(RoutineName)); }
            init { SetValue(nameof(RoutineName), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSystem
        {
            get
            {
                var list = Table.IsSystem.Split(',').Select(s =>
                {
                    if (s.EndsWith(".*") && RoutineName is String)
                    { s.Substring(0, s.Length - 1).Concat(RoutineName); }

                    return s.Trim();
                });

                return list.Any(w => w.Trim().Equals(
                        String.Format("{0}.{1}", SchemaName, RoutineName),
                        KeyExtension.CompareString));
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
            init
            { SetValue(nameof(RoutineType), DbRoutineEnumeration.Cast(value).Name); }
        }

        /// <inheritdoc/>
        String? IRoutineType.RoutineType { get {return GetValue(nameof(RoutineType)); } }

        #region ITemporalItem
        TemporalItem temporal; // Backing field for Temporal Data.

        /// <inheritdoc/>
        public DateTime? CreatedOn { get { return temporal.CreatedOn; } }

        /// <inheritdoc/>
        public String? CreatedBy { get { return temporal.CreatedBy; } }

        /// <inheritdoc/>
        public DateTime? RemovedOn { get { return temporal.RemovedOn; } }

        /// <inheritdoc/>
        public String? RemovedBy { get { return temporal.RemovedBy; } }

        /// <inheritdoc/>
        public Boolean? IsInserted { get { return temporal.IsInserted; } }

        /// <inheritdoc/>
        public Boolean? IsUpdated { get { return temporal.IsUpdated; } }

        /// <inheritdoc/>
        public Boolean? IsDeleted { get { return temporal.IsDeleted; } }

        /// <inheritdoc/>
        public Boolean? IsCurrent { get { return temporal.IsCurrent; } }

        /// <inheritdoc/>
        public DbModificationType Modification { get { return temporal.Modification; } }
        #endregion

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(RoutineId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(string)){ AllowDBNull = false},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Database Routine Item.
        /// </summary>
        public RoutineItem() : base()
        { RoutineId = Guid.NewGuid();

            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, IRoutine source)
            where TResult : RoutineItem, new()
        {
            DbRoutineType routineType = DbRoutineType.Null;
            if (DbRoutineEnumeration.TryParse(source.RoutineType, null, out DbRoutineEnumeration? result))
            { routineType = result.Value; }

            TResult newValue = new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                RoutineName = source.RoutineName,
                RoutineType = routineType
            };

            newValue.Update(source);
            return newValue;
        }

        /// <inheritdoc/>
        public void Update(IRoutine source)
        {
            // Nothing to actually do. All values are fixed.
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Routine Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected RoutineItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new RoutineKeyName(this).ToString(); }

    }
}
