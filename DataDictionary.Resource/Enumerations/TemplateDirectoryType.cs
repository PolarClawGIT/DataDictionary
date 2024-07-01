namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Supported Scripting Folders
    /// </summary>
    public enum TemplateDirectoryType
    {
        /// <summary>
        /// Unspecified Directory
        /// </summary>
        Null,

        /// <summary>
        /// Default Location Visual Studio places Source files.
        /// </summary>
        MySources,

        /// <inheritdoc cref="Environment.SpecialFolder.MyDocuments"/>
        MyDocuments,

        /// <summary>
        /// Default Location of the User Downloads.
        /// </summary>
        MyDownloads
    }
}
