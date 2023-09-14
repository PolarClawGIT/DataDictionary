using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Property
{
    /// <summary>
    /// List of Property Items.
    /// </summary>
    public class PropertyList : BindingTable<PropertyItem>, IReadData, IWriteData, IValidateList<PropertyItem>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? PropertyId, String? PropertyTitle, String? PropertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationProperty]";

            command.AddParameter("@PropertyId", parameters.PropertyId);
            command.AddParameter("@PropertyTitle", parameters.PropertyTitle);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationProperty]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationProperty]", this);
            return command;
        }

        /// <inheritdoc/>
        public IReadOnlyList<PropertyItem> Validate()
        {
            List<PropertyItem> result = new List<PropertyItem>();

            foreach (PropertyItem item in this)
            {
                item.ClearRowErrors();
                if (!item.Validate())
                { result.Add(item); }
            }

            foreach (PropertyItem item in
                this.Where(w =>
                {
                    PropertyKeyUnique key = new PropertyKeyUnique(w);
                    return (this.Any(r => key.Equals(r) && !ReferenceEquals(w, r)));
                }))
            {
                if (String.IsNullOrWhiteSpace(item.GetRowError()))
                {
                    item.SetRowError("[PropertyTitle] cannot be duplicate");
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
