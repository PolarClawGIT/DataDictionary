using DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelIndex : IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : ModelKey
    {
        /// <inheritdoc cref="ModelKey.ModelKey(IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source) { }
    }
}
