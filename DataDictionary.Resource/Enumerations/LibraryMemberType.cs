namespace DataDictionary.Resource.Enumerations
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



}
