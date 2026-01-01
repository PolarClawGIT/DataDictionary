using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;
using Toolbox.Threading;
using Attribute = DataDictionary.BusinessLayer.AppModel.Attribute;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Scripting
        /// </summary>
        public IScripting Scripting { get { return scriptingValue; } }
        private readonly Scripting scriptingValue;

        Scripting InitScripting(IModel model)
        {
            Scripting result = new Scripting();
            result.Builders.AddRange(Attribute.CreateXElements(model.Properties.TryGetValue, model.Definitions.TryGetValue));
            result.Builders.AddRange(Entity.CreateXElements(model.Properties.TryGetValue, model.Definitions.TryGetValue));
            result.Builders.AddRange(Process.CreateXElements(model.Properties.TryGetValue, model.Definitions.TryGetValue));
            return result;
        }
    }
}
