using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{

    /// <summary>
    /// Interface for the XmlBuilder Key
    /// </summary>
    public interface IXmlBuilderIndex : IKey, ISchemaNodeObjectName
    { }

    /// <summary>
    /// Implementation for the XmlBuilder Key
    /// </summary>
    public class XmlBuilderIndex : IXmlBuilderIndex, IPathItem,
        IKeyComparable<XmlBuilderIndex>, IKeyComparable<IXmlBuilderIndex>,
        IKeyEquality<PathItem>, IKeyEquality<ISchemaNodeObjectName>
    {
        /// <inheritdoc/>
        public ScopeType ObjectScope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String? ObjectProperty { get; init; } = String.Empty;

        /// <inheritdoc/>
        public String Member { get { return ((PathItem)this).Member; } }

        /// <inheritdoc/>
        public String MemberPath { get { return ((PathItem)this).MemberPath; } }

        /// <inheritdoc/>
        public String MemberFullPath { get { return ((PathItem)this).MemberFullPath; } }

        /// <inheritdoc/>
        public PathItem? ParentPath { get { return ((PathItem)this).ParentPath; } }

        /// <summary>
        /// Constructor for the Schema Node Key Name.
        /// </summary>
        protected XmlBuilderIndex() : base() { }

        /// <summary>
        /// Constructor for the XmlBuilderIndex Key Name. Cloner.
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilderIndex(IXmlBuilderIndex source) : this()
        {
            ObjectScope = source.ObjectScope;
            ObjectProperty = source.ObjectProperty;
        }

        /// <summary>
        /// Constructor for the XmlBuilderIndex Key Name.
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilderIndex(ISchemaNodeObjectName source) : this()
        {
            ObjectScope = source.ObjectScope;
            ObjectProperty = source.ObjectProperty;
        }

        /// <summary>
        /// Constructor for the Schema Node Key Name.
        /// </summary>
        /// <param name="objectScope"></param>
        /// <param name="objectProperty"></param>
        public XmlBuilderIndex(ScopeType objectScope, String? objectProperty = null)
        {
            ObjectScope = objectScope;
            ObjectProperty = objectProperty;
        }

        /// <inheritdoc/>
        public Boolean HasValue { get { return !ObjectScope.Equals(ScopeType.Null); } }

        /// <summary>
        /// Converts a XmlBuilderIndex to PathIndex.
        /// </summary>
        /// <param name="index"></param>
        public static implicit operator PathItem(XmlBuilderIndex index)
        {
            List<String> values = new List<String>();
            values.AddRange(PathItem.Parse(index.ObjectScope.GetName()));

            if (String.IsNullOrWhiteSpace(index.ObjectProperty))
            { return new PathItem(values); }
            else
            {
                values.AddRange(PathItem.Parse(index.ObjectProperty));
                return new PathItem(values);
            }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(XmlBuilderIndex? other)
        {
            return
                other is XmlBuilderIndex
                && !ObjectScope.Equals(ScopeType.Null)
                && !other.ObjectScope.Equals(ScopeType.Null)
                && ObjectScope.Equals(other.ObjectScope)
                && (
                    (String.IsNullOrWhiteSpace(ObjectProperty) && String.IsNullOrWhiteSpace(other.ObjectProperty))
                    || (String.Equals(ObjectProperty,other.ObjectProperty, StringComparison.CurrentCulture)));
        }

        /// <inheritdoc/>
        public Boolean Equals(IXmlBuilderIndex? other)
        { return other is IXmlBuilderIndex value && Equals(new XmlBuilderIndex(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ISchemaNodeObjectName? other)
        { return other is ISchemaNodeObjectName value && Equals(new XmlBuilderIndex(value)); }

        /// <inheritdoc/>
        public Boolean Equals(PathItem? other)
        { return ((PathItem)this).Equals(other); }

        /// <inheritdoc/>
        public Int32 CompareTo(XmlBuilderIndex? other)
        {
            if (other is null) { return 1; }
            else if (!HasValue) { return -1; }
            else if (!other.HasValue) { return 1; }
            else if (ObjectScope.CompareTo(other.ObjectScope) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ObjectProperty ?? String.Empty, other.ObjectProperty ?? String.Empty, true); }
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IXmlBuilderIndex value && Equals(new XmlBuilderIndex(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(IXmlBuilderIndex? other)
        { if (other is IXmlBuilderIndex value) { return CompareTo(new XmlBuilderIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public Int32 CompareTo(object? obj)
        { if (obj is IXmlBuilderIndex value) { return CompareTo(new XmlBuilderIndex(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(XmlBuilderIndex left, XmlBuilderIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(XmlBuilderIndex left, XmlBuilderIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(XmlBuilderIndex left, XmlBuilderIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(XmlBuilderIndex left, XmlBuilderIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(XmlBuilderIndex left, XmlBuilderIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(XmlBuilderIndex left, XmlBuilderIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(
                ObjectScope.GetName().GetHashCode(KeyExtension.CompareString),
                (ObjectProperty ?? String.Empty).GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return MemberFullPath; }
    }
}
