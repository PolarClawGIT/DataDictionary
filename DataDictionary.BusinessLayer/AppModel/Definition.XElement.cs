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
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            result.Add(new XElementBuilder(
                nameof(IDefinitionValue.DefinitionTitle),
                (source) =>
                {
                    if (source is IDefinitionIndex value
                        && definitionGet(value, out IDefinitionValue? definition)
                        && definition.DefinitionTitle is String)
                    { return definition.DefinitionTitle; }
                    else { return String.Empty; }
                }));

            return result;
        }
    }
}
