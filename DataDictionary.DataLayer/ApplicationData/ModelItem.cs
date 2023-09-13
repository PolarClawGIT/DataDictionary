using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Interface for the Model.
    /// </summary>
    public interface IModelItem : IModelKey, IObsolete
    {
        /// <summary>
        /// Title for the Model.
        /// </summary>
        String? ModelTitle { get; set; }

        /// <summary>
        /// Description for the Model.
        /// </summary>
        String? ModelDescription { get; set; }
    }

    /// <summary>
    /// Implementation of the Model data.
    /// </summary>
    [Serializable]
    public class ModelItem : BindingTableRow, IModelItem, ISerializable
    {
        /// <inheritdoc/>
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } protected set { SetValue<Guid>("ModelId", value); } }

        /// <inheritdoc/>
        public String? ModelTitle { get { return GetValue("ModelTitle"); } set { SetValue("ModelTitle", value); } }

        /// <inheritdoc/>
        public String? ModelDescription { get { return GetValue("ModelDescription"); } set { SetValue("ModelDescription", value); } }

        /// <inheritdoc/>
        public Boolean? Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue("Obsolete", value); } }

        /// <summary>
        /// Constructor for the Model data
        /// </summary>
        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
            Obsolete = false;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ModelTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("ModelDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Get the Model Data given a Model Key  from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="modelIdentifier"></param>
        /// <returns></returns>
        public static Command GetData(IConnection connection, IModelKey modelIdentifier)
        { return GetData(connection, (modelIdentifier.ModelId, null, true)); }

        /// <summary>
        /// Get all the Models from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, true)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? modelTitle, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModel]";

            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@ModelTitle", parameters.modelTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);

            return command;
        }

        /// <summary>
        /// Saves the Model(s) to the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Command SetData(IConnection connection, IBindingTable<ModelItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModel]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", source);

            return command;
        }

        /// <summary>
        /// Saves a single Model to the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Command SetData(IConnection connection, ModelItem item)
        {
            using (BindingTable<ModelItem> source = new BindingTable<ModelItem>() { item })
            {
                Command command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[App_DataDictionary].[procSetModel]";
                command.AddParameter("@ModelId", item.ModelId);
                command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", source);

                return command;
            }
        }

        /// <summary>
        /// Removes the Model from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Command DeleteData(IConnection connection, IModelKey parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteModel]";
            command.AddParameter("@ModelId", parameters.ModelId);

            return command;
        }
        #region ISerializable
        /// <summary>
        /// Serialization constructor for Model.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }
}
