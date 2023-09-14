using DataDictionary.DataLayer.ApplicationData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class ModelFileExtension
    {
        public static IReadOnlyList<WorkItem> SaveModel(this ModelData data, FileInfo file)
        { //TODO: Save to File System
            data.ModelFile = file;

            using (DataSet workset = new DataSet())
            {
                // repeat for each data object
                using (DataTable model = new DataTable())
                { }

                workset.WriteXml(file.FullName, XmlWriteMode.WriteSchema); // Background work
            }

            return new List<WorkItem>().AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> LoadModel(this ModelData data, FileInfo file)
        { //TODO: Load from File System

            using (DataSet workset = new DataSet())
            {
                workset.ReadXml(file.FullName, XmlReadMode.ReadSchema); // background work

                // Repeat for each data object, background work
                if (workset.Tables.Contains(nameof(ModelItem)) && workset.Tables[nameof(ModelItem)] is DataTable source)
                {
                }
            }
            return new List<WorkItem>().AsReadOnly();
        }
    }
}
