using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Transform
    /// </summary>
    public interface ITransformData :
        IBindingData<TransformItem>,
        ILoadData, ILoadData<ITransformKey>,
        ISaveData, ISaveData<ITransformKey>
    { }

    class TransformData : TransformCollection<TransformItem>, ITransformData
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITransformKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITransformKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Scripting Transform",
                DoWork = () =>
                {
                    if (Scripting.Models.FirstOrDefault() is IModelItem model)
                    {
                        ModelKey key = new ModelKey(model);

                        foreach (TransformItem item in this)
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
