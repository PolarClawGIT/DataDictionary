using DataDictionary.DataLayer.AppGeneral;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Help Security Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class HelpSecurityCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IHelpKey>,
        IWriteData, IWriteData<IHelpKey>,
        IRemoveItem<IHelpKey>
        where TItem : BindingTableRow, IHelpSecurityItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IHelpKey key)
        { return LoadCommand(connection, (key.HelpId, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? helpId, Guid? roleId, Guid? principleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetHelpSecurity]";
            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@RoleId", parameters.roleId);
            command.AddParameter("@PrincipleId", parameters.principleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IHelpKey key)
        { return SaveCommand(connection, (key.HelpId, null, null)); }

        Command SaveCommand(IConnection connection, (Guid? helpId, Guid? roleId, Guid? principleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetHelpSecurity]";
            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@RoleId", parameters.roleId);
            command.AddParameter("@PrincipleId", parameters.principleId);

            IEnumerable<TItem> data = this.Where(w => parameters.roleId is null || w.RoleId == parameters.roleId);
            command.AddParameter("@Data", "[AppSecurity].[typeHelpSecurity]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IHelpKey helpKey)
        {
            HelpKey key = new HelpKey(helpKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
