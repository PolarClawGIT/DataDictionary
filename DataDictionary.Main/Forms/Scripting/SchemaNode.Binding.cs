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
        class FormBinding : DataModel<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            //public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

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
            }

            public override void Load(SchemaDefinitionIndex key)
            {
                TemplateIndex templateKey;
                SchemaDefinitionIndex schemaKey = new SchemaDefinitionIndex(key);

                SchemaData.LoadBinding(w => key.Equals(w));
                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { templateKey = new TemplateIndex(schemaValue); }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(key), key);
                    throw ex;
                }

                TemplateData.LoadBinding(w => templateKey.Equals(w));
                NodeData.LoadBinding(w => schemaKey.Equals(w));
                OwnerData.LoadBinding(w => schemaKey.Equals(w));
            }

            public override Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TemplateData.TryGetValue(out TemplateValue? _);

                SecurableIndex? templateKey = null;
                if (TemplateData.TryGetValue(out TemplateValue? templateValue))
                { templateKey = new TemplateIndex(templateValue); }

                isGrant = BusinessData.Authorization.IsScriptAdmin
                    || BusinessData.Authorization.IsScriptOwner
                    || BusinessData.Authorization.IsGrant(templateKey);

                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Add: return isGrant;
                    case Enumerations.ButtonType.Delete: return isGrant && isNode;
                    case Enumerations.ButtonType.OpenDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.SaveDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.DeleteDatabase: return isGrant && isNode;
                    case Enumerations.ButtonType.HistoryDatabase: return isGrant && isNode;
                    default: return false;
                }
            }

            public override Boolean GetLocked()
            {
                if (TemplateData.TryGetValue(out TemplateValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }
        }
    }
}
