using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface IModelIndex : DbLayer.IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : DbLayer.ModelKey, IModelIndex
    {
        /// <inheritdoc cref="DbLayer.ModelKey.ModelKey(DbLayer.IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source)
        { }
    }
}
