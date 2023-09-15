using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Help
{
    /// <summary>
    /// List of Help Items.
    /// </summary>
    public class HelpList : BindingTable<HelpItem>, IReadData, IWriteData, IValidateList<HelpItem>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, string? helpSubject, string? nameSpace, bool? obsolete) parameters)
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

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationHelp]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationHelp]", this);
            return command;
        }

        /// <inheritdoc/>
        public IReadOnlyList<HelpItem> Validate()
        {
            List<HelpItem> result = new List<HelpItem>();

            foreach (HelpItem item in this)
            {
                item.ClearRowErrors();
                if (!item.Validate())
                { result.Add(item); }
            }

            foreach (HelpItem item in
                this.Where(w =>
                {
                    HelpKeyUnique key = new HelpKeyUnique(w);
                    return (this.Any(r => key.Equals(r) && !ReferenceEquals(w, r)));
                }))
            {
                if (String.IsNullOrWhiteSpace(item.GetRowError()))
                {
                    item.SetRowError("[NameSpace] cannot be duplicate");
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
