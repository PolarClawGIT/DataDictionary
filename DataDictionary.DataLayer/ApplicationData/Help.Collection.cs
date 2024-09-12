using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
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
        { return LoadCommand(connection, (key.HelpId, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, false)); }

        /// <inheritdoc cref="LoadCommand(IConnection)"/>
        public Command LoadCommand(IConnection connection, Boolean includeHistory)
        { return LoadCommand(connection, (null, includeHistory)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, Boolean includeHistory) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_General].[procGetApplicationHelp]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@IncludeHistory", parameters.includeHistory);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IHelpKey key)
        { return SaveCommand(connection, (key.HelpId, null)); }

        Command SaveCommand(IConnection connection, (Guid? helpId, Guid? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_General].[procSetApplicationHelp]";
            command.AddParameter("@HelpId", parameters.helpId);

            IEnumerable<TItem> data = this.Where(w => parameters.helpId is null || w.HelpId == parameters.helpId);
            command.AddParameter("@Data", "[App_General].[typeApplicationHelp]", data);
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
                    HelpKeyNameSpace key = new HelpKeyNameSpace(w);
                    return this.Any(r => key.Equals(r) && !ReferenceEquals(w, r));
                }))
            { }

            return result;
        }

        /// <inheritdoc/>
        public virtual void Remove(IHelpKey helpItem)
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
