using DataDictionary.Resource;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Element.
    /// </summary>
    [Obsolete]
    public interface ITemplateElementKey : IKey
    {
        /// <summary>
        /// Template Node Id of the Scripting Template Node.
        /// </summary>
        Guid? ElementId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Template Element.
    /// </summary>
    [Obsolete]
    public class TemplateElementKey : ITemplateElementKey,
        IKeyEquality<ITemplateElementKey>, IKeyEquality<TemplateElementKey>
    {
        /// <inheritdoc/>
        public Guid? ElementId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return ElementId.HasValue && ElementId != Guid.Empty; } }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Element.
        /// </summary>
        /// <param name="source"></param>
        public TemplateElementKey(ITemplateElementKey source) : base()
        {
            if (source.ElementId is Guid) { ElementId = source.ElementId; }
            else { ElementId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateElementKey? other)
        { return other is TemplateElementKey key && key.ElementId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(ElementId, other.ElementId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementKey? other)
        { return other is ITemplateElementKey value && Equals(new TemplateElementKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateElementKey value && Equals(new TemplateElementKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateElementKey left, TemplateElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateElementKey left, TemplateElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ElementId is Guid) { return ElementId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
