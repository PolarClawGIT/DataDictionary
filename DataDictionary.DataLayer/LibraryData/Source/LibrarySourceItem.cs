using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData.Source
{
    /// <summary>
    /// Interface for the Library Source Item
    /// </summary>
    public interface ILibrarySourceItem : ILibrarySourceKey, ILibrarySourceKeyUnique, IDataItem
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
        public Guid? LibraryId { get { return GetValue<Guid>("LibraryId"); } protected set { SetValue("LibraryId", value); } }

        /// <inheritdoc/>
        public string? AssemblyName { get { return GetValue("AssemblyName"); } set { SetValue("AssemblyName", value); } }

        /// <inheritdoc/>
        public string? LibraryTitle { get { return GetValue("LibraryTitle"); } set { SetValue("LibraryTitle", value); } }

        /// <inheritdoc/>
        public string? LibraryDescription { get { return GetValue("LibraryDescription"); } set { SetValue("LibraryDescription", value); } }

        /// <inheritdoc/>
        public string? SourceFile { get { return GetValue("SourceFile"); } set { SetValue("SourceFile", value); } }

        /// <inheritdoc/>
        public DateTime? SourceDate { get { return GetValue<DateTime>("SourceDate"); } set { SetValue("SourceDate", value); } }

         /// <summary>
        /// Constructor for LibraryMemberItem
        /// </summary>
        public LibrarySourceItem() : base()
        {
            LibraryId = Guid.NewGuid();
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("LibraryId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("LibraryTitle", typeof(string)){ AllowDBNull = true},
            new DataColumn("LibraryDescription", typeof(string)){ AllowDBNull = true},
            new DataColumn("AssemblyName", typeof(string)){ AllowDBNull = true},
            new DataColumn("SourceFile", typeof(string)){ AllowDBNull = true},
            new DataColumn("SourceDate", typeof(DateTime)){ AllowDBNull = true},
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
