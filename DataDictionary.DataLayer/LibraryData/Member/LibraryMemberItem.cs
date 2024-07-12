using System.Data;
using System.Runtime.Serialization;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Interface for the Library Member Item
    /// </summary>
    public interface ILibraryMemberItem : ILibraryMemberKey, ILibraryMemberKeyParent, ILibraryMemberKeyName, ILibrarySourceKeyName, ILibraryMemberType, IScopeType
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
        public Guid? LibraryId { get { return GetValue<Guid>(nameof(LibraryId)); } set { SetValue(nameof(LibraryId), value); } }

        /// <inheritdoc/>
        public Guid? MemberId { get { return GetValue<Guid>(nameof(MemberId)); } set { SetValue(nameof(MemberId), value); } }

        /// <inheritdoc/>
        public Guid? MemberParentId { get { return GetValue<Guid>(nameof(MemberParentId)); } set { SetValue(nameof(MemberParentId), value); } }

        /// <inheritdoc/>
        public String? AssemblyName { get { return GetValue(nameof(AssemblyName)); } set { SetValue(nameof(AssemblyName), value); } }

        /// <inheritdoc/>
        public String? MemberNameSpace { get { return GetValue(nameof(MemberNameSpace)); } set { SetValue(nameof(MemberNameSpace), value); } }

        /// <inheritdoc/>
        public String? MemberName { get { return GetValue(nameof(MemberName)); } set { SetValue(nameof(MemberName), value); } }

        /// <inheritdoc/>
        public String? MemberData { get { return GetValue(nameof(MemberData)); } set { SetValue(nameof(MemberData), value); } }

        /// <inheritdoc/>
        public LibraryMemberType MemberType
        {
            get
            {
                String? value = GetValue(nameof(MemberType));
                if (LibraryMemberEnumeration.TryParse(value, null, out LibraryMemberEnumeration? result))
                { return result.Value; }
                else { return LibraryMemberType.Null; }
            }
            set
            {
                if (value is LibraryMemberType.Null)
                { SetValue(nameof(MemberType), null); }
                else { SetValue(nameof(MemberType), LibraryMemberEnumeration.Cast(value).Name); }
            }
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
                    case LibraryMemberType.Parameter: return ScopeType.LibraryTypeParameter;
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
            new DataColumn(nameof(LibraryId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(MemberId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(MemberParentId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AssemblyName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(MemberNameSpace), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(MemberName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(MemberType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(MemberData), typeof(String)){ AllowDBNull = true},
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
