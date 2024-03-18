using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Element
    /// </summary>
    public interface IElementData :
        IBindingData<ElementItem>,
        ILoadData, ILoadData<ISchemaKey>,
        ISaveData, ISaveData<ISchemaKey>
    {

    }

    class ElementData : ElementCollection<ElementItem>, IElementData
    {
        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

    }
}
