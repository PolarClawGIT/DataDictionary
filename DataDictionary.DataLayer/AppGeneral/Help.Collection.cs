using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Generic Base class for Help Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class HelpCollection<TItem> : BindingTable<TItem>,
        IReadData, IWriteData,
        IReadData<IHelpKey>, IWriteData<IHelpKey>,
        IRemoveItem<IHelpKey>,
        ITemporalData, ITemporalData<IHelpKey>
        where TItem : HelpItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpKey key)
        { return LoadCommand(connection, (key.HelpId, null, false, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, false, false)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, true, true)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IHelpKey key)
        { return LoadCommand(connection, (key.HelpId, null, true, true)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, DateTime? asOfUtcDate, Boolean includeHistory, Boolean includeDeleted) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppGeneral].[procGetHelpSubject]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@AsOfUtcDate", parameters.asOfUtcDate);
            command.AddParameter("@IncludeHistory", parameters.includeHistory);
            command.AddParameter("@IncludeDeleted", parameters.includeDeleted);
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
            command.CommandText = "[AppGeneral].[procSetHelpSubject]";
            command.AddParameter("@HelpId", parameters.helpId);

            IEnumerable<TItem> data = this.Where(w => parameters.helpId is null || w.HelpId == parameters.helpId);
            command.AddParameter("@Data", "[AppGeneral].[typeHelpSubject]", data);
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
}
