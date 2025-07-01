using System.Data;
using System.Runtime.Serialization;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppLibrary
{
    /// <summary>
    /// Interface for the Library Member Item
    /// </summary>
    public interface ILibraryMemberItem :
        ILibraryMemberKey, ILibraryMemberKeyParent, ILibraryMemberKeyName,
        ILibrarySourceKeyName, ILibraryMemberType,
        ITemporalItem
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
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for LibraryMemberItem
        /// </summary>
        public LibraryMemberItem() : base()
        {
            if (MemberId is null) { MemberId = Guid.NewGuid(); }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(LibraryId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(MemberId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(MemberParentId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AssemblyName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(MemberNameSpace), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(MemberName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(MemberType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(MemberData), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];


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
        { if (MemberName is not null) { return MemberName; } else { return string.Empty; } }
    }


}
