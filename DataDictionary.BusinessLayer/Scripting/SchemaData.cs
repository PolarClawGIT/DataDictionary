using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Schema
    /// </summary>
    public interface ISchemaData :
        IBindingData<SchemaItem>,
        ILoadData, ILoadData<ISchemaKey>,
        ISaveData, ISaveData<ISchemaKey>
    { }

   
    class SchemaData : SchemaCollection<SchemaItem>, ISchemaData, INamedScopeData<IModelKey>
    {
        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Schema</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target, Func<IModelKey?> parent)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope, Scripting Schema",
                DoWork = () =>
                {
                    if (parent() is IModelKey key)
                    {
                        foreach (SchemaItem item in this)
                        { target.Add(new NamedScopeItem(key, item)); }
                    }
                }
            }.ToList();
        }
    }
}
