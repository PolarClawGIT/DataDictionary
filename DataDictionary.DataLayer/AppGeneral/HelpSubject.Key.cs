using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppGeneral
{
    /// <summary>
    /// Primary key for the Help Documentation.
    /// </summary>
    public interface IHelpSubjectKey : IKey
    {
        /// <summary>
        /// Primary Key ID for the Help Documentation.
        /// </summary>
        Guid? HelpId { get; }
    }

    /// <summary>
    /// Primary key for the Help Documentation.
    /// </summary>
    public class HelpSubjectKey : IHelpSubjectKey,
        IKeyEquality<IHelpSubjectKey>,
        IKeyEquality<HelpSubjectKey>
    {
        /// <inheritdoc/>
        public Guid? HelpId { get; init; } = Guid.Empty;

        /// <summary>
        /// Creates a Help Key of an empty key.
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public HelpSubjectKey() : base () { }

        /// <summary>
        /// Creates a Help Key from a item that implements the Primary key.
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectKey(IHelpSubjectKey source) : base()
        {
            if (source.HelpId is Guid) { HelpId = source.HelpId; }
            else { HelpId = Guid.Empty; }
        }

        #region IEquatable

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectKey? other)
        { return other is HelpSubjectKey key && key.HelpId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(HelpId, other.HelpId); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectKey? other)
        { return other is IHelpSubjectKey value && Equals(new HelpSubjectKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IHelpSubjectKey value && Equals(new HelpSubjectKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(HelpSubjectKey left, HelpSubjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(HelpSubjectKey left, HelpSubjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (HelpId is Guid) { return HelpId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }

        #endregion
    }
}
