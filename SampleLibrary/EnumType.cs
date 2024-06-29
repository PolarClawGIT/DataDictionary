using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Library
{
    /// <summary>
    /// Base Class for Enum like objects
    /// </summary>
    public class EnumType : IEquatable<EnumType>
    {
        /// <summary>
        /// Internal ID for the Enum Item.
        /// </summary>
        protected Guid EnumId { get; } = Guid.NewGuid();

        /// <inheritdoc/>
        public virtual Boolean Equals(EnumType? other)
        { return (other is EnumType && EnumId.Equals(other.EnumId)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return (other is EnumType value && this.Equals(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(EnumType left, EnumType right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(EnumType left, EnumType right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return EnumId.GetHashCode(); }
    }

    /// <summary>
    /// Generic version of the Class for Enum like objects
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class EnumType<TValue> : EnumType
    {
        /// <summary>
        /// Value of the Enum
        /// </summary>
        public virtual required TValue Value { get; init; }

        /// <summary>
        /// Display Value of the Enum
        /// </summary>
        public virtual required String DisplayValue { get; init; }
    }

    /// <summary>
    /// Properties of the ScriptAs Enum.
    /// </summary>
    public class ScriptAsType : EnumType<String>, IParsable<ScriptAsType>
    {
        /// <summary>
        /// Extension used with the Script Type
        /// </summary>
        public required String Extension { get; init; }

        /// <inheritdoc/>
        public static ScriptAsType Parse(String s, IFormatProvider? provider)
        {
            foreach (ScriptAsType item in EnumTypes.ScriptAs.AsEnumerable())
            {
                if (item.Value.Equals(s, StringComparison.CurrentCultureIgnoreCase))
                { return item; }
            }

            return EnumTypes.ScriptAs.None;
        }

        /// <inheritdoc/>
        public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ScriptAsType result)
        {
            foreach (ScriptAsType item in EnumTypes.ScriptAs.AsEnumerable())
            {
                if (item.Value.Equals(s, StringComparison.CurrentCultureIgnoreCase))
                { result = item; return true; }
            }

            result = null;
            return false;
        }
    }

    /// <summary>
    /// The Enum for ScriptAs
    /// </summary>
    public class ScriptAsEnum
    {
        /// <summary>
        /// Null or does not apply
        /// </summary>
        public ScriptAsType None { get; } = new ScriptAsType() { Value = nameof(None), DisplayValue = "N/A", Extension = String.Empty };

        /// <summary>
        /// Microsoft C# Script
        /// </summary>
        public ScriptAsType CSharp { get; } = new ScriptAsType() { Value = nameof(CSharp), DisplayValue = ".Net C#", Extension = "cs" };

        /// <summary>
        /// Microsoft VB.Net script
        /// </summary>
        public ScriptAsType VBNet { get; } = new ScriptAsType() { Value = nameof(VBNet), DisplayValue=".Net Vb", Extension = "vb" };

        /// <summary>
        /// Microsoft TSQL script
        /// </summary>
        public ScriptAsType MsSqL { get; } = new ScriptAsType() { Value = nameof(MsSqL), DisplayValue="Ms TSQL", Extension = "sql" };

        /// <summary>
        /// Plain Text script
        /// </summary>
        public ScriptAsType Text { get; } = new ScriptAsType() { Value = nameof(Text), DisplayValue="Text", Extension = "txt" };

        /// <summary>
        /// XML data
        /// </summary>
        public ScriptAsType XML { get; } = new ScriptAsType() { Value = nameof(XML), DisplayValue="XML", Extension = "xml" };

        /// <inheritdoc cref="Enumerable.AsEnumerable{TSource}(IEnumerable{TSource})" />
        public virtual IEnumerable<ScriptAsType> AsEnumerable()
        { return new List<ScriptAsType>() { None, CSharp, VBNet, MsSqL, Text, XML }; }
    }

    /// <summary>
    /// General Purpose static class to hold the Enum's defined. Acts as a NameSpace.
    /// </summary>
    public static class EnumTypes
    {
        /// <summary>
        /// Enum of ScriptAs
        /// </summary>
        public static ScriptAsEnum ScriptAs { get; } = new ScriptAsEnum();
    }
}
