using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
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


    class SchemaData : SchemaCollection<SchemaItem>, ISchemaData, INamedScopeData
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

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
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Scripting Schema",
                DoWork = () =>
                {
                    if (Scripting.Models.FirstOrDefault() is IModelItem model)
                    {
                        ModelKey key = new ModelKey(model);

                        foreach (SchemaItem item in this)
                        {
                            target.Remove(new NamedScopeKey(item));
                            target.Add(new NamedScopeItem(key, item));
                        }
                    }
                }
            });

            return work;
        }
    }
}
