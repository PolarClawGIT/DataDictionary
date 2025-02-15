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
    public class AttributeView
    {
        /// <inheritdoc cref="AttributeIndex"/>
        /// <remarks>If GUID.Empty, the index is assigned to the first Attribute added.</remarks>
        public AttributeIndex AttributeIndex { get; protected set; } = new AttributeIndex();

        /// <inheritdoc cref="ITemporal.CreatedOn"/>
        public TemporalIndex AsOfUtcDate { get; protected set; } = new TemporalIndex();

        /// <summary>
        /// The current set of data being worked with;
        /// </summary>
        IAttribute currentData = new Attribute();

        /// <inheritdoc cref="Attribute.Values"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<AttributeValue> Attributes { get; private set; } = new BindingView<AttributeValue>(new BindingList<AttributeValue>());

        /// <inheritdoc cref="Attribute.Aliases"/>
        public BindingView<AttributeAliasValue> Aliases { get; private set; } = new BindingView<AttributeAliasValue>(new BindingList<AttributeAliasValue>());

        /// <inheritdoc cref="Attribute.Properties"/>
        public BindingView<AttributePropertyValue> Properties { get; private set; } = new BindingView<AttributePropertyValue>(new BindingList<AttributePropertyValue>());

        /// <inheritdoc cref="Attribute.Definitions"/>
        public BindingView<AttributeDefinitionValue> Definitions { get; private set; } = new BindingView<AttributeDefinitionValue>(new BindingList<AttributeDefinitionValue>());

        /// <inheritdoc cref="Attribute.SubjectArea"/>
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; private set; } = new BindingView<AttributeSubjectAreaValue>(new List<AttributeSubjectAreaValue>());

        /// <inheritdoc cref="IModel.Properties"/>
        public IReadOnlyList<PropertyValue> ModelProperty { get; } = new BindingView<PropertyValue>(new BindingList<PropertyValue>());

        /// <inheritdoc cref="IModel.Definitions"/>
        public IReadOnlyList<DefinitionValue> ModelDefinitions { get; } = new BindingView<DefinitionValue>(new BindingList<DefinitionValue>());

        /// <inheritdoc cref="IModel.SubjectAreas"/>
        public IReadOnlyList<SubjectAreaValue> ModelSubjectAreas { get; } = new BindingView<SubjectAreaValue>(new BindingList<SubjectAreaValue>());

        /// <summary>
        /// Creates a instance of AttributeView that is bound to the Model.
        /// </summary>
        /// <param name="model"></param>
        public AttributeView(IModel model) : base()
        {
            AttributeIndex = new AttributeIndex();

            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(model.SubjectAreas);

            currentData = model.Attributes;

            StartBinding();
        }

        /// <summary>
        /// Creates a instance of AttributeView that is bound to the Model.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="model"></param>
        public AttributeView(IAttributeIndex attribute, IModel model) : this(model)
        {
            AttributeIndex = new AttributeIndex(attribute);

            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
            ModelSubjectAreas = new BindingView<SubjectAreaValue>(model.SubjectAreas);

            currentData = model.Attributes;

            StartBinding();
        }

        void StartBinding()
        {
            Attributes = new BindingView<AttributeValue>(currentData.Values, w => AttributeIndex.Equals(w));
            Aliases = new BindingView<AttributeAliasValue>(currentData.Aliases, w => AttributeIndex.Equals(w));
            Properties = new BindingView<AttributePropertyValue>(currentData.Properties, w => AttributeIndex.Equals(w));
            Definitions = new BindingView<AttributeDefinitionValue>(currentData.Definitions, w => AttributeIndex.Equals(w));
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(currentData.SubjectArea, w => AttributeIndex.Equals(w));

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
                 && AttributeIndex.AttributeId == Guid.Empty)
            { AttributeIndex = new AttributeIndex(value); }
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

        /// <summary>
        /// Loads the data from the database. The Model is updated from the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, AttributeIndex));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <summary>
        /// Loads the data from the database. Data is not bound to the Model.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="attribute"></param>
        /// <param name="asOfUtcDate"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex(asOfUtcDate);

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, attribute, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <summary>
        /// Saves the data to the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return currentData.Save(factory, AttributeIndex); }

        /// <summary>
        /// Remove the Attribute from the database.
        /// If bound to Model, the Attribute is also removed from the Model.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Delete(AttributeIndex));
            work.AddRange(currentData.Save(factory, AttributeIndex));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <summary>
        /// Removes the Data. If linked to a Model, the data is the Model is also removed.
        /// </summary>
        public void Remove()
        {
            foreach (AttributeValue item in Attributes.Where(w => AttributeIndex.Equals(w)).ToList())
            { Attributes.Remove(item); }

            foreach (AttributeAliasValue item in Aliases.Where(w => AttributeIndex.Equals(w)).ToList())
            { Aliases.Remove(item); }

            foreach (AttributePropertyValue item in Properties.Where(w => AttributeIndex.Equals(w)).ToList())
            { Properties.Remove(item); }

            foreach (AttributeDefinitionValue item in Definitions.Where(w => AttributeIndex.Equals(w)).ToList())
            { Definitions.Remove(item); }

            foreach (AttributeSubjectAreaValue item in SubjectArea.Where(w => AttributeIndex.Equals(w)).ToList())
            { SubjectArea.Remove(item); }
        }

    }
}
