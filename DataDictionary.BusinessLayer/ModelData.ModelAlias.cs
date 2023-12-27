using DataDictionary.DataLayer.DomainData.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    public partial class ModelData: IModelAlias
    {
        /// <inheritdoc/>
        public ModelAliasDictionary ModelAlias { get; } = new ModelAliasDictionary();
    }
}
