using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Subtype of SchemaNodeValue for a Object Value.
    /// </summary>
    /// <remarks>This is a wrapper class around SchemaNodeValue</remarks>
    public sealed class SchemaNodeObjectValue : ISchemaNodeObjectValue, IBindingPropertyChanged
    {
        SchemaNodeValue baseValue;

        /// <inheritdoc/>
        public String? NodeName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(baseValue.NodeName))
                { return ObjectScope.GetName(); }
                else { return baseValue.NodeName; }
            }
            set { baseValue.NodeName = value; }
        }

        /// <inheritdoc/>
        public Int32? NodeOrder
        {
            get { return baseValue.NodeOrder; }
            set { baseValue.NodeOrder = value; }
        }

        /// <inheritdoc/>
        public NodeRenderAsType RenderValueAs
        {
            get { return baseValue.RenderValueAs; }
            set { baseValue.RenderValueAs = value; }
        }

        /// <inheritdoc/>
        public ScopeType ObjectScope
        {
            get { return baseValue.ObjectScope; }
            set
            {
                baseValue.ObjectScope = value;
                baseValue.NodeName = null;
            }
        }

        /// <inheritdoc/>
        public String? ObjectProperty
        {
            get { return baseValue.ObjectProperty; }
            set { baseValue.ObjectProperty = value; }
        }


        internal SchemaNodeObjectValue(SchemaNodeValue value) : base()
        {
            baseValue = value;
            value.PropertyChanged += Value_PropertyChanged;

            void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(ObjectScope) or nameof(ObjectProperty) or nameof(NodeName) or nameof(NodeOrder) or nameof(RenderValueAs))
                { this.OnPropertyChanged(PropertyChanged, e.PropertyName); }
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
