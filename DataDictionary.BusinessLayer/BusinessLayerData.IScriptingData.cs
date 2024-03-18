using DataDictionary.BusinessLayer.Scripting;
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
        /// Wrapper for the Catalog (database) Data
        /// </summary>
        public IScriptingEngine ScriptingEngine { get { return scriptingValue; } }
        private readonly ScriptingEngine scriptingValue;

    }
}
