using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Interface for a Help Item used for Help Text.
    /// </summary>
    public interface IHelpSubjectItem : IHelpSubjectKey, IHelpSubjectKeyName, IHelpSubjectKeyNameSpace,
        ITemporalItem
    {
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
    public class HelpSubjectItem : BindingTableRow, IHelpSubjectItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? HelpId
        {
            get { return GetValue<Guid>(nameof(HelpId)); }
            protected init { SetValue(nameof(HelpId), value); }
        }

        /// <inheritdoc/>
        public string? HelpSubject
        {
            get { return GetValue(nameof(HelpSubject)); }
            set { SetValue(nameof(HelpSubject), value); }
        }

        /// <inheritdoc/>
        public string? HelpToolTip
        {
            get { return GetValue(nameof(HelpToolTip)); }
            set { SetValue(nameof(HelpToolTip), value); }
        }

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
        public string? NameSpace
        {
            get { return GetValue(nameof(NameSpace)); }
            set { SetValue(nameof(NameSpace), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Creates an Instance of a Help Document Item.
        /// </summary>
        public HelpSubjectItem() : base()
        {
            HelpId = Guid.NewGuid();

            HelpSubject = "(new Help Subject)";
            NameSpace = String.Format("[NewSubject].[{0}]", HelpId);

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(HelpId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(HelpSubject), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(HelpToolTip), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(HelpText), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NameSpace), typeof(String)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ]; 

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
        protected HelpSubjectItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override String ToString()
        { return HelpSubject ?? String.Empty; }
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
        public static IHelpSubjectItem? GetSubject(this IEnumerable<IHelpSubjectItem> source, IHelpSubjectKeyNameSpace nameSpace)
        { return source.FirstOrDefault(w => nameSpace.Equals(w)); }

        /// <summary>
        /// Get the Help Document given the Object (converted to a NameSpace)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHelpSubjectItem? GetSubject(this IEnumerable<IHelpSubjectItem> source, object obj)
        { return source.GetSubject(new HelpSubjectKeyNameSpace(obj)); }
    }

}
