using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Common components of class based Enumeration.
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <remarks>
    /// A class based Enumeration works by creating static properties for each of
    /// the items that are to be part of the Enumeration. The class itself defines any
    /// additional properties that the Enumeration contains.
    /// References are syntactically identical to an Enum.
    /// 
    /// This has its limitations.
    /// A normal Enum is a constant. A class based Enumeration is not a constant even
    /// when all of its values are fixed. This means that a Switch or pattern matching IS
    /// statements do not work with the class based Enumeration.
    /// Instead, Equals (==) and cascading IF statements must be used.
    /// Inheritance does not work as most/all the Fields are static.
    /// Binding DOES NOT WORK on these because it is a class not a constant.
    /// 
    /// Because of the limitation, I am not using this approach.
    /// 
    /// Include
    /// - Each Enumeration needs to be a public static readonly.
    /// - Make an private constructor. Nothing but itself should be able to create instances.
    /// 
    /// Work based on: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    /// </remarks>
    public interface IEnumeration<TSelf> : IEquatable<TSelf>, IParsable<TSelf>
        where TSelf : class, IEnumeration<TSelf>?
    {
        /// <summary>
        /// The name of the Item of the Enumeration.
        /// </summary>
        /// <remarks>This value is what the database uses.</remarks>
        String Name { get; init; }

        /// <summary>
        /// List of Enumeration Items
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<TSelf> ToList();

        #region IParsable<TSelf> Helpers
        /// <summary>
        /// Parses a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="provider">An object that provides culture-specific formatting information about s. (not used)</param>
        /// <exception cref="ArgumentException">No matching value</exception>
        public static TSelf Parse(String source)
        {
            if (TryParse(source, out TSelf? result))
            { return result; }
            else
            {
                Exception ex = new ArgumentException();
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing source.</param>
        /// <returns>True if s was successfully parsed; otherwise, false.</returns>
        public static Boolean TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out TSelf result)
        {
            if (TSelf.ToList().FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf value)
            { result = value; return true; }
            else { result = null; return false; }
        }
        #endregion
    }

    /// <summary>
    /// The following is an example of the implementation of a class based Enumeration
    /// </summary>
    class ExampleEnumeration : IEnumeration<ExampleEnumeration>
    {
        #region Properties of the Enumeration
        /// <inheritdoc/>
        public required String Name { get; init; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        ExampleEnumeration() : base() { }

        #region the Enumeration
        /// <summary>
        /// Example of an Enumeration field for Null value
        /// </summary>
        public static readonly ExampleEnumeration Null = new ExampleEnumeration() { Name = String.Empty };

        /// <summary>
        /// Example of an Enumeration field for Foo.
        /// </summary>
        public static readonly ExampleEnumeration Foo = new ExampleEnumeration() { Name = "Foo" };

        /// <summary>
        /// Example of an Enumeration field for Bar.
        /// </summary>
        public static readonly ExampleEnumeration Bar = new ExampleEnumeration() { Name = "Bar" };
        #endregion

        #region IEnumeration
        /// <inheritdoc/>
        public static IEnumerable<ExampleEnumeration> ToList()
        { return new List<ExampleEnumeration>() { Null, Foo, Bar }; }

        /// <inheritdoc/>
        public static ExampleEnumeration Parse(String s, IFormatProvider? provider = null)
        { return IEnumeration<ExampleEnumeration>.Parse(s); }

        /// <inheritdoc/>
        public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ExampleEnumeration result)
        { return IEnumeration<ExampleEnumeration>.TryParse(s, out result); }

        /// <inheritdoc/>
        public Boolean Equals(ExampleEnumeration? other)
        { return other is ExampleEnumeration && String.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is ExampleEnumeration value && Equals(value); }

        /// <inheritdoc/>
        public static Boolean operator ==(ExampleEnumeration left, ExampleEnumeration right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ExampleEnumeration left, ExampleEnumeration right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(Name); }
        #endregion

        public override String ToString()
        {
            if (String.IsNullOrWhiteSpace(Name)) { return "nothing"; }
            else { return Name; }
        }
    }

}
