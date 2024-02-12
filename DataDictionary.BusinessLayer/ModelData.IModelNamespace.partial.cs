using DataDictionary.BusinessLayer.NameSpace;

namespace DataDictionary.BusinessLayer
{
    public partial class ModelData: IModelNamespace
    {
        /// <inheritdoc/>
        public ModelNameSpaceDictionary ModelNamespace { get; } = new ModelNameSpaceDictionary();
    }
}
