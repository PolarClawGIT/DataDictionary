using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateNode
    {
        class FormBinding
        {

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }
            //public required Action OnRefresh { get; init; }

            public required BindingSource TemplateBinding { private get; init; }
            BindingView<TemplateValue> templates =
                new BindingView<TemplateValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource TemplateNodeBinding { private get; init; }
            BindingList<BindingValue> templateNodes { get; } = new BindingList<BindingValue>();

            BindingView<TemplateAttributeValue> attributeNodes =
                new BindingView<TemplateAttributeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            BindingView<TemplateElementValue> elementNodes =
                new BindingView<TemplateElementValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            ITemplate data = BusinessData.Scripting;

            public FormBinding()
            { }

            public void Load(TemplateIndex template)
            {
                TemplateBinding.RaiseListChangedEvents = false;
                TemplateNodeBinding.RaiseListChangedEvents = false;

                templates = new BindingView<TemplateValue>(data.Templates, w => template.Equals(w));
                attributeNodes = new BindingView<TemplateAttributeValue>(data.Attributes, w => template.Equals(w));
                elementNodes = new BindingView<TemplateElementValue>(data.Elements, w => template.Equals(w));

                templateNodes.Clear();
                foreach (TemplateAttributeValue item in attributeNodes)
                { templateNodes.Add(new BindingValue(item)); }

                foreach (TemplateElementValue item in elementNodes)
                { templateNodes.Add(new BindingValue(item)); }

                TemplateBinding.DataSource = templates;
                TemplateNodeBinding.DataSource = templateNodes;

                TemplateBinding.RaiseListChangedEvents = false;
                TemplateNodeBinding.RaiseListChangedEvents = false;
                TemplateBinding.ResetBindings(false);
                TemplateNodeBinding.ResetBindings(false);
            }

            public void Load(TemplateIndex template, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = BusinessData.Scripting; } });
                work.AddRange(data.Delete(template));
                work.AddRange(data.Load(factory, template));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Load(TemplateIndex template, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.Add(new WorkItem() { DoWork = () => { data = ITemplate.Create(); } });
                work.AddRange(data.Load(factory, template, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(template);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean SetPosition(ITemplateNodeIndex node)
            {
                TemplateNodeIndex key = new TemplateNodeIndex(node);

                if (templateNodes.FirstOrDefault(w => key.Equals(w)) is BindingValue value)
                { TemplateNodeBinding.Position = templateNodes.IndexOf(value); return true; }
                else { return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out ITemplateNodeValue? result)
            {
                if (TemplateNodeBinding.Position >= 0
                    && TemplateNodeBinding.Current is ITemplateNodeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

        }

        class BindingValue : IBindingPropertyChanged,
            ITemplateNodeValue, ITemplateAttributeValue, ITemplateElementValue,
            IKeyEquality<ITemplateNodeIndex>, // IKeyEquality<ITemplateAttributeIndex>, IKeyEquality<ITemplateElementIndex>
            IScopeType, ITemporal
        {
            TemplateAttributeValue? attributeValue;
            TemplateElementValue? elementValue;

            public String? NodeName
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.AttributeName; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.ElementName; }
                    else { return null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.AttributeName = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.ElementName = value; }
                }
            }

            public Int32? RenderOrder
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.RenderOrder; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.RenderOrder; }
                    else { return null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.RenderOrder = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.RenderOrder = value; }
                }
            }

            public TemplateNodeValueAsType RenderValueAs
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.RenderValueAs; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.RenderValueAs; }
                    else { return TemplateNodeValueAsType.none; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.RenderValueAs = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.RenderValueAs = value; }
                }
            }

            public String? FixedValue
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.FixedValue; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.FixedValue; }
                    else { return null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.FixedValue = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.FixedValue = value; }
                }
            }

            public ScopeType ObjectScope
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.ObjectScope; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.ObjectScope; }
                    else { return ScopeType.Null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.ObjectScope = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.ObjectScope = value; }
                }
            }

            public String? ObjectProperty
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.ObjectProperty; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.ObjectProperty; }
                    else { return null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.ObjectProperty = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.ObjectProperty = value; }
                }
            }

            public Guid? ModelPropertyId
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.ModelPropertyId; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.ModelPropertyId; }
                    else { return null; }
                }

                set
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { attribute.ModelPropertyId = value; }
                    else if (elementValue is TemplateElementValue element)
                    { element.ModelPropertyId = value; }
                }
            }

            public Guid? NodeId
            {
                get
                {
                    if (attributeValue is TemplateAttributeValue attribute)
                    { return attribute.AttributeId; }
                    else if (elementValue is TemplateElementValue element)
                    { return element.ElementId; }
                    else { return null; }
                }
            }

            String? DataLayer.AppScript.ITemplateAttributeItem.AttributeName
            {
                get
                {
                    if (attributeValue is DataLayer.AppScript.ITemplateAttributeItem attribute)
                    { return attribute.AttributeName; }
                    else { return null; }
                }
            }

            Guid? DataLayer.AppScript.ITemplateAttributeKey.AttributeId
            {
                get
                {
                    if (attributeValue is DataLayer.AppScript.ITemplateAttributeKey attribute)
                    { return attribute.AttributeId; }
                    else { return null; }
                }
            }

            public Guid? TemplateId
            {
                get
                {
                    if (attributeValue is ITemplateIndex attribute)
                    { return attribute.TemplateId; }
                    else if (elementValue is ITemplateIndex element)
                    { return element.TemplateId; }
                    else { return null; }
                }
            }

            public DataLayer.ITemporal Temporal
            {
                get
                {
                    if (attributeValue is ITemporal attribute)
                    { return attribute.Temporal; }
                    else if (elementValue is ITemporal element)
                    { return element.Temporal; }
                    else { throw new NotImplementedException(); }
                }
            }

            DataIndex IDataValue.Index
            {
                get
                {
                    if (attributeValue is IDataValue attribute)
                    { return attribute.Index; }
                    else if (elementValue is IDataValue element)
                    { return element.Index; }
                    else { throw new NotImplementedException(); }
                }
            }

            String IDataValue.Title { get { return NodeName ?? String.Empty; } }

            public ScopeType Scope
            {
                get
                {
                    if (attributeValue is IScopeType attribute)
                    { return attribute.Scope; }
                    else if (elementValue is IScopeType element)
                    { return element.Scope; }
                    else { return ScopeType.Null; }
                }
            }

            String? DataLayer.AppScript.ITemplateElementItem.ElementName
            {
                get
                {
                    if (elementValue is DataLayer.AppScript.ITemplateElementItem element)
                    { return element.ElementName; }
                    else { return null; }
                }
            }

            Guid? DataLayer.AppScript.ITemplateElementKey.ElementId
            {
                get
                {
                    if (elementValue is DataLayer.AppScript.ITemplateElementItem element)
                    { return element.ElementId; }
                    else { return null; }
                }
            }

            public Guid? ParentElementId
            {
                get
                {
                    if (elementValue is DataLayer.AppScript.ITemplateElementItem element)
                    { return element.ParentElementId; }
                    else { return null; }
                }
                set
                {
                    if (elementValue is TemplateElementValue element)
                    { element.ParentElementId = value; }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            public BindingValue(TemplateAttributeValue attribute)
            {
                attributeValue = attribute;
                attribute.PropertyChanged += Attribute_PropertyChanged;

                void Attribute_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (PropertyChanged is PropertyChangedEventHandler handler)
                    { handler(this, new PropertyChangedEventArgs(e.PropertyName)); }
                }
            }

            public BindingValue(TemplateElementValue element)
            {
                elementValue = element;
                element.PropertyChanged += Element_PropertyChanged;

                void Element_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (PropertyChanged is PropertyChangedEventHandler handler)
                    { handler(this, new PropertyChangedEventArgs(e.PropertyName)); }
                }
            }

            #region IEquatable
            public Boolean Equals(ITemplateNodeIndex? other)
            { return new TemplateNodeIndex(this).Equals(other); }

            public Boolean Equals(BindingValue? other)
            {
                return other is BindingValue value
                    && NodeId is Guid
                    && value.NodeId is Guid
                    && Guid.Equals(NodeId, value.NodeId);
            }

            /// <inheritdoc/>
            public override Boolean Equals(object? obj)
            {
                return obj is BindingValue value
                    && NodeId is Guid
                    && value.NodeId is Guid
                    && Guid.Equals(NodeId, value.NodeId);
            }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, IDataSourceIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, IDataSourceIndex right)
            { return !left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, ITemplateIndex right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, ITemplateIndex right)
            { return !left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator ==(BindingValue left, BindingValue right)
            { return left.Equals(right); }

            /// <inheritdoc/>
            public static Boolean operator !=(BindingValue left, BindingValue right)
            { return !left.Equals(right); }

            /// <inheritdoc/>
            public override Int32 GetHashCode()
            {
                if (NodeId is Guid) { return NodeId.GetHashCode(); }
                else { return Guid.Empty.GetHashCode(); }
            }
            #endregion

        }
    }
}
