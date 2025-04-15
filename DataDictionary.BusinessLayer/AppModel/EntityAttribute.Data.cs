// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.AppModel;
using System.ComponentModel;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAttributeData :
        IBindingData<EntityAttributeValue>,
        IRemoveItem<IEntityKey>
    { }

    /// <inheritdoc/>
    class EntityAttributeData : EntityAttributeCollection<EntityAttributeValue>,
        IEntityAttributeData,
        ILoadData<IEntityIndex>, ISaveData<IEntityIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <summary>
        /// Reference to the AttributeData that is used to get the Attribute Details.
        /// </summary>
        internal IAttributeData? Attributes
        {
            get { return attributeValues; }
            set
            {
                if (attributeValues is not null)
                { attributeValues.ListChanged -= AttributeValues_ListChanged; }

                attributeValues = value;

                if (attributeValues is not null)
                { attributeValues.ListChanged += AttributeValues_ListChanged; }

                SetAttribute();

                void AttributeValues_ListChanged(Object? sender, ListChangedEventArgs e)
                {   // Brute Force, do them all.
                    SetAttribute();
                }
            }
        }
        IAttributeData? attributeValues;

        void SetAttribute()
        {
            foreach (EntityAttributeValue item in this)
            { item.Attribute = FindAttribute(item); }
        }

        IAttributeValue? FindAttribute(IEntityAttributeItem entity)
        {
            if (attributeValues is not null)
            {
                PathIndex path = new PathIndex(entity.AttributePath);
                if (attributeValues.FirstOrDefault(w => path.Equals(w.AttributePath)) is IAttributeValue value)
                { return value; }
                else { return null; }
            }
            else { return null; }
        }

        protected override Object? AddNewCore()
        {
            var newValue = base.AddNewCore();

            if (newValue is EntityAttributeValue value)
            { value.Attribute = FindAttribute(value); }

            return newValue;
        }

        #region ILoadData,ISaveData
        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateLoad(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IEntityIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IEntityKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IEntityIndex dataKey)
        { return factory.CreateSave(this, (IEntityKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IEntityIndex dataKey)
        { return new WorkItem() { WorkName = "Remove EntityAttribute", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>EntityAttribute</remarks>
        public void Remove(IEntityIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>EntityDefinition</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }


        #endregion
    }
}
