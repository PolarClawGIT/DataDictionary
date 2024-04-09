using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.LibraryData.Source;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Member Item
    /// </summary>
    public interface ILibraryMemberItem : ILibraryMemberKey, ILibraryMemberKeyParent, ILibraryMemberKeyName, ILibrarySourceKeyName, ILibraryMemberType, IScopeKey
    {

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
        public Guid? LibraryId { get { return GetValue<Guid>("LibraryId"); } set { SetValue("LibraryId", value); } }

        /// <inheritdoc/>
        public Guid? MemberId { get { return GetValue<Guid>("MemberId"); } set { SetValue("MemberId", value); } }

        /// <inheritdoc/>
        public Guid? MemberParentId { get { return GetValue<Guid>("MemberParentId"); } set { SetValue("MemberParentId", value); } }

        /// <inheritdoc/>
        public String? AssemblyName { get { return GetValue("AssemblyName"); } set { SetValue("AssemblyName", value); } }

        /// <inheritdoc/>
        public String? MemberNameSpace { get { return GetValue("MemberNameSpace"); } set { SetValue("MemberNameSpace", value); } }

        /// <inheritdoc/>
        public String? MemberName { get { return GetValue("MemberName"); } set { SetValue("MemberName", value); } }

        /// <inheritdoc/>
        public String? MemberData { get { return GetValue("MemberData"); } set { SetValue("MemberData", value); } }

        /// <inheritdoc/>
        public LibraryMemberType MemberType
        {
            get { return LibraryMemberTypeKey.Parse(GetValue("MemberType") ?? String.Empty).MemberType; }
            set { SetValue("MemberType", value.ToName()); }
        }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (MemberType)
                {
                    case LibraryMemberType.NameSpace: return ScopeType.LibraryNameSpace;
                    case LibraryMemberType.Type: return ScopeType.LibraryType;
                    case LibraryMemberType.Field: return ScopeType.LibraryTypeField;
                    case LibraryMemberType.Property: return ScopeType.LibraryTypeProperty;
                    case LibraryMemberType.Method: return ScopeType.LibraryTypeMethod;
                    case LibraryMemberType.Event: return ScopeType.LibraryTypeEvent;
                    case LibraryMemberType.Parameter: return ScopeType.LibraryMethodParameter;
                    default: return ScopeType.Null;
                }
            }
        }

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
