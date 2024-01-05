using DataDictionary.BusinessLayer.NameSpace;
using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    public partial class ModelData: IModelNamespace
    {
        /// <inheritdoc/>
        public ModelNameSpaceDictionary ModelNamespace { get; } = new ModelNameSpaceDictionary();
    }
}
