using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class ModelDatabaseExtension
    {
        public static IReadOnlyList<WorkItem> LoadModel(this ModelData data, IModelIdentifier modelId)
        { //TODO: Load from Database by Model ID
            return new List<WorkItem>().AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> SaveModel(this ModelData data)
        { //TODO: Save to Database
            return new List<WorkItem>().AsReadOnly();
        }      
    }
}
