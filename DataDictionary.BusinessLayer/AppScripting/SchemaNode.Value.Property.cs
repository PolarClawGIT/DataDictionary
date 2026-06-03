using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Subtype of SchemaNodeValue for a Property Value.
    /// </summary>
    /// <remarks>This is a wrapper class around SchemaNodeValue</remarks>
    public sealed class SchemaNodePropertyValue: ISchemaNodePropertyValue, IBindingPropertyChanged
    {
        SchemaNodeValue baseValue;

        /// <inheritdoc/>
        public String? NodeName
        {
            get { return baseValue.NodeName; }
            set { baseValue.NodeName = value; }
        }

        /// <inheritdoc/>
        public ScopeType ObjectScope
        {
            get { return baseValue.ObjectScope; }
            set { baseValue.ObjectScope = value; }
        }

        /// <inheritdoc/>
        public Guid? ModelPropertyId
        {
            get { return baseValue.ModelPropertyId; }
            set { baseValue.ModelPropertyId = value; }
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

        internal SchemaNodePropertyValue(SchemaNodeValue value) : base()
        {
            baseValue = value;
            value.PropertyChanged += Value_PropertyChanged;

            void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(ObjectScope) or nameof(ModelPropertyId) or nameof(NodeOrder) or nameof(RenderValueAs))
                { this.OnPropertyChanged(PropertyChanged, e.PropertyName); }
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
