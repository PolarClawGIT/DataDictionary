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
    public class AttributeView : IView<AttributeValue, AttributeIndex>
    {
        /// <inheritdoc/>
        public AttributeIndex Index { get; protected set; } = new AttributeIndex();

        /// <inheritdoc/>
        public TemporalIndex AsOfUtcDate { get; protected set; } = new TemporalIndex();

        /// <summary>
        /// The current set of data being worked with.
        /// </summary>
        IAttribute currentData = new Attribute();

        /// <inheritdoc/>
        AttributeValue IView<AttributeValue, AttributeIndex>.Value
        { get { return Attributes.FirstOrDefault() ?? new AttributeValue(); } }

        /// <inheritdoc/>
        BindingView<AttributeValue> IView<AttributeValue>.Values
        { get { return Attributes; } }

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
        /// Creates a instance of AttributeView that is empty.
        /// Gets rid of IDE290.
        /// </summary>
        protected AttributeView() : base() { }

        /// <summary>
        /// Creates a instance of AttributeView with the static Model data.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="definitions"></param>
        /// <param name="subjectAreas"></param>
        public AttributeView(
            IPropertyData properties,
            IDefinitionData definitions,
            ISubjectAreaData subjectAreas) : this()
        {
            ModelProperty = new BindingView<PropertyValue>(properties);
            ModelDefinitions = new BindingView<DefinitionValue>(definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(subjectAreas);
        }

        /// <summary>
        /// Creates a instance of AttributeView with the static Model data and assigns the Index.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="properties"></param>
        /// <param name="definitions"></param>
        /// <param name="subjectAreas"></param>
        /// <remarks>Preferred constructor</remarks>
        public AttributeView(
            IAttributeIndex attribute,
            IPropertyData properties,
            IDefinitionData definitions,
            ISubjectAreaData subjectAreas) : this(properties, definitions, subjectAreas)
        { Index = new AttributeIndex(attribute); }

        void StartChangedEvents()
        {
            Attributes = new BindingView<AttributeValue>(currentData.Values, w => Index.Equals(w));
            Aliases = new BindingView<AttributeAliasValue>(currentData.Aliases, w => Index.Equals(w));
            Properties = new BindingView<AttributePropertyValue>(currentData.Properties, w => Index.Equals(w));
            Definitions = new BindingView<AttributeDefinitionValue>(currentData.Definitions, w => Index.Equals(w));
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(currentData.SubjectArea, w => Index.Equals(w));

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

            if (Attributes.FirstOrDefault() is AttributeValue value)
            { AsOfUtcDate = new TemporalIndex(value); }
            else { AsOfUtcDate = new TemporalIndex(); }

            Attributes.ListChanged += Attributes_ListChanged;
        }

        void Attributes_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list.
            if (e.ListChangedType is ListChangedType.ItemDeleted
                && sender is IBindingList values
                && values.Count is 0)
            { StopChangedEvents(); }
        }

        void StopChangedEvents()
        {
            Attributes.RaiseListChangedEvents = false;
            Aliases.RaiseListChangedEvents = false;
            Properties.RaiseListChangedEvents = false;
            Definitions.RaiseListChangedEvents = false;
            SubjectArea.RaiseListChangedEvents = false;

            Attributes.ListChanged -= Attributes_ListChanged;
        }

        /// <summary>
        /// Rebinds the instance to an empty Attribute.
        /// </summary>
        public void Bind()
        {   // TODO: Is this needed?
            StopChangedEvents();
            Index = new AttributeIndex();
            AsOfUtcDate = new TemporalIndex();
            currentData = new Attribute();
            StartChangedEvents();
        }

        /// <summary>
        /// Rebinds the instance to the Model Attribute.
        /// </summary>
        /// <param name="values"></param>
        public void Bind(IAttribute values)
        {   // TODO: Is this needed?
            StopChangedEvents();
            currentData = values;
            StartChangedEvents();
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

        /// <summary>
        /// Loads and Binds to the Model Attributes
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Existing Attribute data is lost.</remarks>
        public IReadOnlyList<WorkItem> Load(IAttribute values)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = values });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Existing Attribute data is overwritten.</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(currentData.Load(factory, Index));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Existing Attribute data is overwritten.</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(currentData.Load(factory, Index, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return currentData.Save(factory, Index); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(currentData.Delete(Index));
            work.AddRange(currentData.Save(factory, Index));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        public void Remove()
        {
            foreach (AttributeValue item in Attributes.Where(w => Index.Equals(w)).ToList())
            { Attributes.Remove(item); }

            foreach (AttributeAliasValue item in Aliases.Where(w => Index.Equals(w)).ToList())
            { Aliases.Remove(item); }

            foreach (AttributePropertyValue item in Properties.Where(w => Index.Equals(w)).ToList())
            { Properties.Remove(item); }

            foreach (AttributeDefinitionValue item in Definitions.Where(w => Index.Equals(w)).ToList())
            { Definitions.Remove(item); }

            foreach (AttributeSubjectAreaValue item in SubjectArea.Where(w => Index.Equals(w)).ToList())
            { SubjectArea.Remove(item); }
        }

    }
}
