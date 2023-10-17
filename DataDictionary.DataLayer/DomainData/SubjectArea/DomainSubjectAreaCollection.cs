using DataDictionary.DataLayer.ApplicationData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.SubjectArea
{
    /// <summary>
    /// Generic Base class for Domain SubjectArea's
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainSubjectAreaCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>,
        IWriteData<IModelKey>,
        IRemoveData<IDomainSubjectAreaKey>
        where TItem : BindingTableRow, IDomainSubjectAreaItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? SubjectAreaId, string? SubjectAreaTitle, bool? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.SubjectAreaId);
            command.AddParameter("@SubjectAreaTitle", parameters.SubjectAreaTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainSubjectArea]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainSubjectArea]", this);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        { return DeleteCommand(connection, (null, parameters.ModelId)); }

        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? SubjectAreaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteDomainSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.SubjectAreaId);

            return command;
        }

        /// <inheritdoc/>
        public void Remove(IDomainSubjectAreaKey SubjectAreaItem)
        {
            DomainSubjectAreaKey key = new DomainSubjectAreaKey(SubjectAreaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Domain Subject Area's
    /// </summary>
    public class DomainSubjectAreaCollection : DomainSubjectAreaCollection<DomainSubjectAreaItem>
    { }
}
