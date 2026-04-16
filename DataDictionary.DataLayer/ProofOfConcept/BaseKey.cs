using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.ProofOfConcept
{
    /// <summary>
    /// POC code for a generic Key base class.
    /// The Point is to try to simplify/reduce the code needed to define a Key Class. 
    /// This is not leading to much success.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class BaseKey<T> : IKey, IKeyEquality<BaseKey<T>>
            where T : struct, IEquatable<T>
    {
        /// <summary>
        /// The Value of the Key.
        /// This is the backing field.
        /// </summary>
        protected Nullable<T> KeyValue;

        /// <summary>
        /// Gets a function that determines whether a specified value is considered empty for the type
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>The default implementation treats a value as empty if it is equal to the default
        /// value of <typeparamref name="T"/> according to the default equality comparer. Derived classes can override
        /// this property to provide custom logic for determining emptiness.
        /// </remarks>
        protected virtual Func<T, Boolean> IsEmpty { get; } = (a) => EqualityComparer<T>.Default.Equals(a, default(T));

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public virtual Boolean HasValue { get { return KeyValue is T key && KeyValue.HasValue && !IsEmpty(key); } }

        /// <summary>
        /// Basic Constructor for the Key
        /// </summary>
        /// <param name="newValue"></param>
        protected BaseKey(T? newValue) : base()
        { KeyValue = newValue; }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public virtual Boolean Equals(BaseKey<T>? other)
        {
            return other is BaseKey<T> key
                && this.GetType() == other.GetType() // Both need to be of the same child type?
                && KeyValue.HasValue
                && key.KeyValue.HasValue
                && KeyValue.Equals(other.KeyValue);
            //&& EqualityComparer<T>.Default.Equals(KeyValue, other.KeyValue);
        }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public static Boolean operator ==(BaseKey<T> left, BaseKey<T> right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public static Boolean operator !=(BaseKey<T> left, BaseKey<T> right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public override Boolean Equals(Object? obj)
        {
            if (obj is BaseKey<T> key) { return Equals(key); }
            else { return false; }
        }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public override int GetHashCode()
        { return KeyValue.GetHashCode(); }

        /// <inheritdoc/>
        /// <remarks>BaseKey</remarks>
        public override String ToString()
        {
            if (KeyValue is T key && key.ToString() is String result)
            { return result; }
            else { return String.Empty; }
        }
    }

    class CatalogKey : BaseKey<Guid>,
        AppCatalog.ICatalogKey, IKeyEquality<AppCatalog.ICatalogKey>
    {
        /// <inheritdoc/>
        public virtual Guid? CatalogId { get { return base.KeyValue; } }

        /// <inheritdoc/>
        protected override Func<Guid, Boolean> IsEmpty { get; } = (a) => a != Guid.Empty;

        /// <summary>
        /// Empty constructor for a Catalog Key
        /// </summary>
        public CatalogKey() : base(Guid.Empty)
        { }

        /// <summary>
        /// Constructor for a Catalog Key
        /// </summary>
        public CatalogKey(ICatalogKey catalog) : base(catalog.CatalogId)
        { }

        /// <inheritdoc/>
        /// <remarks>CatalogKey</remarks>
        public virtual Boolean Equals(ICatalogKey? other)
        {
            if (other is ICatalogKey key)
            { return base.Equals(new CatalogKey(key)); }
            else { return false; }
        }

        /// <inheritdoc/>
        /// <remarks>CatalogKey</remarks>
        public static Boolean operator ==(CatalogKey left, ICatalogKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>CatalogKey</remarks>
        public static Boolean operator !=(CatalogKey left, ICatalogKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        /// <remarks>CatalogKey</remarks>
        public override Boolean Equals(Object? obj)
        {
            if (obj is CatalogKey key)
            { return Equals(key); }
            else { return false; }
        }

        /// <inheritdoc/>
        /// <remarks>CatalogKey</remarks>
        public override Int32 GetHashCode()
        { return CatalogId.GetHashCode(); }
    }

    class UnitCatalogKey
    {
        public static void SomeMethod()
        {
            CatalogKey key = new CatalogKey();
            AppCatalog.CatalogItem item = new CatalogItem();
            CatalogKey itemKey = new CatalogKey(item);

            if (itemKey.Equals(item)) { }
            if (key.Equals(itemKey)) { }
            if (itemKey == item) { }
        }
    }
}
