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

            result.GetValue(nameof(EntityId)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Scope)).NodeValueAs = TemplateNodeValueAsType.none;
            result.GetValue(nameof(Temporal)).NodeValueAs = TemplateNodeValueAsType.none;

            return result;
        }
    }

    partial class Entity
    {
        public static IXElementBuilderList CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> PropertyGet,
            TryGetValue<IDefinitionIndex, IDefinitionValue> DefinitionGet)
        {
            XElementBuilderList builders = new XElementBuilderList();
            builders.Add(ScopeType.ModelEntity, EntityValue.CreateXElements());
            builders.Add(ScopeType.ModelEntityProperty, EntityPropertyValue.CreateXElements(PropertyGet));
            builders.Add(ScopeType.ModelEntityDefinition, EntityDefinitionValue.CreateXElements(DefinitionGet));

            return builders;
        }
    }
}
