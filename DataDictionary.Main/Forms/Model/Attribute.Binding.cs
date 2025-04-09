using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;
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

            public required BindingSource BindingProperty { private get; init; }
            public BindingView<AttributePropertyValue> Properties { get; private set; } = 
                new BindingView<AttributePropertyValue>([]) 
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAlias { private get; init; }
            public BindingView<AttributeAliasValue> Aliases { get; private set; } = 
                new BindingView<AttributeAliasValue>([]) 
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingSubjectArea { private get; init; }
            public BindingView<AttributeSubjectAreaValue> SubjectAreas { get; private set; } =
                new BindingView<AttributeSubjectAreaValue>([]) 
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

                BindingAttribute.DataSource = Attribute;
                BindingProperty.DataSource = Properties;
                BindingAlias.DataSource = Aliases;
                BindingSubjectArea.DataSource = SubjectAreas;
            }

            public void SetIndex(IAttributeIndex attribute)
            {
                attributeIndex = new AttributeIndex(attribute);

                Attribute.ListChanged -= OnListChanged;
                BindingAttribute.RaiseListChangedEvents = false;
                BindingProperty.RaiseListChangedEvents = false;
                BindingAlias.RaiseListChangedEvents = false;
                BindingSubjectArea.RaiseListChangedEvents = false;

                Attribute.RaiseListChangedEvents = false;
                Properties.RaiseListChangedEvents = false;
                Aliases.RaiseListChangedEvents = false;
                SubjectAreas.RaiseListChangedEvents = false;

                Attribute = new BindingView<AttributeValue>(attributeData.Values, w => attributeIndex.Equals(w));
                Properties = new BindingView<AttributePropertyValue>(attributeData.Properties, w => attributeIndex.Equals(w));
                Aliases = new BindingView<AttributeAliasValue>(attributeData.Aliases, w => attributeIndex.Equals(w));
                SubjectAreas = new BindingView<AttributeSubjectAreaValue>(attributeData.SubjectArea, w => attributeIndex.Equals(w));

                if (Attribute.Count > 0)
                {
                    BindingAttribute.DataSource = Attribute;
                    BindingProperty.DataSource = Properties;
                    BindingAlias.DataSource = Aliases;
                    BindingSubjectArea.DataSource = SubjectAreas;
                    BindingAttribute.Position = 0;
                    Attribute.ListChanged += OnListChanged;

                    BindingAttribute.RaiseListChangedEvents = true;
                    BindingProperty.RaiseListChangedEvents = true;
                    BindingAlias.RaiseListChangedEvents = true;
                    BindingSubjectArea.RaiseListChangedEvents = true;

                    Attribute.RaiseListChangedEvents = true;
                    Properties.RaiseListChangedEvents = true;
                    Aliases.RaiseListChangedEvents = true;
                    SubjectAreas.RaiseListChangedEvents = true;
                }

                Attribute.ResetList();
                Properties.ResetList();
                Aliases.ResetList();
                SubjectAreas.ResetList();
            }

            public void SetIndex(IAttributeIndex attribute, ITemporalIndex temporal)
            {
                SetIndex(attribute);
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
                SetIndex(newValue);

                return newValue;
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                StopBinding();
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

                DoWork(work, StartBinding);

                void StopBinding()
                {
                    BindingAttribute.SuspendBinding();
                    BindingProperty.SuspendBinding();
                    BindingAlias.SuspendBinding();
                    BindingSubjectArea.SuspendBinding();
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    SetIndex(attributeIndex);
                    BindingAttribute.ResumeBinding();
                    BindingProperty.ResumeBinding();
                    BindingAlias.ResumeBinding();
                    BindingSubjectArea.ResumeBinding();

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                StopBinding();
                work.Add(factory.OpenConnection());
                work.AddRange(attributeData.Save(factory, attributeIndex));

                DoWork(work, StartBinding);

                void StopBinding()
                {
                    BindingAttribute.SuspendBinding();
                    BindingProperty.SuspendBinding();
                    BindingAlias.SuspendBinding();
                    BindingSubjectArea.SuspendBinding();

                    temporalIndex = null;
                    attributeData = BusinessData.Model.Attributes;
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    SetIndex(attributeIndex);
                    BindingAttribute.ResumeBinding();
                    BindingProperty.ResumeBinding();
                    BindingAlias.ResumeBinding();
                    BindingSubjectArea.ResumeBinding();

                    if (onComplete is not null) { onComplete(args); }
                }
            }

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
                    attributeData.Remove(value);
                    SetIndex(value);
                }
            }
        }
    }
}
