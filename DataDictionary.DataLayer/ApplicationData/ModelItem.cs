using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IModelId
    {
        Nullable<Guid> ModelId { get; }
    }

    public interface IModelItem: IModelId
    {

        String? ModelTitle { get; set; }
        String? ModelDescription { get; set; }
    }

    public class ModelItem : BindingTableRow, IModelItem
    {
        public Nullable<Guid> ModelId { get { return GetValue<Guid>("ModelId"); } protected set { SetValue<Guid>("ModelId", value); } }
        public String? ModelTitle { get { return GetValue("ModelTitle"); } set { SetValue("ModelTitle", value); } }
        public String? ModelDescription { get { return GetValue("ModelDescription"); } set { SetValue("ModelTitle", value); } }

        public ModelItem() : base()
        {
            ModelId = Guid.NewGuid();
            ModelTitle = "New Model";
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("ModelId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ModelTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("ModelDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
