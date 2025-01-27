// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// A view of a single Attribute and it's supporting data.
    /// </summary>
    /// <remarks>
    /// Class uses BindingView to provide access to the underlining sources.
    /// This is a wrapper around AttributeData.
    /// </remarks>
    public class AttributeView :
        ILoadData<IAttributeIndex>, ISaveData<IAttributeIndex>
    {
        //TODO: This is POC code.
        // The idea is to have a single class that represents all the data used by a given form.
        // The Binding Views would be created here rather then in the Form.
        // This would result in re-thinking how most forms work.
        // The upside is that it would be able to handle Temporal data without the UI
        // being aware of the difference.

        AttributeData attributes;
        IModel currentModel;

        public BindingView<AttributeValue> Attributes { get; protected set; }
        public BindingView<AttributeAliasValue> Aliases { get; protected set; }
        public BindingView<AttributePropertyValue> Properties { get; protected set; }
        public BindingView<AttributeDefinitionValue> Definitions { get; protected set; }
        public BindingView<AttributeSubjectAreaValue> SubjectArea { get; protected set; }

        /// <summary>
        /// Creates an empty AttributeView
        /// </summary>
        /// <param name="model">The current model</param>
        public AttributeView(IModel model) : base()
        {
            currentModel = model;
            attributes = new AttributeData() { Model = model };

            Attributes = new BindingView<AttributeValue>(attributes);
            Aliases = new BindingView<AttributeAliasValue>(attributes.Aliases);
            Properties = new BindingView<AttributePropertyValue>(attributes.Properties);
            Definitions = new BindingView<AttributeDefinitionValue>(attributes.Definitions);
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(attributes.SubjectArea);
        }


        /// <summary>
        /// Loads from the data from the current Model.
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public void Load(IAttributeIndex dataKey)
        {
            AttributeIndex key = new AttributeIndex(dataKey);

            Attributes = new BindingView<AttributeValue>(currentModel.Attributes, w => key.Equals(w));
            Aliases = new BindingView<AttributeAliasValue>(currentModel.Attributes.Aliases, w => key.Equals(w));
            Properties = new BindingView<AttributePropertyValue>(currentModel.Attributes.Properties, w => key.Equals(w));
            Definitions = new BindingView<AttributeDefinitionValue>(currentModel.Attributes.Definitions, w => key.Equals(w));
            SubjectArea = new BindingView<AttributeSubjectAreaValue>(currentModel.Attributes.SubjectArea, w => key.Equals(w));
        }

        /// <inheritdoc/>
        /// <remarks>AttributeView</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey)
        { return attributes.Load(factory, dataKey); }

        /// <inheritdoc/>
        /// <remarks>AttributeView</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IAttributeIndex dataKey, DateTime asOfUtcDate)
        { return attributes.Load(factory, dataKey, asOfUtcDate); }

        /// <inheritdoc/>
        /// <remarks>AttributeView</remarks>
        public IReadOnlyList<WorkItem> Delete(IAttributeIndex dataKey)
        { return attributes.Delete(dataKey); }

        /// <inheritdoc/>
        /// <remarks>AttributeView</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return attributes.Delete(); }

        /// <inheritdoc/>
        /// <remarks>AttributeView</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IAttributeIndex dataKey)
        { return attributes.Save(factory, dataKey); }
    }
}
