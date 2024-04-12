using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ScriptingData.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Instance
    /// </summary>
    public interface IInstanceData :
        IBindingData<InstanceItem>,
        ILoadData, ILoadData<ISelectionKey>,
        ISaveData, ISaveData<ISelectionKey>
    {

    }

    class InstanceData : InstanceCollection<InstanceItem>, IInstanceData
    {
        /// <inheritdoc/>
        /// <remarks>Instance</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Instance</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Instance</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Instance</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

    }
}
