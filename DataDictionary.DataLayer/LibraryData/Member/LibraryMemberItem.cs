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
    public interface ILibraryMemberItem : ILibraryMemberKey, ILibrarySourceKeyUnique, IDataItem
    {
        /// <summary>
        /// Type of Member, such as the name of the Class, Enum, Method, Property, ...
        /// </summary>
        string? MemberType { get; }

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
        { if (LibraryId is null) { LibraryId = Guid.NewGuid(); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("LibraryId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AssemblyName", typeof(string)){ AllowDBNull = false},
            new DataColumn("MemberNameSpace", typeof(string)){ AllowDBNull = true},
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

    /// <summary>
    /// List of Member Types
    /// </summary>
    public enum LibraryMemberType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        NULL,

        /// <summary>
        /// .Net NameSpace
        /// </summary>
        NameSpace,

        /// <summary>
        /// A type is a class, interface, struct, enum, or delegate.
        /// </summary>
        Type,

        /// <summary>
        /// A field.
        /// </summary>
        Field,

        /// <summary>
        /// A property. Includes indexers or other indexed properties.
        /// </summary>
        Property,

        /// <summary>
        /// A Method/Function. Includes special methods, such as constructors and operators.
        /// </summary>
        Method,

        /// <summary>
        /// An Event
        /// </summary>
        Event,

        /// <summary>
        /// An Error occurred. The rest of the string provides information about the error. 
        /// </summary>
        Error
    }

    /// <summary>
    /// Extension for the LibraryMember
    /// </summary>
    public static class LibraryMemberExtension
    {
        static Dictionary<LibraryMemberType, (string code, string name)> memberTypeCrossRefrence = new Dictionary<LibraryMemberType, (string code, string name)>
        {
            { LibraryMemberType.NULL,      (string.Empty,string.Empty) },
            { LibraryMemberType.NameSpace, ("N","NameSpace") },
            { LibraryMemberType.Type,      ("T","Type") },
            { LibraryMemberType.Field,     ("F","Field") },
            { LibraryMemberType.Property,  ("P","Property") },
            { LibraryMemberType.Method,    ("M","Method") },
            { LibraryMemberType.Event,     ("E","Event") },
            { LibraryMemberType.Error,     ("!","Error") },
        };

        /// <summary>
        /// Given the LibraryMemberItem, return the member type enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (LibraryMemberType type, string code, string name) MemberItemType(this LibraryMemberItem value)
        {
            if (memberTypeCrossRefrence.FirstOrDefault(w => value.MemberType == w.Value.code || value.MemberType == w.Value.name) is KeyValuePair<LibraryMemberType, (string code, string name)> result)
            { return (result.Key, result.Value.code, result.Value.name); }
            else { return (LibraryMemberType.NULL, memberTypeCrossRefrence[LibraryMemberType.NULL].code, memberTypeCrossRefrence[LibraryMemberType.NULL].name); }
        }
    }
}
