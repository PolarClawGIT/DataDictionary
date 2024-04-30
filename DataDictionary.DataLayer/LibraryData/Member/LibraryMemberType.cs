using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.LibraryData.Member
{
    // This is all in one file because they are so closely related and was easer to find.

    /// <summary>
    /// List of supported .Net Library Types.
    /// </summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"/>
    public enum LibraryMemberType
    {
        /// <summary>
        /// Unknown Member Type
        /// </summary>
        Null,

        /// <summary>
        /// .Net Library NameSpace
        /// </summary>
        NameSpace,

        /// <summary>
        ///.Net Library class, interface, struct, enum, or delegate.
        /// </summary>
        Type,

        /// <summary>
        /// .Net Library Field
        /// </summary>
        Field,

        /// <summary>
        /// .Net Library property. Includes indexers or other indexed properties.
        /// </summary>
        Property,

        /// <summary>
        /// .Net Library method. Includes special methods, such as constructors and operators.
        /// </summary>
        Method,

        /// <summary>
        /// .Net Library Event
        /// </summary>
        Event,

        /// <summary>
        /// .Net Library method Parameter
        /// </summary>
        Parameter,

    }

    /// <summary>
    /// Interface for Database MemberType Key.
    /// </summary>
    public interface ILibraryMemberType : IKey
    {
        /// <summary>
        /// Type of Member (NameSpace, Type, Property, Field, ...)
        /// </summary>
        LibraryMemberType MemberType { get; }
    }

    /// <summary>
    /// Support for LibraryMemberType
    /// </summary>
    public static class LibraryMemberTypeExtension
    {
        /// <summary>
        /// Returns the Library Member Type Name
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"/>
        public static String? ToName(this LibraryMemberType source)
        { return new LibraryMemberTypeKey(source).Name; }

        /// <summary>
        /// Returns the Library Member Type Code.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/"/>
        public static Char? ToCode(this LibraryMemberType source)
        { return new LibraryMemberTypeKey(source).Code; }
    }

}
