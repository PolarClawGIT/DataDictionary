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
    partial class SchemaDefinition
    {
        partial class FormBinding
        {
            public Func<ITemplateData> GetData { get; set; } = () => BusinessData.Templates;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding() : base()
            { }

            public void Load(ISchemaDefinitionIndex schema)
            {
                TemplateIndex templateKey;
                SchemaDefinitionIndex schemaKey = new SchemaDefinitionIndex(schema);

                TemplateBinding.RaiseListChangedEvents = false;
                SchemaBinding.RaiseListChangedEvents = false;
                ObjectBinding.RaiseListChangedEvents = false;

                templateValues.RaiseListChangedEvents = false;
                schemaValues.RaiseListChangedEvents = false;
                objectValues.RaiseListChangedEvents = false;

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
                objectValues = new BindingView<TemplateObjectValue>(GetData().Objects, w => templateKey.Equals(w));

                if (templateValues.Count > 0)
                {
                    TemplateBinding.DataSource = templateValues;
                    SchemaBinding.DataSource = schemaValues;
                    ObjectBinding.DataSource = objectValues;

                    TemplateBinding.RaiseListChangedEvents = true;
                    SchemaBinding.RaiseListChangedEvents = true;
                    ObjectBinding.RaiseListChangedEvents = true;

                    templateValues.RaiseListChangedEvents = true;
                    schemaValues.RaiseListChangedEvents = true;
                    objectValues.RaiseListChangedEvents = true;
                }

                TemplateBinding.ResetBindings(false);
                SchemaBinding.ResetBindings(false);
                ObjectBinding.ResetBindings(false);
                SchemaBinding.MoveFirst();
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
