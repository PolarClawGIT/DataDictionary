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
        public AttributeIndex AttributeIndex { get; protected set; }

        /// <inheritdoc cref="ITemporal.CreatedOn"/>
        public TemporalIndex AsOfUtcDate { get; protected set; } = new TemporalIndex();

        IAttribute currentData;
        IModel currentModel;

        /// <inheritdoc cref="Attribute.Attributes"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<AttributeValue> Attributes { get; private set; } = null!;

        /// <inheritdoc cref="Attribute.Aliases"/>
        public BindingView<AttributeAliasValue> Aliases { get; private set; } = null!;

        /// <inheritdoc cref="Attribute.Properties"/>
        public BindingView<AttributePropertyValue> Properties { get; private set; } = null!;

        /// <inheritdoc cref="Attribute.Definitions"/>
        public BindingView<AttributeDefinitionValue> Definitions { get; private set; } = null!;

        /// <inheritdoc cref="Attribute.SubjectArea"/>
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; private set; } = null!;

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
            AttributeIndex = new AttributeIndex();
            currentData = model.ModelAttribute;

            StartBinding();

            ModelProperty = new BindingView<PropertyValue>(model.Properties);
            ModelDefinitions = new BindingView<DefinitionValue>(model.Definitions);
        }

        void StartBinding()
        {
            Attributes = new BindingView<AttributeValue>(currentData.Attributes, w => AttributeIndex.Equals(w));
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
        /// Loads the data from the Model
        /// </summary>
        /// <param name="attribute"></param>
        public IReadOnlyList<WorkItem> Load(IAttributeIndex attribute)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = StopBinding });

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    AttributeIndex = new AttributeIndex(attribute);
                    currentData = currentModel.ModelAttribute;
                }
            });

            work.Add(new WorkItem() { DoWork = StartBinding });
            return work;
        }

        /// <summary>
        /// Loads the data from the database to the Model. Rebinds the data to the Model.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex attribute)
        {
            List<WorkItem> work = new List<WorkItem>();
            AttributeIndex = new AttributeIndex(attribute);
            currentData = currentModel.ModelAttribute;
            AsOfUtcDate = new TemporalIndex();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, attribute));
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
            currentData = new Attribute() { Model = currentModel };
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
