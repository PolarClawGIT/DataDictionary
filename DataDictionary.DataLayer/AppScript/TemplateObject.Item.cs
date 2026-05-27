using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting TemplateObject
    /// </summary>
    public interface ITemplateObjectItem : ITemplateKey, ITemplateObjectKeyName
    {
        /// <summary>
        /// Do not include the matching Object in the results.
        /// NOT filter.
        /// </summary>
        Boolean IsExcluded { get; }

        /// <summary>
        /// Keep Orphaned is an object that is not in the CURRENT model.
        /// This allows a Template to be used in multiple models that are not exactly alike.
        /// </summary>
        Boolean KeepOrphaned { get; }
    }

    /// <summary>
    /// Implementation for the Scripting TemplateObject.
    /// </summary>
    [Serializable, Obsolete("Merged to Schema Document")]
    public class TemplateObjectItem : BindingTableRow, ITemplateObjectItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? ObjectId
        {
            get { return GetValue<Guid>(nameof(ObjectId)); }
            protected set { SetValue(nameof(ObjectId), value); }
        }

        /// <inheritdoc/>
        public String? ObjectName
        {
            get { return GetValue(nameof(ObjectName)); }
            set { SetValue(nameof(ObjectName), value); }
        }

        /// <inheritdoc/>
        public ScopeType ObjectScope
        {
            get
            {
                String? value = GetValue(nameof(ObjectScope));
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set
            { SetValue(nameof(ObjectScope), value.GetEnumeration().Name); }
        }


        /// <inheritdoc/>
        public Boolean IsExcluded
        {
            get
            {
                if (GetValue<bool>(nameof(IsExcluded), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(IsExcluded), value); }
        }

        /// <inheritdoc/>
        public Boolean KeepOrphaned
        {
            get
            {
                if (GetValue<bool>(nameof(KeepOrphaned), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set { SetValue<Boolean>(nameof(KeepOrphaned), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Schema Definition
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected TemplateObjectItem() : base()
        {
            if (ObjectId is null) { ObjectId = Guid.NewGuid(); }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Schema Definition
        /// </summary>
        public TemplateObjectItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ObjectId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsExcluded), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(KeepOrphaned), typeof(Boolean)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TemplateObjectItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return ObjectName ?? String.Empty; }
    }
}
