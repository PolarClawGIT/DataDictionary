using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{

    /// <inheritdoc/>
    public interface ITemplateObjectValue : ITemplateObjectItem, 
        ITemplateObjectIndex, ITemplateObjectNameIndex, ITemplateIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Returns the Object Name converted to a Path.
        /// </summary>
        PathIndex ObjectPath { get; }
    }

    /// <inheritdoc/>
    public class TemplateObjectValue : TemplateObjectItem, ITemplateObjectValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        public DataIndex Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        public String Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingObject; } }

        /// <inheritdoc/>
        public PathIndex ObjectPath
        {
            get
            {
                return new PathIndex(
                    new PathIndex(PathIndex.Parse(base.ObjectName).ToArray()));
            }
            set
            {
                base.ObjectName = value.MemberFullPath;
                OnPropertyChanged(nameof(ObjectName));
            }
        }

        /// <inheritdoc/>
        public TemplateObjectValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateObjectIndex(this),
                GetTitle = () => ObjectName ?? Scope.GetEnumeration().Name,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(ObjectName)
            };
        }

        /// <inheritdoc cref="TemplateObjectItem.TemplateObjectItem(ITemplateKey)"/>
        public TemplateObjectValue(ITemplateIndex template): base(template)
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateObjectIndex(this),
                GetTitle = () => ObjectName ?? Scope.GetEnumeration().Name,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(ObjectName)
            };
        }
    }
}
