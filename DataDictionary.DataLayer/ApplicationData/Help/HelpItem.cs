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

namespace DataDictionary.DataLayer.ApplicationData.Help
{

    /// <summary>
    /// Interface for a Help Item used for Help Text.
    /// </summary>
    public interface IHelpItem : IHelpKey, IHelpKeyParent, IHelpKeyUnique, IObsolete
    {
        /// <summary>
        /// Title/Subject of the Help Document.
        /// </summary>
        string? HelpSubject { get; }

        /// <summary>
        /// Body of the Help Document
        /// </summary>
        string? HelpText { get; }
    }

    /// <summary>
    /// Help Documentation Item for the Application.
    /// </summary>
    [Serializable]
    public class HelpItem : BindingTableRow, IHelpItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? HelpId { get { return GetValue<Guid>("HelpId"); } protected set { SetValue("HelpId", value); } }

        /// <inheritdoc/>
        public Guid? HelpParentId { get { return GetValue<Guid>("HelpParentId"); } protected set { SetValue("HelpParentId", value); } }

        /// <inheritdoc/>
        public string? HelpSubject { get { return GetValue("HelpSubject"); } set { SetValue("HelpSubject", value); } }

        /// <inheritdoc/>
        public string? HelpText { get { return GetValue("HelpText"); } set { SetValue("HelpText", value); } }

        /// <inheritdoc/>
        public string? NameSpace { get { return GetValue("NameSpace"); } set { SetValue("NameSpace", value); } }

        /// <inheritdoc/>
        public bool? Obsolete { get { return GetValue<bool>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        /// <summary>
        /// Creates an Instance of a Help Document Item.
        /// </summary>
        public HelpItem() : base()
        {
            if (HelpId is null) { HelpId = Guid.NewGuid(); }
            if (Obsolete is null) { Obsolete = false; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("HelpId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("HelpParentId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("HelpSubject", typeof(string)){ AllowDBNull = false},
            new DataColumn("HelpText", typeof(string)){ AllowDBNull = false},
            new DataColumn("NameSpace", typeof(string)){ AllowDBNull = false},
            new DataColumn("Obsolete", typeof(bool)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public Boolean Validate()
        {
            Boolean result = false;

            if (String.IsNullOrWhiteSpace(this.HelpText))
            { SetRowError("[HelpText] cannot be empty"); }
            else if (this.HelpId == Guid.Empty)
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
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, IHelpKeyUnique nameSpace)
        { return source.FirstOrDefault(w => nameSpace.Equals(w)); }

        /// <summary>
        /// Get the Help Document given the Object (converted to a NameSpace)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, object obj)
        { return source.GetSubject(new HelpKeyUnique(obj)); }
    }

}
