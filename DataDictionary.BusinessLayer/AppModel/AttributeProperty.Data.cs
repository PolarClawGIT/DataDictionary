using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Interface component for the Model Attribute Property
    /// </summary>
    public interface IAttributePropertyData : IBindingData<AttributePropertyValue>
    { }

    class AttributePropertyData : AttributePropertyCollection<AttributePropertyValue>, IAttributePropertyData,
        ILoadData<IAttributeKey>, ISaveData<IAttributeKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        public required AttributeData Attributes { get; init; }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove AttributeProperty", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeKey dataKey)
        { return new WorkItem() { WorkName = "Remove AttributeProperty", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>AttributeProperty</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }
    }
}
