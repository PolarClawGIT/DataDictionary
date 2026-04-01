using DataDictionary.Resource;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Attribute.
    /// </summary>
    [Obsolete]
    public interface ITemplateAttributeKey : IKey
    {
        /// <summary>
        /// Template Attribute Id of the Scripting Template Attribute.
        /// </summary>
        Guid? AttributeId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Template Attribute.
    /// </summary>
    [Obsolete]
    public class TemplateAttributeKey : ITemplateAttributeKey,
        IKeyEquality<ITemplateAttributeKey>, IKeyEquality<TemplateAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Attribute.
        /// </summary>
        /// <param name="source"></param>
        public TemplateAttributeKey(ITemplateAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateAttributeKey? other)
        { return other is TemplateAttributeKey key && key.AttributeId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(AttributeId, other.AttributeId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateAttributeKey? other)
        { return other is ITemplateAttributeKey value && Equals(new TemplateAttributeKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateAttributeKey value && Equals(new TemplateAttributeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateAttributeKey left, TemplateAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateAttributeKey left, TemplateAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (AttributeId is Guid) { return AttributeId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
