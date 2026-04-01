using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.Obsolete;
using DataDictionary.Resource;
using Attribute = DataDictionary.BusinessLayer.AppModel.Attribute;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Scripting Templates
        /// </summary>
        public AppScripting.ITemplateData Templates { get { return templateValues; } }
        AppScripting.TemplateData templateValues;

        /// <summary>
        /// Wrapper for the Scripting
        /// </summary>
        [Obsolete]
        public IScripting Scripting { get { return scriptingValue; } }
        private readonly Scripting scriptingValue;

        [Obsolete]
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
