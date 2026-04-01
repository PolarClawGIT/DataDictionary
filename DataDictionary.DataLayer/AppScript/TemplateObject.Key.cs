using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppScript
{

    /// <summary>
    /// Interface for the Scripting TemplateObject Key
    /// </summary>
    public interface ITemplateObjectKey : IKey
    {
        /// <summary>
        /// Object ID for the Scripting TemplateObject.
        /// </summary>
        Guid? ObjectId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting TemplateObject Key
    /// </summary>
    public class TemplateObjectKey : ITemplateObjectKey,
        IKeyEquality<TemplateObjectKey>,
        IKeyEquality<ITemplateObjectKey>
    {
        /// <inheritdoc/>
        public Guid? ObjectId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty TemplateObject Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public TemplateObjectKey()
        { }

        /// <summary>
        /// Constructor for the TemplateObject Key
        /// </summary>
        /// <param name="source"></param>
        public TemplateObjectKey(ITemplateObjectKey source) : base()
        {
            if (source.ObjectId is Guid) { ObjectId = source.ObjectId; }
            else { ObjectId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectKey? other)
        { return other is TemplateObjectKey key && key.ObjectId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(ObjectId, key.ObjectId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectKey? other)
        { return other is ITemplateObjectKey key && Equals(new TemplateObjectKey(key)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateObjectKey key && Equals(new TemplateObjectKey(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateObjectKey left, TemplateObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateObjectKey left, TemplateObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ObjectId); }


        #endregion
    }

}
