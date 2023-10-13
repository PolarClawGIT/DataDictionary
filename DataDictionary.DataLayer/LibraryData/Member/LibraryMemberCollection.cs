using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.LibraryData.Source;
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
        IRemoveData<ILibrarySourceKey>
        where TItem : BindingTableRow, ILibraryMemberItem, new()
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
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibraryMember]", this);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ILibrarySourceKey libraryItem)
        {
            LibrarySourceKey key = new LibrarySourceKey(libraryItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { this.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of the Library Member Items
    /// </summary>
    public class LibraryMemberCollection : LibraryMemberCollection<LibraryMemberItem> { }
}
