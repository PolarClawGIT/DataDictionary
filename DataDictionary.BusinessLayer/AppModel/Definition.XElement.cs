using DataDictionary.BusinessLayer.AppScripting;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class DefinitionValue 
    {
        [Obsolete("switch to XmlBuilder", true)]
        public static IEnumerable<XElementBuilder> CreateXElements(
            TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        { return XElementBuilder.Create(typeof(DefinitionValue), definitionGet, nameof(DefinitionTitle)); }
    }
}
