using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ModelData;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.ComponentModel;
>>>>>>> RenameIndexValue
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Model
{
    /// <summary>
    /// Model Synchronize value
    /// </summary>
    public class ModelSynchronizeValue : SynchronizeValue<ModelValue>
    {
        /// <inheritdoc cref="ModelItem.ModelTitle"/>
        public String ModelTitle
        {
            get { return Source.ModelTitle ?? String.Empty; }
            set { Source.ModelTitle = value; }
        }

        /// <inheritdoc/>
        public ModelSynchronizeValue(ModelValue data) : base(data)
        { }

        /// <inheritdoc/>
        protected override void Source_OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(ModelTitle))
            { OnPropertyChanged(e.PropertyName); }
        }
    }

    /// <summary>
    /// Model Synchronize to compare what Models are in the Database vs the Model
    /// </summary>
    public class ModelSynchronize : SynchronizeData<ModelSynchronizeValue, ModelValue, ModelIndex>
    {
        /// <summary>
        /// Concrete class for the Abstract DbModelCollection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        class SourceCollection<TValue> : ModelCollection<TValue>
            where TValue : ModelValue, IModelValue, new()
        { }

        /// <inheritdoc/>
        protected override IBindingList<ModelValue> ModelData { get { return businessData.Models; } }
        BusinessLayerData businessData;

        /// <inheritdoc/>
        protected override IBindingList<ModelValue> DatabaseData { get { return sourceData; } }
        SourceCollection<ModelValue> sourceData = new SourceCollection<ModelValue>();

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="businessData"></param>
        public ModelSynchronize(BusinessLayerData businessData) : base()
        {
            this.businessData = businessData;

            foreach (ModelValue item in businessData.Models)
            { Add(new ModelSynchronizeValue(item) { InModel = true }); }
        }

        /// <inheritdoc/>
        protected override ModelIndex GetKey(ModelValue data)
        { return new ModelIndex(data); }

        /// <inheritdoc/>
        protected override ModelSynchronizeValue GetValue(ModelValue data)
        { return new ModelSynchronizeValue(data); }

        /// <summary>
        /// Clears then reloads the Model List from the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> GetModels(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = sourceData.Clear });
            work.Add(factory.CreateLoad(sourceData));
            return work;
        }

        /// <summary>
        /// Loads a Model from the Database (including all schema components)
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> OpenFromDb(IDatabaseWork factory, IModelIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            IModelKey model = new ModelKey(key);

            work.AddRange(businessData.Remove());
            work.AddRange(businessData.Load(factory, key));
            return work;
        }

        /// <summary>
        /// Saves the Model to the Database (including all schema components)
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveToDb(IDatabaseWork factory, IModelIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(businessData.Save(factory, key));
            work.AddRange(GetModels(factory));
            return work;
        }

        /// <summary>
        /// Deletes a Model from the Database (including all schema components).
        /// Copy in the Model is not removed.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> DeleteFromDb(IDatabaseWork factory, IModelIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            IModelKey dbKey = new ModelKey(key);

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    while (sourceData.FirstOrDefault(w => dbKey.Equals(w)) is ModelValue item)
                    { sourceData.Remove(dbKey); }
                }
            });
            work.Add(factory.CreateSave(sourceData, dbKey));
            return work;
        }
    }
}
