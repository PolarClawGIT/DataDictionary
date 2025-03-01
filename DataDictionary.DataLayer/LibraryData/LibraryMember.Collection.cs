using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Generic Base class for Library Member Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class LibraryMemberCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ILibrarySourceKey>,
        IWriteData<IModelKey>, IWriteData<ILibrarySourceKey>,
        IRemoveItem<ILibrarySourceKey>, IRemoveItem<ILibrarySourceKeyName>
        where TItem : BindingTableRow, ILibraryMemberItem, ILibrarySourceKey, ILibrarySourceKeyName, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key, ITemporalKey asOfUtcDate)
        { throw new NotImplementedException(); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey library)
        { return LoadCommand(connection, libraryId: library.LibraryId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey key, ITemporalKey asOfUtcDate)
        { throw new NotImplementedException(); }

        Command LoadCommand(IConnection connection, Guid? modelId = null, Guid? libraryId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = LibraryMember.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(LibrarySource.LibraryId, libraryId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ILibrarySourceKey library)
        { return SaveCommand(connection, libraryId: library.LibraryId); }

        Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? libraryId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = LibraryMember.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(LibrarySource.LibraryId, libraryId);

            IEnumerable<TItem> data = this.Where(w => libraryId is null || w.LibraryId == libraryId);
            command.AddParameter(WriteData.Data, LibraryMember.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ILibrarySourceKey libraryKey)
        {
            foreach (TItem item in this.Where(w => libraryKey.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(ILibrarySourceKeyName libraryKey)
        {
            foreach (TItem item in this.Where(w => libraryKey.Equals(w)).ToList())
            { base.Remove(item); }
        }


    }
}
