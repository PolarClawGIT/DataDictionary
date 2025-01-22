using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Generic List/Collection of the Library Source Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class LibrarySourceCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<ILibrarySourceKey>,
        IWriteData<IModelKey>, IWriteData<ILibrarySourceKey>,
        IRemoveItem<ILibrarySourceKey>, IRemoveItem<ILibrarySourceKeyName>
        where TItem : BindingTableRow, ILibrarySourceItem, ILibrarySourceKey, ILibrarySourceKeyName, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, modelId: null, libraryId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key, DateTime asOfUtcDate)
        { throw new NotImplementedException(); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey library)
        { return LoadCommand(connection, libraryId: library.LibraryId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey key, DateTime asOfUtcDate)
        { throw new NotImplementedException(); }

        Command LoadCommand(IConnection connection, Guid? modelId = null, Guid? libraryId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = LibrarySource.GetProcedure;
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
            command.CommandText = LibrarySource.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(LibrarySource.LibraryId, libraryId);

            IEnumerable<TItem> data = this.Where(w => libraryId is null || w.LibraryId == libraryId);
            command.AddParameter(WriteData.Data, LibrarySource.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(ILibrarySourceKey libraryItem)
        {
            LibrarySourceKey key = new LibrarySourceKey(libraryItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(ILibrarySourceKeyName libraryItem)
        {
            LibrarySourceKeyName key = new LibrarySourceKeyName(libraryItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }


    }
}
