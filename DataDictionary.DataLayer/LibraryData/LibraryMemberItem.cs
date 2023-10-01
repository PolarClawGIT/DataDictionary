using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Interface for the Library Member Item
    /// </summary>
    public interface ILibraryMemberItem : ILibraryMemberKey, ILibrarySourceKeyUnique, INotifyPropertyChanged
    {
        /// <summary>
        /// Type of Member, such as the name of the Class, Enum, Method, Property, ...
        /// </summary>
        String? MemberType { get; }

        /// <summary>
        /// Data for the Member.
        /// This is expected to be a XML fragment when generated from Visual studio Document.
        /// </summary>
        String? MemberData { get; }
    }

    /// <summary>
    /// Implementation of the Library Member Item
    /// </summary>
    [Serializable]
    public class LibraryMemberItem : BindingTableRow, ILibraryMemberItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? LibraryId { get { return GetValue<Guid>("LibraryId"); } }

        /// <inheritdoc/>
        public string? AssemblyName { get { return GetValue("AssemblyName"); } set { SetValue("AssemblyName", value); } }

        /// <inheritdoc/>
        public string? MemberNameSpace { get { return GetValue("MemberNameSpace"); } set { SetValue("MemberNameSpace", value); } }

        /// <inheritdoc/>
        public string? MemberName { get { return GetValue("MemberName"); } set { SetValue("MemberName", value); } }

        /// <inheritdoc/>
        public string? MemberType { get { return GetValue("MemberType"); } set { SetValue("MemberType", value); } }

        /// <inheritdoc/>
        public string? MemberData { get { return GetValue("MemberData"); } set { SetValue("MemberData", value); } }

        /// <summary>
        /// Constructor for LibraryMemberItem
        /// </summary>
        public LibraryMemberItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("LibraryId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AssemblyName", typeof(String)){ AllowDBNull = false},
            new DataColumn("MemberNameSpace", typeof(String)){ AllowDBNull = true},
            new DataColumn("MemberName", typeof(String)){ AllowDBNull = false},
            new DataColumn("MemberType", typeof(String)){ AllowDBNull = true},
            new DataColumn("MemberData", typeof(String)){ AllowDBNull = true},
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
        protected LibraryMemberItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (MemberName is not null) { return MemberName; } else { return string.Empty; } }

    }
}
