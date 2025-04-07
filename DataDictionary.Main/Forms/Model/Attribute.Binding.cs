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
            BindingSource bindingAttribute;
            BindingSource bindingProperty;
            BindingSource bindingAlias;
            BindingSource bindingSubjectArea;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public BindingView<AttributeValue> Attribute { get; private set; }
            public BindingView<AttributePropertyValue> Properties { get; private set; }
            public BindingView<AttributeAliasValue> Aliases { get; private set; }
            public BindingView<AttributeSubjectAreaValue> SubjectAreas { get; private set; }

            AttributeIndex attributeIndex = new AttributeIndex();
            TemporalIndex? temporalIndex = null;
            IAttribute attributeData = BusinessData.Model.Attributes;

            public FormBinding(ref BindingSource attribute,
                                ref BindingSource property,
                                ref BindingSource aliases,
                                ref BindingSource subjectArea)
            {
                bindingAttribute = attribute;
                bindingProperty = property;
                bindingAlias = aliases;
                bindingSubjectArea = subjectArea;

                Attribute = new BindingView<AttributeValue>(attributeData.Values, w => attributeIndex.Equals(w));
                Properties = new BindingView<AttributePropertyValue>(attributeData.Properties, w => attributeIndex.Equals(w));
                Aliases = new BindingView<AttributeAliasValue>(attributeData.Aliases, w => attributeIndex.Equals(w));
                SubjectAreas = new BindingView<AttributeSubjectAreaValue>(attributeData.SubjectArea, w => attributeIndex.Equals(w));

                bindingAttribute.DataSource = Attribute;
                bindingProperty.DataSource = Properties;
                bindingAlias.DataSource = Aliases;
                bindingSubjectArea.DataSource = SubjectAreas;
            }

            public void SetIndex(IAttributeIndex attribute)
            {
                attributeIndex = new AttributeIndex(attribute);

                Attribute.ListChanged -= OnListChanged;
                bindingAttribute.RaiseListChangedEvents = false;
                bindingProperty.RaiseListChangedEvents = false;
                bindingAlias.RaiseListChangedEvents = false;
                bindingSubjectArea.RaiseListChangedEvents = false;

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
                    bindingAttribute.DataSource = Attribute;
                    bindingProperty.DataSource = Properties;
                    bindingAlias.DataSource = Aliases;
                    bindingSubjectArea.DataSource = SubjectAreas;
                    bindingAttribute.Position = 0;
                    Attribute.ListChanged += OnListChanged;

                    bindingAttribute.RaiseListChangedEvents = true;
                    bindingProperty.RaiseListChangedEvents = true;
                    bindingAlias.RaiseListChangedEvents = true;
                    bindingSubjectArea.RaiseListChangedEvents = true;

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

                    bindingAttribute.RaiseListChangedEvents = false;
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
                    bindingAttribute.SuspendBinding();
                    bindingProperty.SuspendBinding();
                    bindingAlias.SuspendBinding();
                    bindingSubjectArea.SuspendBinding();
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    SetIndex(attributeIndex);
                    bindingAttribute.ResumeBinding();
                    bindingProperty.ResumeBinding();
                    bindingAlias.ResumeBinding();
                    bindingSubjectArea.ResumeBinding();

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
                    bindingAttribute.SuspendBinding();
                    bindingProperty.SuspendBinding();
                    bindingAlias.SuspendBinding();
                    bindingSubjectArea.SuspendBinding();

                    temporalIndex = null;
                    attributeData = BusinessData.Model.Attributes;
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    SetIndex(attributeIndex);
                    bindingAttribute.ResumeBinding();
                    bindingProperty.ResumeBinding();
                    bindingAlias.ResumeBinding();
                    bindingSubjectArea.ResumeBinding();

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out AttributeValue? result)
            {
                if (bindingAttribute.Position >= 0
                    && bindingAttribute.Current is AttributeValue value)
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
