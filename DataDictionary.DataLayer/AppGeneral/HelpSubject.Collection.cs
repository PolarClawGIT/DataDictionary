using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Generic Base class for Help Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class HelpSubjectCollection<TItem> : BindingTable<TItem>,
        IReadData, IWriteData,
        IReadData<IHelpSubjectKey>, IWriteData<IHelpSubjectKey>,
        IRemoveItem<IHelpSubjectKey>,
        ITemporalData, ITemporalData<IHelpSubjectKey>
        where TItem : HelpItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpSubjectKey key)
        { return LoadCommand(connection, (key.HelpId, null, false)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, false)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, true)); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection, IHelpSubjectKey key)
        { return LoadCommand(connection, (key.HelpId, null, true)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, DateTime? asOfUtcDate, Boolean includeHistory) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = HelpSubject.GetProcedure;
            command.AddParameter(HelpSubject.HelpId, parameters.helpId);
            command.AddParameter(Temporal.AsOfUtcDate, parameters.asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, parameters.includeHistory);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IHelpSubjectKey key)
        { return SaveCommand(connection, (key.HelpId, null)); }

        Command SaveCommand(IConnection connection, (Guid? helpId, Guid? dummy) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = HelpSubject.SetProcedure;
            command.AddParameter(HelpSubject.HelpId, parameters.helpId);

            IEnumerable<TItem> data = this.Where(w => parameters.helpId is null || w.HelpId == parameters.helpId);
            command.AddParameter(WriteData.Data, HelpSubject.TableType, data);
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
                    HelpSubjectKeyNameSpace key = new HelpSubjectKeyNameSpace(w);
                    return this.Any(r => key.Equals(r) && !ReferenceEquals(w, r));
                }))
            { }

            return result;
        }

        /// <inheritdoc/>
        public virtual void Remove(IHelpSubjectKey helpItem)
        {
            HelpSubjectKey key = new HelpSubjectKey(helpItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
