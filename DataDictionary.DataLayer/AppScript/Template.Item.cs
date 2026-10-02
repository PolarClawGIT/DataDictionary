using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template
    /// </summary>
    public interface ITemplateItem : ITemplateKeyName, ITemplateKey, IDocumentDirectory
    {
        /// <summary>
        /// Description of the Scripting Template
        /// </summary>
        String? TemplateDescription { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template.
    /// </summary>
    [Serializable]
    public class TemplateItem: BindingTableRow, ITemplateItem, ISerializable
    {
        /// <inheritdoc/>
        public virtual Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public virtual String? TemplateTitle
        {
            get { return GetValue(nameof(TemplateTitle)); }
            set { SetValue(nameof(TemplateTitle), value); }
        }

        /// <inheritdoc/>
        public virtual String? TemplateDescription
        {
            get { return GetValue(nameof(TemplateDescription)); }
            set { SetValue(nameof(TemplateDescription), value); }
        }

        /// <inheritdoc/>
        public virtual DirectoryType RootFolder
        {
            get
            {
                String? value = GetValue(nameof(RootFolder));
                if (value.TryParse(out DirectoryType result))
                { return result; }
                else { return DirectoryType.Null; }
            }
            set
            { SetValue(nameof(RootFolder), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public virtual String? RelativePath
        {
            get { return GetValue(nameof(RelativePath)); }
            set { SetValue(nameof(RelativePath), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Data Source
        /// </summary>
        public TemplateItem() : base()
        {
            if (TemplateId is null) { TemplateId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(TemplateTitle)) { TemplateTitle = "(new Template)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RootFolder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RelativePath), typeof(String)){ AllowDBNull = true},
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
        protected TemplateItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return TemplateTitle ?? String.Empty; }
    }
}
