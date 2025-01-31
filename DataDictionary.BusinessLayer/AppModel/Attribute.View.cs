using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Wrapper class that returns the BindingViews for the Attribute
    /// </summary>
    public class AttributeView
    {
        // POC code.
        // Second try at making an Attribute View

        /// <inheritdoc cref="AttributeIndex"/>
        public AttributeIndex AttributeIndex { get; protected set; }

        IAttribute attributeValue;
        IModel currentModel;

        /// <inheritdoc cref="Attribute.Attributes"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<AttributeValue> Attributes { get; private set; }

        /// <inheritdoc cref="Attribute.Aliases"/>
        public BindingView<AttributeAliasValue> Aliases { get; private set; }

        /// <inheritdoc cref="Attribute.Properties"/>
        public BindingView<AttributePropertyValue> Properties { get; private set; }

        /// <inheritdoc cref="Attribute.Definitions"/>
        public BindingView<AttributeDefinitionValue> Definitions { get; private set; }

        /// <inheritdoc cref="Attribute.SubjectArea"/>
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; private set; }

        /// <inheritdoc cref="IModel.Properties"/>
        public BindingView<PropertyValue> ModelProperty { get; }

        /// <inheritdoc cref="IModel.Definitions"/>
        public BindingView<DefinitionValue> ModelDefinitions { get; }

        /// <summary>
        /// Creates a AttributeView tied to the Model.
        /// </summary>
        /// <param name="model"></param>
        public AttributeView(IModel model)
        {
            currentModel = model;
            AttributeValue value = new AttributeValue();
            AttributeIndex = new AttributeIndex(value);
            attributeValue = model.ModelAttribute;

            Attributes = new BindingView<AttributeValue>(attributeValue.Attributes, w => 1 == 2);
            Aliases = new BindingView<AttributeAliasValue>(attributeValue.Aliases, w => 1 == 2);
            Properties = new BindingView<AttributePropertyValue>(attributeValue.Properties, w => 1 == 2);
            Definitions = new BindingView<AttributeDefinitionValue>(attributeValue.Definitions, w => 1 == 2);
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(attributeValue.SubjectArea, w => 1 == 2);

            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
        }

        void StartBinding()
        {
            Attributes = new BindingView<AttributeValue>(attributeValue.Attributes, w => AttributeIndex.Equals(w));
            Aliases = new BindingView<AttributeAliasValue>(attributeValue.Aliases, w => AttributeIndex.Equals(w));
            Properties = new BindingView<AttributePropertyValue>(attributeValue.Properties, w => AttributeIndex.Equals(w));
            Definitions = new BindingView<AttributeDefinitionValue>(attributeValue.Definitions, w => AttributeIndex.Equals(w));
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(attributeValue.SubjectArea, w => AttributeIndex.Equals(w));

            Attributes.RaiseListChangedEvents = true;
            Attributes.ResetBindings();

            Aliases.RaiseListChangedEvents = true;
            Aliases.ResetBindings();

            Properties.RaiseListChangedEvents = true;
            Properties.ResetBindings();

            Definitions.RaiseListChangedEvents = true;
            Definitions.ResetBindings();

            SubjectArea.RaiseListChangedEvents = true;
            SubjectArea.ResetBindings();
        }

        void StopBinding()
        {
            Attributes.RaiseListChangedEvents = false;
            Aliases.RaiseListChangedEvents = false;
            Properties.RaiseListChangedEvents = false;
            Definitions.RaiseListChangedEvents = false;
            SubjectArea.RaiseListChangedEvents = false;
        }

        public void Load(IAttributeIndex attribute)
        {
            StopBinding();
            AttributeIndex = new AttributeIndex(attribute);
            attributeValue = currentModel.ModelAttribute;
            StartBinding();
        }

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute)
        {
            List<WorkItem> work = new List<WorkItem>();
            attributeValue = currentModel.ModelAttribute;

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(attributeValue.Load(factory, attribute));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute, DateTime asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            attributeValue = new Attribute() { Model = currentModel };

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(attributeValue.Load(factory, attribute, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return attributeValue.Save(factory, AttributeIndex); }

        public IReadOnlyList<WorkItem> Delete()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(attributeValue.Delete(AttributeIndex));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

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
