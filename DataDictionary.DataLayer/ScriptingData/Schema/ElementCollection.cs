using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{

    /// <summary>
    /// Generic Base class for Scripting Schema Element Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ElementCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISchemaKey>, IReadData<IElementKey>,
        IWriteData, IWriteData<ISchemaKey>, IWriteData<IElementKey>,
        IRemoveItem<ISchemaKey>, IRemoveItem<IElementKey>
        where TItem : BindingTableRow, IElementItem, ISchemaKey, IElementKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISchemaKey key)
        { return LoadCommand(connection, (key.SchemaId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IElementKey key)
        { return LoadCommand(connection, (null, key.ElementId)); }

        Command LoadCommand(IConnection connection, (Guid? SchemaId, Guid? ElementId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingSchemaElement]";

            command.AddParameter("@SchemaId", parameters.SchemaId);
            command.AddParameter("@ElementId", parameters.ElementId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISchemaKey key)
        { return SaveCommand(connection, (key.SchemaId, null)); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IElementKey key)
        { return SaveCommand(connection, (null, key.ElementId)); }

        Command SaveCommand(IConnection connection, (Guid? SchemaId, Guid? ElementId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingSchemaElement]";
            command.AddParameter("@SchemaId", parameters.SchemaId);
            command.AddParameter("@ElementId", parameters.ElementId);

            IEnumerable<TItem> data = this.Where(w => 
                (parameters.SchemaId is null || w.SchemaId == parameters.SchemaId)
                && (parameters.ElementId is null || w.ElementId == parameters.ElementId));
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingSchemaElement]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISchemaKey schemaKey)
        {
            SchemaKey key = new SchemaKey(schemaKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Remove(IElementKey elementKey)
        {
            ElementKey key = new ElementKey(elementKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Scripting Schema Element Items.
    /// </summary>
    public class ElementCollection : ElementCollection<ElementItem>
    { }
}
