namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for TemplateDirectoryType
    /// </summary>
    public interface ITemplateDirectory
    {
        /// <summary>
        /// Root Directory to place documents in (must be a supported Special Folder).
        /// </summary>
        DirectoryType RootDirectory { get; }
    }

    /// <summary>
    /// Supported Folders
    /// </summary>
    public enum DirectoryType
    {
        // This is primary used for Scripting.

        /// <summary>
        /// Unspecified Directory or unidentified.
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