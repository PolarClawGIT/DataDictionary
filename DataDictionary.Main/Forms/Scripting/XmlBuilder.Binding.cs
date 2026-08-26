using DataDictionary.BusinessLayer.AppScripting;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    /// <summary>
    /// Wrapper list class used with the Data Binding.
    /// </summary>
    class XmlBuilderData : BindingList<XmlBuilderNode>, IBindingList<XmlBuilderNode>
    {   
        public void Load(SchemaDefinitionIndex key, ISchemaNodeData data)
        {
            Clear();

            foreach (var item in BusinessData.Templates.XmlBuilders)
            {
                XmlBuilderIndex builderKey = new XmlBuilderIndex(item);
                XmlBuilderNode newValue;

                if (data.Where(w => key.Equals(w) && builderKey.Equals(w)) is SchemaNodeValue value)
                {   newValue = new XmlBuilderNode(item, value); }
                else { newValue = new XmlBuilderNode(item); }

                Add(newValue);
            }
        }
    }
}
