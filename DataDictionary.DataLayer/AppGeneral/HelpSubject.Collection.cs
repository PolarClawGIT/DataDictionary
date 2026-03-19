// Ignore Spelling: Utc

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
        IReadTemporal
        where TItem : HelpSubjectItem, IHelpSubjectItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, helpId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpSubjectKey key)
        { return LoadCommand(connection, helpId: key.HelpId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpSubjectKey key, ITemporalKey asOfUtcDate)
        { return LoadCommand(connection, helpId: key.HelpId, asOfUtcDate: asOfUtcDate.AsOfUtcDate); }

        /// <inheritdoc/>
        public Command HistoryCommand(IConnection connection)
        { return LoadCommand(connection, includeHistory: true); }

        Command LoadCommand(IConnection connection, 
            Guid? helpId = null, 
            DateTime? asOfUtcDate = null, Boolean includeHistory = false)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = HelpSubject.GetProcedure;
            command.AddParameter(HelpSubject.Identifier, helpId);
            command.AddParameter(Temporal.AsOfUtcDate, asOfUtcDate);
            command.AddParameter(Temporal.IncludeHistory, includeHistory);

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, helpId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IHelpSubjectKey key)
        { return SaveCommand(connection, helpId: key.HelpId); }

        Command SaveCommand(IConnection connection, Guid? helpId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = HelpSubject.SetProcedure;
            command.AddParameter(HelpSubject.Identifier, helpId);

            IEnumerable<TItem> data = this.Where(w => helpId is null || w.HelpId == helpId);
            command.AddParameter(WriteData.Data, HelpSubject.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public IReadOnlyList<HelpSubjectItem> Validate()
        {
            List<HelpSubjectItem> result = new List<HelpSubjectItem>();

            foreach (HelpSubjectItem item in this)
            {
                item.ClearRowErrors();
                if (!item.Validate())
                { result.Add(item); }
            }

            foreach (HelpSubjectItem item in
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
