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
    /// Generic Base class for Help Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class HelpCollection<TItem> : BindingTable<TItem>,
        IReadData, IWriteData,
        IReadData<IHelpKey>, IWriteData<IHelpKey>,
        IRemoveItem<IHelpKey>
        where TItem : HelpItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpKey key)
        { return LoadCommand(connection, (key.HelpId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, string? helpSubject, string? nameSpace) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationHelp]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@HelpSubject", parameters.helpSubject);
            command.AddParameter("@NameSpace", parameters.nameSpace);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IHelpKey key)
        { return SaveCommand(connection, key.HelpId); }

        Command SaveCommand(IConnection connection, Guid? helpId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationHelp]";
            command.AddParameter("@HelpId", helpId);

            IEnumerable<TItem> data = this.Where(w => helpId is null || w.HelpId == helpId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationHelp]", data);
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
            { }

            return result;
        }

        /// <inheritdoc/>
        public void Remove(IHelpKey helpItem)
        {
            HelpKey key = new HelpKey(helpItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Help Items.
    /// </summary>
    public class HelpCollection : HelpCollection<HelpItem>
    { }
}
