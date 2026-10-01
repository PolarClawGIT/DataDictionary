using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for a class that has a Path and a Scope.
    /// </summary>
    public interface IPathIndex : IKey, IScopeType
    {
        // Todo: Need to setup Alias and Template Objects to be comparable to this.
        // In the end, the goal is to be able to compare any given IPathIndex to a target Path/Scope.
        // This should also get rid of the specialized logic for Alias and Template Objects.

        /// <summary>
        /// The NamedPath for the Value
        /// </summary>
        PathItem Path { get; }
    }

    /// <summary>
    /// PathIndex represents a naming of an object such that it can be searched for across different scopes.
    /// </summary>
    public class PathIndex : IPathIndex,
        IKeyEquality<PathIndex>,
        IKeyEquality<IPathIndex>
    {
        /// <inheritdoc/>
        public PathItem Path { get; init; } = new PathItem();

        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return Path.HasValue && Scope != ScopeType.Null; } }

        /// <summary>
        /// Blank internal constructor.
        /// </summary>
        protected PathIndex() : base()
        { }

        /// <summary>
        /// Constructor that clones an existing IPathIndex
        /// </summary>
        /// <param name="source"></param>
        public PathIndex(IPathIndex source) : this()
        {
            Path = source.Path;
            Scope = source.Scope;
        }

        /// <summary>
        /// Constructor used to build a PathIndex from objects that support IPathValue.
        /// </summary>
        /// <param name="source"></param>
        public PathIndex(IPathValue source) : this()
        {
            Path = source.Path;
            Scope = source.Scope;
        }

        /// <summary>
        /// Allows the construction of a PathIndex from a chain of PathValues.
        /// </summary>
        /// <param name="paths"></param>
        /// <remarks>
        /// This handles to nested value scenario such as Subject Areas.<br/>
        /// The last item in the list determines the scope.</remarks>
        public PathIndex(params IEnumerable<IPathValue> paths) : this()
        {
            PathItem? newPath = null;

            foreach (var item in paths)
            {
                if (newPath is null)
                { newPath = new PathItem(item.Path); }
                else
                { newPath = newPath.Append(item.Path); }

                Scope = item.Scope;
            }

            if (newPath is not null)
            { Path = newPath; }
        }

        /// <summary>
        /// Constructor that handles Model Aliases
        /// </summary>
        /// <param name="source"></param>
        /// <example>
        /// var source = new AttributeAliasValue(); // or EntityAliasValue or ProcessAliasValue ...
        /// var x = new PathIndex(new AliasIndex(source));
        /// </example>
        public PathIndex(AppModel.AliasIndex source) : this()
        {   // This is needed to deal with situations where IAttributeAliasValue is a IAliasIndex and a IPathValue.
            Path = new PathItem(PathItem.Parse(source.AliasPath));
            Scope = source.AliasScope;
        }

        /// <summary>
        /// Constructor that handles Template Objects
        /// </summary>
        /// <param name="source"></param>
        /// <example>
        /// var source = new SchemaDocumentValue(); // or TransformDocumentValue ...
        /// var x = new PathIndex(new TemplateObjectIndex(source));
        /// </example>
        public PathIndex(AppScripting.TemplateObjectIndex source) : this()
        {   // This is needed to deal with situations where ISchemaDocumentValue is a ITemplateObjectIndex and a IPathValue
            Path = new PathItem(PathItem.Parse(source.ObjectPath));
            Scope = source.ObjectScope;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(PathIndex? other)
        {
            return other is PathIndex key
                && this.HasValue
                && key.HasValue
                && this.Scope == other.Scope
                && this.Path.Equals(other.Path);
        }

        /// <inheritdoc/>
        public Boolean Equals(IPathIndex? other)
        { return other is IPathIndex value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IPathIndex value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(PathIndex left, PathIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PathIndex left, PathIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(Scope.GetHashCode(), Path.GetHashCode()); }
        #endregion
    }
}
