using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    /// <summary>
    /// Generic Base class for Library Member Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class LibraryMemberCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<ILibrarySourceKey>,
        IWriteData<IModelKey>, IWriteData<ILibrarySourceKey>,
        IRemoveItem<LibrarySourceKey>, IRemoveItem<LibrarySourceKeyName>
        where TItem : BindingTableRow, ILibraryMemberItem, ILibrarySourceKey, ILibrarySourceKeyName, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ILibrarySourceKey library)
        { return LoadCommand(connection, (null, library.LibraryId)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? libraryId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetLibraryMember]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@LibraryId", parameters.libraryId);
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
            command.CommandText = "[App_DataDictionary].[procSetLibraryMember]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@LibraryId", parameters.libraryId);

            IEnumerable<TItem> data = this.Where(w => parameters.libraryId is null || w.LibraryId == parameters.libraryId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibraryMember]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(LibrarySourceKey libraryKey)
        {
            foreach (TItem item in this.Where(w => libraryKey.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(LibrarySourceKeyName libraryKey)
        {
            foreach (TItem item in this.Where(w => libraryKey.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of the Library Member Items
    /// </summary>
    public class LibraryMemberCollection : LibraryMemberCollection<LibraryMemberItem> { }
}
