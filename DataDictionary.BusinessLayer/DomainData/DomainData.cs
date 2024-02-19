using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DomainData
{
    /// <summary>
    /// Interface representing Domain data
    /// </summary>
    public interface IDomainData:
           ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        IAttributeData DomainAttributes { get; }

        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        IEntityData DomainEntities { get; }

        /// <summary>
        /// List of Domain Models. (expected, 0 or 1 item)
        /// </summary>
        IModelData DomainModel { get; }
    }

    class DomainData: IDomainData
    {
        /// <inheritdoc/>
        public IAttributeData DomainAttributes { get { return attributes; } }
        private readonly AttributeData attributes;

        /// <inheritdoc/>
        public IEntityData DomainEntities { get { return entites; } }
        private readonly EntityData entites;

        /// <inheritdoc/>
        public IModelData DomainModel { get { return models; } }
        private readonly ModelData models;

        public DomainData() : base ()
        {
            attributes = new AttributeData();
            entites = new EntityData();
            models = new ModelData();
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(models.Load(factory, dataKey));
            work.AddRange(attributes.Load(factory, dataKey));
            work.AddRange(entites.Load(factory, dataKey));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Catalog</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(models.Save(factory, dataKey));
            work.AddRange(attributes.Save(factory, dataKey));
            work.AddRange(entites.Save(factory, dataKey));

            return work;
        }

    }
}
