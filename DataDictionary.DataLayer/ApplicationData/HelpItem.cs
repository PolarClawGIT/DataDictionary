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

namespace DataDictionary.DataLayer.ApplicationData
{

    /// <summary>
    /// Interface for a Help Item used for Help Text.
    /// </summary>
    public interface IHelpItem: IHelpKey, IHelpKeyParent, IHelpKeyUnique, IObsolete
    {
        /// <summary>
        /// Title/Subject of the Help Document.
        /// </summary>
        String? HelpSubject { get; }

        /// <summary>
        /// Body of the Help Document
        /// </summary>
        String? HelpText { get; }
    }

    /// <summary>
    /// Help Documentation Item for the Application.
    /// </summary>
    [Serializable]
    public class HelpItem : BindingTableRow, IHelpItem, ISerializable
    {
        /// <inheritdoc/>
        public Nullable<Guid> HelpId { get { return GetValue<Guid>("HelpId"); } protected set { SetValue<Guid>("HelpId", value); } }

        /// <inheritdoc/>
        public Nullable<Guid> HelpParentId { get { return GetValue<Guid>("HelpParentId"); } protected set { SetValue<Guid>("HelpParentId", value); } }

        /// <inheritdoc/>
        public String? HelpSubject { get { return GetValue("HelpSubject"); } set { SetValue("HelpSubject", value); } }

        /// <inheritdoc/>
        public String? HelpText { get { return GetValue("HelpText"); } set { SetValue("HelpText", value); } }

        /// <inheritdoc/>
        public String? NameSpace { get { return GetValue("NameSpace"); } set { SetValue("NameSpace", value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("Obsolete", value); } }

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
            new DataColumn("HelpSubject", typeof(String)){ AllowDBNull = false},
            new DataColumn("HelpText", typeof(String)){ AllowDBNull = false},
            new DataColumn("NameSpace", typeof(String)){ AllowDBNull = false},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Get the Help Documentation from the Application Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? helpId, String? helpSubject, String? nameSpace, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationHelp]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@HelpSubject", parameters.helpSubject);
            command.AddParameter("@NameSpace", parameters.nameSpace);
            command.AddParameter("@Obsolete", parameters.obsolete);
            return command;
        }

        /// <summary>
        /// Stores the Help Documentation to the Application Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Command SetData(IConnection connection, IBindingTable<HelpItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationHelp]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationHelp]", source);
            return command;
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
        public static IHelpItem? GetSubject(this IEnumerable<IHelpItem> source, Object obj)
        { return GetSubject(source,new HelpKeyUnique(obj)); }
    }

}
