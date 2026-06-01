using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class EntityValue : IXElementFactory
    {
        /// <inheritdoc/>
        public static IEnumerable<XElementBuilder> CreateXElements()
        {
            List<XElementBuilder> result = new List<XElementBuilder>();
            result.AddRange(XElementBuilder.Create(typeof(EntityValue)));

            result.GetValue(nameof(EntityId)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = NodeRenderAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = NodeRenderAsType.none;

            return result;
        }
    }

    partial class Entity
    {
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> propertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelEntity, EntityValue.CreateXElements());
            builders.Add(ScopeType.ModelEntityProperty, EntityPropertyValue.CreateXElements(propertyGet));
            builders.Add(ScopeType.ModelEntityDefinition, EntityDefinitionValue.CreateXElements(definitionGet));

            return builders;
        }
    }
}
