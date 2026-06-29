using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
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

            IAttribute data = BusinessData.Model.Attribute;

            public FormBinding()
            { }

            public void Load(AttributeIndex attribute)
            {
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

                Attribute = new BindingView<AttributeValue>(data.Attributes, w => attribute.Equals(w));
                Properties = new BindingView<AttributePropertyValue>(data.Properties, w => attribute.Equals(w));
                Aliases = new BindingView<AttributeAliasValue>(data.Aliases, w => attribute.Equals(w));
                SubjectAreas = new BindingView<AttributeSubjectAreaValue>(data.SubjectArea, w => attribute.Equals(w));
                Definitions = new BindingView<AttributeDefinitionValue>(data.Definitions, w => attribute.Equals(w));

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

            public void Load(AttributeIndex attribute, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Model.Attribute; } });
                work.AddRange(data.Delete(attribute));
                work.AddRange(data.Load(factory, attribute));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(attribute);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(AttributeIndex attribute, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = IAttribute.Create(BusinessData.Model.Properties, BusinessData.Model.Definitions); } });
                work.AddRange(data.Load(factory, attribute, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(attribute);
                    if (onComplete is not null) { onComplete(args); }
                }
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

            public Boolean TryAddValue([NotNullWhen(true)] out AttributeValue? result)
            {
                if (BusinessData.Authorization.IsModelAdmin
                    || BusinessData.Authorization.IsModelOwner)
                {
                    AttributeValue value = new AttributeValue();
                    data.Attributes.Add(value);
                    result = value; return true;
                }
                else { result = null; return false; }
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

            public void Save(AttributeIndex attribute, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory, attribute));

                DoWork(work, onComplete);
            }

            public ITemporalData GetTemporal(AttributeIndex attribute)
            { return data.GetTemporal(attribute); }

            public Boolean TryGetValue([NotNullWhen(true)] out AttributeValue? result)
            {
                if (BindingAttribute.Position >= 0
                    && BindingAttribute.Current is AttributeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void RemoveValue(AttributeIndex attribute)
            { data.Remove(attribute); }

            public Boolean GetAuthorization(Enumerations.ButtonType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Delete: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.ButtonType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.ButtonType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.ButtonType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.ButtonType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
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

            public XElement GetXElement()
            {
                XElement? build = BusinessData.XmlBuilders.Build<AttributeValue, IAttributeIndex>(
                    ScopeType.ModelAttribute, Attribute,
                        (ScopeType.ModelAttributeProperty, Properties, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c))),
                        (ScopeType.ModelAttributeDefinition, Definitions, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c)))
                    );

                if (build is XElement) { return build; }
                else { return new XElement(ScopeType.ModelAttribute.GetName()); }
            }
        }
    }
}
