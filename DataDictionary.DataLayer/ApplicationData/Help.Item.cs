using DataDictionary.Resource.Enumerations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{

    /// <summary>
    /// Interface for a Help Item used for Help Text.
    /// </summary>
    public interface IHelpItem : IHelpKey, IHelpKeyNameSpace, IScopeType, ITemporalItem
    {
        /// <summary>
        /// Title/Subject of the Help Document.
        /// </summary>
        String? HelpSubject { get; }

        /// <summary>
        /// Body of the Help Document
        /// </summary>
        String? HelpText { get; }

        /// <summary>
        /// ToolTip of the Help Document. May appear on individual controls.
        /// </summary>
        String? HelpToolTip { get; }
    }

    /// <summary>
    /// Help Documentation Item for the Application.
    /// </summary>
    [Serializable]
    public class HelpItem : BindingTableRow, IHelpItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? HelpId { get { return GetValue<Guid>(nameof(HelpId)); } protected set { SetValue(nameof(HelpId), value); } }

        // Obsolete, Use NameSpace instead
        //public Guid? HelpParentId { get { return GetValue<Guid>("HelpParentId"); } protected set { SetValue("HelpParentId", value); } }

        /// <inheritdoc/>
        public string? HelpSubject { get { return GetValue(nameof(HelpSubject)); } set { SetValue(nameof(HelpSubject), value); } }

        /// <inheritdoc/>
        public string? HelpToolTip { get { return GetValue(nameof(HelpToolTip)); } set { SetValue(nameof(HelpToolTip), value); } }

        /// <inheritdoc/>
        public string? HelpText
        {
            get { return GetValue(nameof(HelpText)); }
            set
            {
                // The Rich Text control is specifically aggressive about changing the value outside of what the user does.
                if (HelpText == value) { }
                else { SetValue(nameof(HelpText), value); }
            }
        }

        /// <inheritdoc/>
        public string? NameSpace { get { return GetValue(nameof(NameSpace)); } set { SetValue(nameof(NameSpace), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ApplicationHelp;

        /// <inheritdoc/>
        public String? ModifiedBy { get { return GetValue(nameof(ModifiedBy)); } }

        /// <inheritdoc/>
        public DateTime? ModifiedOn { get { return GetValue<DateTime>(nameof(ModifiedOn)); } }

        /// <inheritdoc/>
        public Boolean? IsInserted
        { get { return GetValue<bool>(nameof(IsInserted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsUpdated
        { get { return GetValue<bool>(nameof(IsUpdated), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsDeleted
        { get { return GetValue<bool>(nameof(IsDeleted), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public Boolean? IsCurrent
        { get { return GetValue<bool>(nameof(IsCurrent), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public DbModificationType Modification
        {
            get
            {
                if(IsDeleted == true) { return DbModificationType.Deleted; }
                else if(IsInserted == true) { return DbModificationType.Inserted; }
                else if(IsUpdated == true) { return DbModificationType.Updated; }
                else { return DbModificationType.Null; }
            }
        }

        /// <summary>
        /// Creates an Instance of a Help Document Item.
        /// </summary>
        public HelpItem() : base()
        {
            if (HelpId is null) { HelpId = Guid.NewGuid(); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(HelpId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(HelpSubject), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(HelpToolTip), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(HelpText), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NameSpace), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ModifiedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ModifiedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public Boolean Validate()
        {
            Boolean result = false;

            if (String.IsNullOrWhiteSpace(HelpText))
            { SetRowError("[HelpText] cannot be empty"); }
            else if (HelpId == Guid.Empty)
            { SetRowError("[HelpId] cannot be empty"); }
            else { result = true; }

            return result;
        }

        #region ISerializable
        /// <summary>
        /// Serialization constructor of a Help Document.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected HelpItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }

    /// <summary>
    /// Help Item Extension
    /// </summary>
    public static class HelpItemExtension
    {
        /// <summary>
        /// Gets the Help Document given the namespace key.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, IHelpKeyNameSpace nameSpace)
        { return source.FirstOrDefault(w => nameSpace.Equals(w)); }

        /// <summary>
        /// Get the Help Document given the Object (converted to a NameSpace)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, object obj)
        { return source.GetSubject(new HelpKeyNameSpace(obj)); }
    }

}
