using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : ITemplateItem, ITemplateIndex, ITemplateIndexName,
        IScopeType, IDirectoryValue, ITemporal, IAuthorization
    { }

    /// <inheritdoc/>
    public class TemplateValue : TemplateItem, ITemplateValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue
        IDirectoryValue directory; // Backing field for IDirectoryValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTemplate; } }

        /// <inheritdoc/>
        public override DirectoryType RootFolder
        {
            get { return base.RootFolder; }
            set
            {
                base.RootFolder = value;
                // Causes InitialDirectory and RootPath to be adjusted based on change in RootFolder
                InitialDirectory = directory.InitialDirectory;
            }
        }

        /// <inheritdoc/>
        public String InitialDirectory
        {
            get { return directory.InitialDirectory; }
            set { directory.InitialDirectory = value; }
        }

        /// <inheritdoc/>
        public TemplateValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TemplateIndex(this),
                GetPath = () => new PathItem(TemplateTitle ?? Scope.GetName()),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? Scope.GetName(),
                IsPathChanged = (e) => e.PropertyName is nameof(TemplateTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TemplateTitle)
            };

            directory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };
        }

        /// <inheritdoc/>
        Boolean IDirectoryValue.IsValid([NotNullWhen(false)] out Exception? exception)
        { return directory.IsValid(out exception); }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        { return new TemplateIndex(this).GetAuthorization(authorizations); }
    }
}
