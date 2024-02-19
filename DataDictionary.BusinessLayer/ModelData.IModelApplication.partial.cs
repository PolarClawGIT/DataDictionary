using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData: IModelApplication
    {
        /// <inheritdoc/>
        public HelpCollection HelpSubjects { get; } = new HelpCollection();

        /// <inheritdoc/>
        public PropertyCollection Properties { get; } = new PropertyCollection();

    }
}
