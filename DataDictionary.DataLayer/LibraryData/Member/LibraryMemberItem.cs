using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.LibraryData.Source;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Member Item
    /// </summary>
    public interface ILibraryMemberItem : ILibraryMemberKey, ILibraryMemberKeyParent, ILibraryMemberKeyName, ILibrarySourceKeyName, ILibraryScopeType, IDataItem
    {
        /// <summary>
        /// Data for the Member.
        /// This is expected to be a XML fragment when generated from Visual studio Document.
        /// </summary>
        string? MemberData { get; }
    }

    /// <summary>
    /// Implementation of the Library Member Item
    /// </summary>
    [Serializable]
    public class LibraryMemberItem : BindingTableRow, ILibraryMemberItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? LibraryId { get { return GetValue<Guid>("LibraryId"); } set { SetValue("LibraryId", value); } }

        /// <inheritdoc/>
        public Guid? MemberId { get { return GetValue<Guid>("MemberId"); } set { SetValue("MemberId", value); } }

        /// <inheritdoc/>
        public Guid? MemberParentId { get { return GetValue<Guid>("MemberParentId"); } set { SetValue("MemberParentId", value); } }

        /// <inheritdoc/>
        public string? AssemblyName { get { return GetValue("AssemblyName"); } set { SetValue("AssemblyName", value); } }

        /// <inheritdoc/>
        public string? NameSpace { get { return GetValue("NameSpace"); } set { SetValue("NameSpace", value); } }

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
        { if (MemberId is null) { MemberId = Guid.NewGuid(); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("LibraryId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("MemberId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("MemberParentId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AssemblyName", typeof(string)){ AllowDBNull = false},
            new DataColumn("NameSpace", typeof(string)){ AllowDBNull = true},
            new DataColumn("MemberName", typeof(string)){ AllowDBNull = false},
            new DataColumn("MemberType", typeof(string)){ AllowDBNull = true},
            new DataColumn("MemberData", typeof(string)){ AllowDBNull = true},
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
