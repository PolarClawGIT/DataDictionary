using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for Single Directory.
    /// </summary>
    public interface IDirectoryValue : IBindingPropertyChanged
    {
        /// <summary>
        /// Full Directory Path (includes root).
        /// </summary>
        /// <remarks>Use to set the FileDialog (Open File or Save File) initial directory or to update the Relative Path</remarks>
        /// <example>
        /// dialog.InitialDirectory = IDirectoryValue.InitialDirectory;
        /// </example>
        String InitialDirectory { get; set; }

        /// <summary>
        /// Validates the Directory info and returns an Exception if there is an issue.
        /// </summary>
        /// <returns></returns>
        Boolean IsValid([NotNullWhen(false)] out Exception? exception);
    }

    /// <summary>
    /// Represents a Single Directory.
    /// Used to hold directory information for use with the Folder Browser Dialog.
    /// This is a Wrapper around the fields in the base table so that they can be treated as a single unit.
    /// </summary>
    public class DirectoryValue : IDirectoryValue
    {
        /// <summary>
        /// Function that returns the Current root Directory Type.
        /// </summary>
        protected internal Func<DirectoryType> GetRootFolder { protected get; init; } = () => DirectoryType.Null;

        /// <summary>
        /// Function representing the Get function for the Relative Directory.
        /// </summary>
        protected internal Func<String> GetDirectory { protected get; init; }

        /// <summary>
        /// Method representing the Set method for the Relative Directory.
        /// </summary>
        protected internal Action<String> SetDirectory { protected get; init; }
        String directoryValue = String.Empty;

        /// <inheritdoc/>
        public virtual String InitialDirectory
        {
            get
            {
                String relativeDirectory = GetDirectory();
                DirectoryType rootFolder = GetRootFolder();
                String rootPath = String.Empty;
                if (rootFolder.GetFolder() is DirectoryInfo rootDirectory)
                { rootPath = rootDirectory.FullName; }

                if (rootFolder is DirectoryType.Null && String.IsNullOrWhiteSpace(relativeDirectory))
                { return String.Empty; }
                else if (rootFolder is DirectoryType.Null) { return relativeDirectory ?? String.Empty; }
                else if (String.IsNullOrWhiteSpace(relativeDirectory)) { return rootPath; }
                else { return Path.Combine(rootPath, relativeDirectory ?? String.Empty); }
            }
            set
            {
                DirectoryType rootFolder = GetRootFolder();
                String rootPath = String.Empty;
                if (rootFolder.GetFolder() is DirectoryInfo rootDirectory)
                { rootPath = rootDirectory.FullName; }

                if (value.StartsWith(rootPath))
                {
                    String path;
                    if (String.IsNullOrWhiteSpace(rootPath))
                    { path = value; }
                    else { path = Path.GetRelativePath(rootPath, value); }

                    if (path is "." || String.IsNullOrWhiteSpace(path))
                    { SetDirectory(String.Empty); }
                    else { SetDirectory(path); }
                }

                else { SetDirectory(value); }

                OnPropertyChanged(nameof(InitialDirectory));
            }
        }

        /// <summary>
        /// Create an Instance of a DirectoryValue.
        /// </summary>
        /// <remarks>
        /// The initial State is not linked to a base class.
        /// Use Init Get/Set properties to link the base class.
        /// </remarks>
        public DirectoryValue() : base()
        {
            GetDirectory = () =>
            {
                if (String.IsNullOrWhiteSpace(directoryValue)
                && GetRootFolder().GetFolder() is DirectoryInfo directory)
                { return directory.FullName; }
                else { return directoryValue; }
            };
            SetDirectory = (v) => directoryValue = v;
        }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="BindingPropertyChanged.OnPropertyChanged"/>
        protected virtual void OnPropertyChanged(String propertyName)
        { this.OnPropertyChanged(PropertyChanged, nameof(propertyName)); }

        /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        {
            exception = null;
            String directory = InitialDirectory;
            List<String> directories;

            if (String.IsNullOrWhiteSpace(Path.GetPathRoot(directory)))
            {
                directories = (Path.GetDirectoryName(directory) ?? String.Empty).
                    Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries).
                    ToList();
            }
            else
            {
                directories = (Path.GetRelativePath(Path.GetPathRoot(directory) ?? Path.DirectorySeparatorChar.ToString(), directory)).
                    Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries).
                    ToList();
            }

            if (String.IsNullOrWhiteSpace(directory))
            { exception = new ArgumentNullException(nameof(directory)); }
            else if (directories.Any(a => a.Any(b => Path.GetInvalidFileNameChars().Contains(b))))
            { exception = new ArgumentException("Invalid Directory Character(s)"); }
            else
            {
                try
                { var file = new DirectoryInfo(directory); }
                catch (Exception ex)
                { exception = ex; }
            }

            if (exception is not null)
            {
                exception.Data.Add(nameof(directory), directory);
                exception.Data.Add(nameof(directories), String.Join("//", directories));
            }

            return exception is null;
        }
    }
}
