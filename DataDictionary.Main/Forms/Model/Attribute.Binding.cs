using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class Attribute
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource BindingAttribute { private get; init; }
            public BindingView<AttributeValue> Attribute { get; private set; } =
                new BindingView<AttributeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAlias { private get; init; }
            public BindingView<AttributeAliasValue> Aliases { get; private set; } =
                new BindingView<AttributeAliasValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingSubjectArea { private get; init; }
            public BindingView<AttributeSubjectAreaValue> SubjectAreas { get; private set; } =
                new BindingView<AttributeSubjectAreaValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingProperty { private get; init; }
            public BindingView<AttributePropertyValue> Properties { get; private set; } =
                new BindingView<AttributePropertyValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingDefinition { private get; init; }
            public BindingView<AttributeDefinitionValue> Definitions { get; private set; } =
                new BindingView<AttributeDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            AttributeIndex attributeIndex = new AttributeIndex();
            TemporalIndex? temporalIndex = null;
            IAttribute attributeData = BusinessData.Model.Attributes;

            public FormBinding()
            { }

            public void Init()
            {
                // Note: C# 13 adds "field".
                // This code could then be moved to the BindingHelpSubject init.

                Attribute = new BindingView<AttributeValue>(attributeData.Values, w => attributeIndex.Equals(w));
                Properties = new BindingView<AttributePropertyValue>(attributeData.Properties, w => attributeIndex.Equals(w));
                Aliases = new BindingView<AttributeAliasValue>(attributeData.Aliases, w => attributeIndex.Equals(w));
                SubjectAreas = new BindingView<AttributeSubjectAreaValue>(attributeData.SubjectArea, w => attributeIndex.Equals(w));
                Definitions = new BindingView<AttributeDefinitionValue>(attributeData.Definitions, w => attributeIndex.Equals(w));

                BindingAttribute.DataSource = Attribute;
                BindingProperty.DataSource = Properties;
                BindingAlias.DataSource = Aliases;
                BindingSubjectArea.DataSource = SubjectAreas;
                BindingDefinition.DataSource = Definitions;
            }

            public void SetPosition(IAttributeIndex attribute)
            {
                attributeIndex = new AttributeIndex(attribute);

                Attribute.ListChanged -= OnListChanged;
                BindingAttribute.RaiseListChangedEvents = false;
                BindingProperty.RaiseListChangedEvents = false;
                BindingAlias.RaiseListChangedEvents = false;
                BindingSubjectArea.RaiseListChangedEvents = false;
                BindingDefinition.RaiseListChangedEvents = false;

                Attribute.RaiseListChangedEvents = false;
                Properties.RaiseListChangedEvents = false;
                Aliases.RaiseListChangedEvents = false;
                SubjectAreas.RaiseListChangedEvents = false;
                Definitions.RaiseListChangedEvents = false;

                Attribute = new BindingView<AttributeValue>(attributeData.Values, w => attributeIndex.Equals(w));
                Properties = new BindingView<AttributePropertyValue>(attributeData.Properties, w => attributeIndex.Equals(w));
                Aliases = new BindingView<AttributeAliasValue>(attributeData.Aliases, w => attributeIndex.Equals(w));
                SubjectAreas = new BindingView<AttributeSubjectAreaValue>(attributeData.SubjectArea, w => attributeIndex.Equals(w));
                Definitions = new BindingView<AttributeDefinitionValue>(attributeData.Definitions, w => attributeIndex.Equals(w));

                if (Attribute.Count > 0)
                {
                    BindingAttribute.DataSource = Attribute;
                    BindingProperty.DataSource = Properties;
                    BindingAlias.DataSource = Aliases;
                    BindingSubjectArea.DataSource = SubjectAreas;
                    BindingDefinition.DataSource = Definitions;

                    BindingAttribute.RaiseListChangedEvents = true;
                    BindingProperty.RaiseListChangedEvents = true;
                    BindingAlias.RaiseListChangedEvents = true;
                    BindingSubjectArea.RaiseListChangedEvents = true;
                    BindingDefinition.RaiseListChangedEvents = true;

                    Attribute.RaiseListChangedEvents = true;
                    Properties.RaiseListChangedEvents = true;
                    Aliases.RaiseListChangedEvents = true;
                    SubjectAreas.RaiseListChangedEvents = true;
                    Definitions.RaiseListChangedEvents = true;
                }

                Attribute.ResetBindings();
                Properties.ResetBindings();
                Aliases.ResetBindings();
                SubjectAreas.ResetBindings();
                Definitions.ResetBindings();

                BindingAttribute.MoveFirst(); // For some reason this must be done last or it does not work.
            }

            public void SetPosition(IAttributeIndex attribute, ITemporalIndex temporal)
            {
                SetPosition(attribute);
                temporalIndex = new TemporalIndex(temporal);
            }

            private void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                {
                    // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow.
                    // The exception occurs on empty list and is triggered by the ListChanged Event.
                    // When this event occurs, all BindingSources need to set RaiseListChangedEvents to false.
                    // A related error can occur with DataGridViews when the BindingList has an empty list.

                    BindingAttribute.RaiseListChangedEvents = false;
                }
            }

            public AttributeValue NewValue()
            {
                AttributeValue newValue = new AttributeValue();
                attributeData.Values.Add(newValue);
                SetPosition(newValue);

                return newValue;
            }

            public AttributePropertyValue NewProperty()
            {
                if (TryGetValue(out AttributeValue? value))
                { return new AttributePropertyValue(value); }
                else { throw new InvalidOperationException("Current AttributeValue not defined"); }
            }

            public AttributeDefinitionValue NewDefinition()
            {
                if (TryGetValue(out AttributeValue? value))
                { return new AttributeDefinitionValue(value); }
                else { throw new InvalidOperationException("Current AttributeValue not defined"); }
            }

            public IAliasSubType NewAlias()
            {
                if (TryGetValue(out AttributeValue? value))
                { return new AttributeAliasValue(value); }
                else { throw new InvalidOperationException("Current AttributeValue not defined"); }
                throw new NotImplementedException();
            }

            public void AddSubjectArea(ISubjectAreaIndex subject)
            {
                if (TryGetValue(out AttributeValue? attribute))
                { SubjectAreas.Add(new AttributeSubjectAreaValue(attribute, subject)); }
            }

            public void RemoveSubjectArea(ISubjectAreaIndex subject)
            {
                SubjectAreaIndex key = new SubjectAreaIndex(subject);

                while (SubjectAreas.FirstOrDefault(w => key.Equals(w)) is AttributeSubjectAreaValue item)
                { SubjectAreas.Remove(item); }
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    work.AddRange(attributeData.Delete(attributeIndex));
                    work.AddRange(attributeData.Load(factory, attributeIndex));
                }
                else
                {
                    attributeData = IAttribute.Create();
                    work.AddRange(attributeData.Load(factory, attributeIndex, temporalIndex));
                }

                DoWork(work, onComplete);
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(attributeData.Save(factory, attributeIndex));

                DoWork(work, onComplete);
            }

            public ITemporalData GetTemporal()
            { return attributeData.GetTemporal(attributeIndex); }

            public Boolean TryGetValue([NotNullWhen(true)] out AttributeValue? result)
            {
                if (BindingAttribute.Position >= 0
                    && BindingAttribute.Current is AttributeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void RemoveValue()
            {
                if (TryGetValue(out AttributeValue? value))
                {
                    attributeData.RaiseListChangedEvents = false;
                    attributeData.Remove(value);
                    SetPosition(value);
                }
            }

            public Boolean GetAuthorization(CommandImageType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case CommandImageType.Default: return true;
                    case CommandImageType.Delete: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case CommandImageType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out AttributeValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }


        }
    }
}
