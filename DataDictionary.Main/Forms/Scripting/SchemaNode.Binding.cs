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
        class FormBinding
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templateValues =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource SchemaBinding { private get; init; }
            BindingView<SchemaDefinitionValue> schemaValues =
                new BindingView<SchemaDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource NodeBinding { private get; init; }
            BindingView<SchemaNodeValue> nodeValues =
                new BindingView<SchemaNodeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource NodeOwnerBinding { private get; init; }
            BindingView<SchemaNodeOwnerValue> nodeOwnerValues =
                new BindingView<SchemaNodeOwnerValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public IXElementBuilderList Builders { get; } = BusinessData.Templates.SchemataNodes.Builders;

            public FormBinding()
            { }

            public void Load(ISchemaDefinitionIndex schema)
            {
                TemplateIndex templateKey;
                SchemaDefinitionIndex schemaKey = new SchemaDefinitionIndex(schema);

                TemplateBinding.RaiseListChangedEvents = false;
                SchemaBinding.RaiseListChangedEvents = false;
                NodeBinding.RaiseListChangedEvents = false;
                NodeOwnerBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                schemaValues.RaiseListChangedEvents = false;
                nodeValues.RaiseListChangedEvents = false;
                nodeOwnerValues.RaiseListChangedEvents = false;

                schemaValues = new BindingView<SchemaDefinitionValue>(GetData().Schemata, w => schemaKey.Equals(w));
                if (schemaValues.FirstOrDefault() is SchemaDefinitionValue value)
                { templateKey = new TemplateIndex(value); }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(schema), schema);
                    throw ex;
                }

                templateValues = new BindingView<TemplateValue>(GetData(), w => templateKey.Equals(w));
                nodeValues = new BindingView<SchemaNodeValue>(GetData().SchemataNodes, w => schemaKey.Equals(w));
                nodeOwnerValues = new BindingView<SchemaNodeOwnerValue>(GetData().SchemataNodeOwners, w => schemaKey.Equals(w));

                if (templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;
                    SchemaBinding.DataSource = schemaValues;
                    NodeBinding.DataSource = nodeValues;
                    NodeOwnerBinding.DataSource = nodeOwnerValues;

                    TemplateBinding.RaiseListChangedEvents = true;
                    SchemaBinding.RaiseListChangedEvents = true;
                    NodeBinding.RaiseListChangedEvents = true;
                    NodeOwnerBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                    schemaValues.RaiseListChangedEvents = true;
                    nodeValues.RaiseListChangedEvents = true;
                    nodeOwnerValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                SchemaBinding.ResetBindings(false);
                NodeBinding.ResetBindings(false);
                NodeOwnerBinding.ResetBindings(false);
                SchemaBinding.MoveFirst();
            }

            public Boolean TryGetValue([NotNullWhen(true)] out TemplateValue? result)
            {
                if (TemplateBinding.Position >= 0
                    && TemplateBinding.Current is TemplateValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out SchemaDefinitionValue? result)
            {
                if (SchemaBinding.Position >= 0
                    && SchemaBinding.Current is SchemaDefinitionValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                Boolean isNode = TryGetValue(out TemplateValue? _);

                SecurableIndex? templateKey = null;
                if (TryGetValue(out TemplateValue? templateValue))
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

            public Boolean GetLocked()
            {
                if (TryGetValue(out TemplateValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }
        }
    }
}
