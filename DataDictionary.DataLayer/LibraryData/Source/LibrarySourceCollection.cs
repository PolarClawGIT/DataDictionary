using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.LibraryData.Source
{
    /// <summary>
    /// Generic List/Collection of the Library Source Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class LibrarySourceCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>, IReadData<ILibrarySourceKey>, 
        IWriteData<IModelKey>, IWriteData<ILibrarySourceKey>,
        IRemoveItem<LibrarySourceKey>, IRemoveItem<LibrarySourceKeyName>
        where TItem : BindingTableRow, ILibrarySourceItem, ILibrarySourceKey, ILibrarySourceKeyName,  new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey library)
        { return LoadCommand(connection, (null, library.LibraryId, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? libraryId, string? assemblyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetLibrarySource]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@LibraryId", parameters.libraryId);
            command.AddParameter("@AssemblyName", parameters.assemblyName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ILibrarySourceKey sourceKey)
        { return SaveCommand(connection, (null, sourceKey.LibraryId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? libraryId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetLibrarySource]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@LibraryId", parameters.libraryId);

            IEnumerable<TItem> data = this.Where(w => parameters.libraryId is null || w.LibraryId == parameters.libraryId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibrarySource]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(LibrarySourceKey libraryItem)
        {
            LibrarySourceKey key = new LibrarySourceKey(libraryItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(LibrarySourceKeyName libraryItem)
        {
            LibrarySourceKeyName key = new LibrarySourceKeyName(libraryItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of the Library Source Items
    /// </summary>
    public class LibrarySourceCollection : LibrarySourceCollection<LibrarySourceItem> { }
}
