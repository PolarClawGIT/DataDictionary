using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeValue : ISchemaNodeItem, ISchemaNodeIndex, ISchemaComposite,
        IScopeType, ITemporal
    {
        /* Not being Supported
        // TODO: Remove?

        /// <inheritdoc cref="SchemaNodeFixedValue"/>
        SchemaNodeFixedValue FixedNodeValue { get; }

        /// <inheritdoc cref="SchemaNodeObjectValue"/>
        SchemaNodeObjectValue ObjectNodeValue { get; }

        /// <inheritdoc cref="SchemaNodeObjectValue"/>
        SchemaNodePropertyValue PropertyNodeValue { get; }

        /// <summary>
        /// Is the Node Value an Object Property.
        /// </summary>
        Boolean IsObjectValue { get; }

        /// <summary>
        /// Is the Node Value an Object Model Property (AppModel.Property)
        /// </summary>
        Boolean IsPropertyValue { get; }

        /// <summary>
        /// Is the Node Value fixed.
        /// </summary>
        Boolean IsFixedValue { get; }

        /// <summary>
        /// Is the Node Name to be Overridden
        /// </summary>
        Boolean IsNameOverride { get; }
        */
    }

    /// <inheritdoc/>
    public class SchemaNodeValue : SchemaNodeItem, ISchemaNodeValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingNode; } }

        /// <summary>
        /// XML Builder for this Node.<br/>
        /// Used to override the default builder and connect the builder to the data object.
        /// </summary>
        /// <remarks>
        /// This may not return the same object on each call.<br/>
        /// If the Object Scope or Property changes, a new XmlBuilder is needed.<br/>
        /// This is dependent on the static delegate TryGetBuilder.</remarks>
        //private XmlBuilder Builder { get; }

        /* Not being supported
        // TODO: Remove?

        /// <inheritdoc/>
        public SchemaNodeFixedValue FixedNodeValue { get; }

        /// <inheritdoc/>
        public SchemaNodeObjectValue ObjectNodeValue { get; }

        /// <inheritdoc/>
        public SchemaNodePropertyValue PropertyNodeValue { get; }

        /// <inheritdoc/>
        public Boolean IsObjectValue
        {
            get { return field; }
            set
            {
                if (value)
                {
                    field = true;
                    IsPropertyValue = false;
                    IsFixedValue = false;

                    FixedValue = String.Empty;
                    PropertyId = null;
                }
                else { field = false; }
                this.OnPropertyChanged(PropertyChanged, nameof(IsObjectValue));
            }
        }

        /// <inheritdoc/>
        public Boolean IsPropertyValue
        {
            get { return field; }
            set
            {
                if (value)
                {
                    field = true;
                    IsObjectValue = false;
                    IsFixedValue = false;

                    ObjectProperty = String.Empty;
                    FixedValue = String.Empty;
                }
                else { field = false; }
                this.OnPropertyChanged(PropertyChanged, nameof(IsPropertyValue));
            }
        }

        /// <inheritdoc/>
        public Boolean IsFixedValue
        {
            get { return field; }
            set
            {
                if (value)
                {
                    field = true;
                    IsObjectValue = false;
                    IsPropertyValue = false;

                    ObjectScope = ScopeType.Null;
                    ObjectProperty = String.Empty;
                    PropertyId = null;
                }
                else { field = false; }
                this.OnPropertyChanged(PropertyChanged, nameof(IsFixedValue));
            }
        }

        /// <inheritdoc/>
        public Boolean IsNameOverride
        {
            get { return field; }
            set
            {
                field = value;
                this.OnPropertyChanged(PropertyChanged, nameof(IsObjectValue));
            }
        }*/

        /// <inheritdoc/>
        public SchemaNodeValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaNodeIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(NodeName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => NodeName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(NodeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(NodeName)
            };

            //Builder = GetBuilder();

            /* Not being supported
            // TODO: Remove?

            FixedNodeValue = new SchemaNodeFixedValue(this);
            ObjectNodeValue = new SchemaNodeObjectValue(this);
            PropertyNodeValue = new SchemaNodePropertyValue(this);

            if (!String.IsNullOrWhiteSpace(ObjectProperty))
            { IsObjectValue = true; }
            else if (new PropertyKey(this).HasValue)
            { IsPropertyValue = true; }
            else if (!String.IsNullOrWhiteSpace(FixedValue))
            { IsFixedValue = true; }

            if(!String.IsNullOrWhiteSpace(FixedValue))
            { IsNameOverride = true; }*/
        }

        /// <inheritdoc cref="SchemaNodeItem.SchemaNodeItem(ITemplateKey, ISchemaDefinitionKey)"/>
        public SchemaNodeValue(ITemplateIndex template, ISchemaDefinitionIndex schema) : base(template, schema)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaNodeIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => NodeName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(NodeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(NodeName)
            };

            //Builder = GetBuilder();


            /* Not being supported
            // TODO: Remove?

            FixedNodeValue = new SchemaNodeFixedValue(this);
            ObjectNodeValue = new SchemaNodeObjectValue(this);
            PropertyNodeValue = new SchemaNodePropertyValue(this);

            if (!String.IsNullOrWhiteSpace(ObjectProperty))
            { IsObjectValue = true; }
            else if (new PropertyKey(this).HasValue)
            { IsPropertyValue = true; }
            else if (!String.IsNullOrWhiteSpace(FixedValue))
            { IsFixedValue = true; }

            if (!String.IsNullOrWhiteSpace(FixedValue))
            { IsNameOverride = true; }*/
        }

        /// <inheritdoc/>
        public override event PropertyChangedEventHandler? PropertyChanged;
    }

}
