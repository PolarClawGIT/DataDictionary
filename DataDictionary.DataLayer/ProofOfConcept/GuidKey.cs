using DataDictionary.Resource;

namespace DataDictionary.DataLayer.ProofOfConcept
{
    /// <summary>
    /// Base Class for Keys that use a Guid.
    /// </summary>
    /// <remarks>POC, does this make coding keys any simpler?</remarks>
    abstract class GuidKey : IKey, IKeyEquality<GuidKey>
    {
        /// <summary>
        /// Internal storage of the Guid Value used as a Key.
        /// </summary>
        protected Guid? GuidValue { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        /// <remarks>GuidKey</remarks>
        public virtual Boolean HasValue { get { return GuidValue.HasValue && GuidValue != Guid.Empty; } }

        protected GuidKey() : base()
        { }

        protected GuidKey(Guid? key) : this()
        {
            if (key.HasValue && key.Value != Guid.Empty)
            { GuidValue = key; }
        }

        #region IEquatable
        /// <inheritdoc/>
        /// <remarks>GuidKey</remarks>
        public virtual Boolean Equals(GuidKey? other)
        {
            return other is GuidKey key
                && this.GetType() == other.GetType() // Both need to be of the same child type?
                && GuidValue.HasValue && GuidValue != Guid.Empty
                && key.GuidValue.HasValue && key.GuidValue != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(GuidValue, other.GuidValue);
        }

        /// <inheritdoc/>
        /// <remarks>GuidKey</remarks>
        public override Boolean Equals(object? obj)
        {
            if (obj is GuidKey key) { return Equals(key); }
            else { return false; }
        }

        /// <inheritdoc/>
        /// <remarks>GuidKey</remarks>
        public static Boolean operator ==(GuidKey left, GuidKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>GuidKey</remarks>
        public static Boolean operator !=(GuidKey left, GuidKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(GuidValue); }

        #endregion
    }

    /// <summary>
    /// POC interface for GuidKey concept
    /// </summary>
    interface ITestKey : IKey
    {
        Guid? TestKeyId { get; }
    }

    class TestKey : GuidKey,
        ITestKey, IKeyEquality<ITestKey>//, IKeyEquality<TestKey>
    {
        /// <inheritdoc/>
        public Guid? TestKeyId { get { return base.GuidValue; } }

        public TestKey() : base() { }

        public TestKey(ITestKey key) : base(key.TestKeyId)
        { }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public Boolean Equals(ITestKey? other)
        {
            if (other is ITestKey key)
            { return base.Equals(new TestKey(key)); }
            else { return false; }
        }
        /*
        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public Boolean Equals(TestKey? other)
        { return base.Equals(other); }
        
        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public override Boolean Equals(object? other)
        { return base.Equals(other); }
        
        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator ==(TestKey left, TestKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator !=(TestKey left, TestKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator ==(TestKey left, ITestKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator !=(TestKey left, ITestKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator ==(ITestKey left, TestKey right)
        { return new TestKey(left).Equals(right); }

        /// <inheritdoc/>
        /// <remarks>TestKey</remarks>
        public static Boolean operator !=(ITestKey left, TestKey right)
        { return !new TestKey(left).Equals(right); }
        
        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(TestKeyId); }
        */
        
    }

    class AltKey:GuidKey
    {

    }

    class TestItem : ITestKey
    {
        /// <inheritdoc/>
        public Guid? TestKeyId { get; init; } = Guid.Empty;

        public TestItem() : base()
        {
            TestKeyId = Guid.NewGuid();
        }
    }

    class UnitTest
    {
        void SomeMethod()
        {
            TestKey key = new TestKey();
            TestKey alt = new TestKey();
            ITestKey other = new TestKey();
            TestItem item = new TestItem();
            AltKey altKey = new AltKey();

            if (key.Equals(other)) { }
            if (key.HasValue) { }
            if (key == new TestKey(other)) { }

            if (key.Equals(alt)) { }
            if (key == alt) { }

            if (key.Equals(item)) { }
            if (new TestKey(item).Equals(key)) { }

            if (altKey.Equals(key)) { }

            //if (key == item) { }
            //if (item == key) { }
        }
    }
}
