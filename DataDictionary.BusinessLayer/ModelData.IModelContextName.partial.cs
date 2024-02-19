using DataDictionary.BusinessLayer.ContextName;

namespace DataDictionary.BusinessLayer
{
    public partial class ModelData_Old: IModelContextName
    {
        /// <inheritdoc/>
        public ContextNameDictionary ContextName { get; } = new ContextNameDictionary();
    }
}
