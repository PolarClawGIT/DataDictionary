using DataDictionary.BusinessLayer.AppScripting;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class DefinitionValue : IXElementFactory<IDefinitionIndex, IDefinitionValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class,
        /// configuring how the node retrieves and renders <see cref="DefinitionValue"/>.
        /// </summary>
        /// <param name="definitionGet"></param>
        /// <returns></returns>
        public static IEnumerable<XElementBuilder> CreateXElements(
            TryGetValue<IDefinitionIndex, IDefinitionValue> definitionGet)
        { return XElementBuilder.Create(typeof(DefinitionValue), definitionGet, nameof(DefinitionTitle)); }
    }
}
