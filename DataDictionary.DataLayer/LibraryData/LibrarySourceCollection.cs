using DataDictionary.DataLayer.ApplicationData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.LibraryData
{
    /// <summary>
    /// Generic List/Collection of the Library Source Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class LibrarySourceCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IReadData<ILibrarySourceKey>, IReadData, IWriteData<IModelKey>, IWriteData<ILibrarySourceKey>
        where TItem : LibrarySourceItem, new()
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
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibrarySource]", this);
            return command;
        }

        Command DeleteCommand(IConnection connection, ILibrarySourceKey sourceKey)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetLibrarySource]";
            command.AddParameter("@LibraryId", sourceKey.LibraryId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibrarySource]", new LibrarySourceCollection());
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of the Library Source Items
    /// </summary>
    public class LibrarySourceCollection : LibrarySourceCollection<LibrarySourceItem> { }
}
