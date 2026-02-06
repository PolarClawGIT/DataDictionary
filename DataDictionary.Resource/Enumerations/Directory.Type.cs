namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Supported Folders
    /// </summary>
    public enum DirectoryType
    {
        // This is primary used for Scripting.

        /// <summary>
        /// Unspecified Directory
        /// </summary>
        Null,

        /// <summary>
        /// Base Directory for My Documents.
        /// Fixed: Environment.SpecialFolder.MyDocuments
        /// </summary>
        MyDocuments,

        /// <summary>
        /// Offset Directory used for the Downloads folder.
        /// Default: Environment.SpecialFolder.UserProfile & Downloads
        /// </summary>
        MyDownloads,

        /// <summary>
        /// Location Visual Studio places Source files.
        /// Default: Environment.SpecialFolder.UserProfile & source\repos
        /// </summary>
        Projects,

        /// <summary>
        /// Location Application looks for Data Dictionary
        /// Default: Environment.SpecialFolder.MyDocuments & DataDictionary
        /// </summary>
        Dictionary,
    }
}
