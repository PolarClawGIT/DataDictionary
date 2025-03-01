// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.AppModel;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Wrapper class that returns the BindingViews for the Attribute
    /// </summary>
    public class AttributeView
    {
        /// <summary>
        /// The current set of data being worked with.
        /// </summary>
        IAttribute currentData;

        /// <summary>
        /// Connection to the Model Data
        /// </summary>
        IAttribute modelData;

        /// <inheritdoc cref="Attribute.Values"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<AttributeValue> Attributes { get; private set; }
            = new BindingView<AttributeValue>(new BindingList<AttributeValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <inheritdoc cref="Attribute.Aliases"/>
        public BindingView<AttributeAliasValue> Aliases { get; private set; }
            = new BindingView<AttributeAliasValue>(new BindingList<AttributeAliasValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <inheritdoc cref="Attribute.Properties"/>
        public BindingView<AttributePropertyValue> Properties { get; private set; }
            = new BindingView<AttributePropertyValue>(new BindingList<AttributePropertyValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <inheritdoc cref="Attribute.Definitions"/>
        public BindingView<AttributeDefinitionValue> Definitions { get; private set; }
            = new BindingView<AttributeDefinitionValue>(new BindingList<AttributeDefinitionValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <inheritdoc cref="Attribute.SubjectArea"/>
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; private set; }
            = new BindingView<AttributeSubjectAreaValue>(new List<AttributeSubjectAreaValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <inheritdoc cref="IModel.Properties"/>
        public IReadOnlyList<PropertyValue> ModelProperty { get; }
            = new BindingView<PropertyValue>(new BindingList<PropertyValue>());

        /// <inheritdoc cref="IModel.Definitions"/>
        public IReadOnlyList<DefinitionValue> ModelDefinitions { get; }
            = new BindingView<DefinitionValue>(new BindingList<DefinitionValue>());

        /// <inheritdoc cref="IModel.SubjectAreas"/>
        public IReadOnlyList<SubjectAreaValue> ModelSubjectAreas { get; }
            = new BindingView<SubjectAreaValue>(new BindingList<SubjectAreaValue>());

        /// <summary>
        /// Creates an instance of the AttributeView
        /// </summary>
        /// <param name="model"></param>
        public AttributeView(IModel model) : base()
        {
            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(model.SubjectAreas);
            currentData = model.Attributes;
            modelData = model.Attributes;

            CreateViews(new AttributeIndex());
            StartChangedEvents();
        }

        /// <summary>
        /// Event is raised when the Attribute list becomes empty.
        /// </summary>
        /// <remarks>
        /// This addresses invalid operation exception fired by CurrencyManager.FindGoodRow.
        /// The exception occurs on empty list and is triggered by the ListChanged Event.
        /// When this event occurs, all BindingSources need to set RaiseListChangedEvents to false.
        /// A related error can occur with DataGridViews when the BindingList has an empty list.
        /// The code in this class handles RaiseListChangedEvents on the BindingLists.
        /// </remarks>
        public event EventHandler? ListEmpty;

        void CreateViews(IAttributeIndex attribute)
        {
            AttributeIndex key = new AttributeIndex(attribute);
            Attributes.ListChanged -= OnListChanged;

            Attributes = new BindingView<AttributeValue>(currentData.Values, w => key.Equals(w));
            Aliases = new BindingView<AttributeAliasValue>(currentData.Aliases, w => key.Equals(w));
            Properties = new BindingView<AttributePropertyValue>(currentData.Properties, w => key.Equals(w));
            Definitions = new BindingView<AttributeDefinitionValue>(currentData.Definitions, w => key.Equals(w));
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(currentData.SubjectArea, w => key.Equals(w));

            Attributes.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListEmpty is EventHandler handler
                    && e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                { handler(sender, new EventArgs()); }
            }
        }

        void StartChangedEvents()
        {
            Attributes.RaiseListChangedEvents = true;
            Attributes.ResetList();

            Aliases.RaiseListChangedEvents = true;
            Aliases.ResetList();

            Properties.RaiseListChangedEvents = true;
            Properties.ResetList();

            Definitions.RaiseListChangedEvents = true;
            Definitions.ResetList();

            SubjectArea.RaiseListChangedEvents = true;
            SubjectArea.ResetList();
        }

        void StopChangedEvents()
        {
            Attributes.RaiseListChangedEvents = false;
            Aliases.RaiseListChangedEvents = false;
            Properties.RaiseListChangedEvents = false;
            Definitions.RaiseListChangedEvents = false;
            SubjectArea.RaiseListChangedEvents = false;
        }

        /// <summary>
        /// Returns the Temporal Data object for the Attribute
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ITemporalView GetTemporal(IModelIndex model)
        {
            IModelKey key = new ModelIndex(model);

            return new TemporalData<AttributeData, AttributeValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data, key) };
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load()
        {
            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            { return Load(attribute); }
            else { throw new InvalidOperationException("No Attribute found"); }
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IAttributeIndex attribute)
        {
            List<WorkItem> work = new List<WorkItem>();
            AttributeIndex key = new AttributeIndex(attribute);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = modelData });
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            { return Load(factory, attribute); }
            else { throw new InvalidOperationException("No Attribute found"); }
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute)
        {
            List<WorkItem> work = new List<WorkItem>();
            AttributeIndex key = new AttributeIndex(attribute);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = new Attribute() });
            work.AddRange(currentData.Load(factory, key));
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ILoadHistoryData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            { return Load(factory, attribute, asOfUtcDate); }
            else { throw new InvalidOperationException("No Attribute found"); }
        }

        /// <inheritdoc cref="ILoadHistoryData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            AttributeIndex key = new AttributeIndex(attribute);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = new Attribute() });
            work.AddRange(currentData.Load(factory, key, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ISaveData{TKey}"/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            { return currentData.Save(factory, new AttributeIndex(attribute)); }
            else { throw new InvalidOperationException("No Attribute found"); }
        }

        /// <inheritdoc cref="IDeleteData"/>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            {
                AttributeIndex key = new AttributeIndex(attribute);

                work.Add(new WorkItem() { DoWork = StopChangedEvents });
                work.AddRange(currentData.Delete(key));
                work.AddRange(currentData.Save(factory, key));

                work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
                work.Add(new WorkItem() { DoWork = StartChangedEvents });
            }
            else { throw new InvalidOperationException("No Attribute found"); }

            return work;
        }

        /// <inheritdoc cref="IRemoveItem{TKey}"/>
        public void Remove()
        {
            if (Attributes.FirstOrDefault() is IAttributeIndex attribute)
            {
                AttributeIndex key = new AttributeIndex(attribute);

                foreach (AttributeValue item in Attributes.Where(w => key.Equals(w)).ToList())
                { Attributes.Remove(item); }

                foreach (AttributeAliasValue item in Aliases.Where(w => key.Equals(w)).ToList())
                { Aliases.Remove(item); }

                foreach (AttributePropertyValue item in Properties.Where(w => key.Equals(w)).ToList())
                { Properties.Remove(item); }

                foreach (AttributeDefinitionValue item in Definitions.Where(w => key.Equals(w)).ToList())
                { Definitions.Remove(item); }

                foreach (AttributeSubjectAreaValue item in SubjectArea.Where(w => key.Equals(w)).ToList())
                { SubjectArea.Remove(item); }
            }
        }

    }
}
