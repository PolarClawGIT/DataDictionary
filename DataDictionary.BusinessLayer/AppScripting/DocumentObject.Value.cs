using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentObjectValue : IDocumentObjectItem,
        IDocumentIndex, ITemplateObjectIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class DocumentObjectValue : DocumentObjectItem, IDocumentObjectValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingObject; } }

        /// <inheritdoc cref="DocumentObjectItem.ObjectPath"/>
        public new PathItem ObjectPath
        {
            get
            {
                // Changing the property in the base class is not always caught by the OnPropertyChanged.
                // Extra code is needed to check if the data has changed and update the backing field.  
                if (!objectPathValue.MemberFullPath.Equals(base.ObjectPath))
                { objectPathValue = new PathItem(PathItem.Parse(base.ObjectPath).ToArray()); }

                return objectPathValue;
            }
            set
            {
                base.ObjectPath = value.MemberFullPath;
                objectPathValue.Set(value);
                OnPropertyChanged(nameof(base.ObjectPath));
            }
        }
        PathItem objectPathValue = new PathItem();

        /// <inheritdoc/>
        public DocumentObjectValue() : base()
        { }

        /// <inheritdoc cref="DocumentObjectItem.DocumentObjectItem(IDocumentItem)"/>
        public DocumentObjectValue(IDocumentValue document) : base(document)
        { }
    }
}
