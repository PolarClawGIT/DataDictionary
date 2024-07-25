using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData.Source
{
    /// <summary>
    /// Interface for the Library Source Item
    /// </summary>
    public interface ILibrarySourceItem : ILibrarySourceKey, ILibrarySourceKeyName, IScopeType
    {
        /// <summary>
        /// Title for the Library
        /// </summary>
        string? LibraryTitle { get; }

        /// <summary>
        /// Description for the Library
        /// </summary>
        string? LibraryDescription { get; }

        /// <summary>
        /// File that was used to source the data for the Library
        /// </summary>
        string? SourceFile { get; }

        /// <summary>
        /// Date that when the data was sourced for the Library
        /// </summary>
        DateTime? SourceDate { get; }
    }

    /// <summary>
    /// Implementation of the Library Source Item
    /// </summary>
    [Serializable]
    public class LibrarySourceItem : BindingTableRow, ILibrarySourceItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? LibraryId { get { return GetValue<Guid>(nameof(LibraryId)); } protected set { SetValue(nameof(LibraryId), value); } }

        /// <inheritdoc/>
        public string? AssemblyName { get { return GetValue(nameof(AssemblyName)); } set { SetValue(nameof(AssemblyName), value); } }

        /// <inheritdoc/>
        public string? LibraryTitle { get { return GetValue(nameof(LibraryTitle)); } set { SetValue(nameof(LibraryTitle), value); } }

        /// <inheritdoc/>
        public string? LibraryDescription { get { return GetValue(nameof(LibraryDescription)); } set { SetValue(nameof(LibraryDescription), value); } }

        /// <inheritdoc/>
        public string? SourceFile { get { return GetValue(nameof(SourceFile)); } set { SetValue(nameof(SourceFile), value); } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get { return GetValue<DateTime>(nameof(SourceDate)); } set { SetValue(nameof(SourceDate), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.Library;

        /// <summary>
        /// Constructor for LibraryMemberItem
        /// </summary>
        public LibrarySourceItem() : base()
        { if (LibraryId is null) { LibraryId = Guid.NewGuid(); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(LibraryId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(LibraryTitle), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(LibraryDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(AssemblyName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SourceFile), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SourceDate), typeof(DateTime)){ AllowDBNull = true},
        };


        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected LibrarySourceItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (LibraryTitle is not null) { return LibraryTitle; } else { return string.Empty; } }
    }
}
