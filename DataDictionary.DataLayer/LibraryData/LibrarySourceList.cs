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
    /// List of Library Sources
    /// </summary>
    public class LibrarySourceList : BindingTable<LibrarySourceItem>, IReadData<IModelKey>, IWriteData<IModelKey>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

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
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetLibrarySource]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeLibrarySource]", this);
            return command;
        }
    }
}
