using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Subtype of SchemaNodeValue for a fixed Value.
    /// </summary>
    /// <remarks>This is a wrapper class around SchemaNodeValue</remarks>
    [Obsolete("Not being supported/needed", true)]
    public sealed class SchemaNodeFixedValue : ISchemaNodeFixedValue, IBindingPropertyChanged
    {
        SchemaNodeValue baseValue;

        /// <inheritdoc/>
        public String? NodeName
        {
            get { return baseValue.NodeName; }
            set { baseValue.NodeName = value; }
        }

        /// <inheritdoc/>
        public String? FixedValue { get; set; }

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

        /// <summary>
        /// Constructor for the SchemaNodeFixedValue.
        /// </summary>
        /// <param name="value"></param>
        internal SchemaNodeFixedValue(SchemaNodeValue value) : base()
        {
            baseValue = value;
            value.PropertyChanged += Value_PropertyChanged;

            void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(NodeName) or nameof(FixedValue) or nameof(NodeOrder) or nameof(RenderValueAs))
                { this.OnPropertyChanged(PropertyChanged, e.PropertyName); }
            }
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
