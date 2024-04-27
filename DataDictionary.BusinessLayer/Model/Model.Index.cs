<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLayer = DataDictionary.DataLayer.ModelData;
=======
﻿using DataDictionary.DataLayer.ModelData;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IModelIndex : DbLayer.IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : DbLayer.ModelKey, IModelIndex
    {
        /// <inheritdoc cref="DbLayer.ModelKey.ModelKey(DbLayer.IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source)
        { }
=======
    public interface IModelIndex : IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : ModelKey, IModelIndex
    {
        /// <inheritdoc cref="ModelKey.ModelKey(IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source) { }
>>>>>>> RenameIndexValue
    }
}
