using DataDictionary.BusinessLayer.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Model Data (Attribute, Entity, and Process)
        /// </summary>
        public IModel Model { get { return modelValues; } }
        private readonly Model modelValues;
    }
}
