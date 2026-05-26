using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode
    {
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<SchemaNodeValue> NodeData { get; }
            public DataBinding<SchemaNodeOwnerValue> OwnerData { get; }

            public IXElementBuilderList Builders { get; } = BusinessData.Templates.SchemataNodes.Builders;

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding,
                BindingSource ownerBinding)
            {
                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                NodeData = new DataBinding<SchemaNodeValue>(nodeBinding, () => GetData().SchemataNodes);
                OwnerData = new DataBinding<SchemaNodeOwnerValue>(ownerBinding, () => GetData().SchemataNodeOwners);
                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);
            }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                TemplateIndex templateKey = new TemplateIndex();

                SchemaData.LoadBinding(w => key.Equals(w));
                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { templateKey = new TemplateIndex(schemaValue); }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
                NodeData.LoadBinding(w => key.Equals(w));
                OwnerData.LoadBinding(w => key.Equals(w));
            }
        }
    }
}
