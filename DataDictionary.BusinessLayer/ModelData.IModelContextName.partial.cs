using DataDictionary.BusinessLayer.ContextName;

namespace DataDictionary.BusinessLayer
{
    public partial class ModelData: IModelContextName
    {
        /// <inheritdoc/>
        public ContextNameDictionary ContextName { get; } = new ContextNameDictionary();
    }
}
