// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Wrapper class that returns the BindingViews for the Attribute
    /// </summary>
    public class AttributeView : IView<AttributeIndex, AttributeValue>
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
        AttributeValue IView<AttributeIndex, AttributeValue>.Value
        { get { return Attributes.FirstOrDefault() ?? new AttributeValue(); } }

        /// <inheritdoc/>
        BindingView<AttributeValue> IView<AttributeIndex, AttributeValue>.Values
        { get { return Attributes; } }

        /// <inheritdoc cref="Attribute.Values"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<AttributeValue> Attributes { get; private set; }
            = new BindingView<AttributeValue>(new BindingList<AttributeValue>());

        /// <inheritdoc cref="Attribute.Aliases"/>
        public BindingView<AttributeAliasValue> Aliases { get; private set; }
            = new BindingView<AttributeAliasValue>(new BindingList<AttributeAliasValue>());

        /// <inheritdoc cref="Attribute.Properties"/>
        public BindingView<AttributePropertyValue> Properties { get; private set; }
            = new BindingView<AttributePropertyValue>(new BindingList<AttributePropertyValue>());

        /// <inheritdoc cref="Attribute.Definitions"/>
        public BindingView<AttributeDefinitionValue> Definitions { get; private set; }
            = new BindingView<AttributeDefinitionValue>(new BindingList<AttributeDefinitionValue>());

        /// <inheritdoc cref="Attribute.SubjectArea"/>
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; private set; }
            = new BindingView<AttributeSubjectAreaValue>(new List<AttributeSubjectAreaValue>());

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
        /// </summary>
        protected AttributeView() : base() { }

        /// <summary>
        /// Creates a instance of AttributeView that is empty with Model data.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="definitions"></param>
        /// <param name="subjectAreas"></param>
        protected AttributeView(
            IPropertyData properties,
            IDefinitionData definitions,
            ISubjectAreaData subjectAreas) : this()
        {
            ModelProperty = new BindingView<PropertyValue>(properties);
            ModelDefinitions = new BindingView<DefinitionValue>(definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(subjectAreas);
        }

        /// <summary>
        /// Creates a instance of AttributeView that is bound to the Model.
        /// </summary>
        /// <param name="model"></param>
        public AttributeView(IModel model) : this(model.Properties, model.Definitions, model.SubjectAreas)
        {
            Index = new AttributeIndex();
            currentData = model.Attributes; //TODO: need to make this a Load.

            StartBinding();
        }

        /// <summary>
        /// Creates a instance of AttributeView that is bound to the Model.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="model"></param>
        public AttributeView(IAttributeIndex attribute, IModel model) : this(model.Properties, model.Definitions, model.SubjectAreas)
        {
            Index = new AttributeIndex(attribute);//TODO: need to make this a Load.

            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(model.SubjectAreas);

            currentData = model.Attributes;//TODO: need to make this a Load.

            StartBinding();
        }

        void StartBinding()
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

            Attributes.ListChanged += Attributes_ListChanged;
        }

        private void Attributes_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list.
            if (e.ListChangedType is ListChangedType.ItemDeleted
                && sender is IBindingList values
                && values.Count is 0)
            { StopBinding(); }

            if (e.ListChangedType is ListChangedType.ItemAdded
                 && sender is IEnumerable<IAttributeValue> list
                 && list.FirstOrDefault() is IAttributeValue value
                 && Index.AttributeId == Guid.Empty)
            { Index = new AttributeIndex(value); }
        }

        void StopBinding()
        {
            Attributes.RaiseListChangedEvents = false;
            Aliases.RaiseListChangedEvents = false;
            Properties.RaiseListChangedEvents = false;
            Definitions.RaiseListChangedEvents = false;
            SubjectArea.RaiseListChangedEvents = false;

            Attributes.ListChanged -= Attributes_ListChanged;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, Index));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex(asOfUtcDate);

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, Index, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return currentData.Save(factory, Index); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Delete(Index));
            work.AddRange(currentData.Save(factory, Index));
            work.Add(new WorkItem() { DoWork = StartBinding });

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
