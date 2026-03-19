using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Key
    /// </summary>
    public interface ITemplateKey : IKey
    {
        /// <summary>
        /// Template ID for the Scripting Template.
        /// </summary>
        Guid? TemplateId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Key
    /// </summary>
    public class TemplateKey : ITemplateKey,
        IKeyEquality<TemplateKey>,
        IKeyEquality<ITemplateKey>
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty Template Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public TemplateKey()
        { }

        /// <summary>
        /// Constructor for the Template Key
        /// </summary>
        /// <param name="source"></param>
        public TemplateKey(ITemplateKey source) : base()
        {
            if (source.TemplateId is Guid) { TemplateId = source.TemplateId; }
            else { TemplateId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateKey? other)
        { return other is TemplateKey key && key.TemplateId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(TemplateId, key.TemplateId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateKey? other)
        { return other is ITemplateKey key && Equals(new TemplateKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateKey key && Equals(new TemplateKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateKey left, TemplateKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateKey left, TemplateKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(TemplateId); }


        #endregion
    }
}
