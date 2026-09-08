using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for Single File.
    /// </summary>
    public interface IFileValue : IBindingPropertyChanged
    {
        /// <summary>
        /// File Name for the File within the File Path.
        /// dialog.FileName = IFileValue.FileName
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// List of FileFormats supported. Normally only one.
        /// </summary>
        IEnumerable<FileFormatType> FileFormats { get; }

        /// <summary>
        /// Generate the WorkItems for Opens the File and loads it to the FileContent property
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <remarks>Does not update the FileName property.</remarks>
        IReadOnlyList<WorkItem> Open(FileInfo file);

        /// <summary>
        /// Generate the WorkItems for Saves the FileContent property to the file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <remarks>Does not update the FileName property.</remarks>
        IReadOnlyList<WorkItem> Save(FileInfo file);

        /// <summary>
        /// Validates the File info and returns an Exception if there is an issue.
        /// </summary>
        /// <returns></returns>
        Boolean IsValid([NotNullWhen(false)] out Exception? exception);
    }

    /// <summary>
    /// Represents a Single File.
    /// Used to hold file information for use with the File Open/Save Dialog.
    /// This is a Wrapper around the fields in the base table so that they can be treated as a single unit.
    /// </summary>
    public class FileValue : IFileValue
    {

        /// <inheritdoc/>
        public String FileName
        {
            get { return GetFileName(); }
            set
            {
                SetFileName(value);
                OnPropertyChanged(nameof(FileName));
            }
        }

        internal Func<String> GetContent { private get; init; }
        internal Action<String> SetContent { private get; init; }

        /// <inheritdoc/>
        public IEnumerable<FileFormatType> FileFormats
        { get { return GetFileFormats(); } }

        /// <summary>
        /// Function representing Get function for the FileName.
        /// </summary>
        protected internal Func<String> GetFileName { protected get; init; }

        /// <summary>
        /// Function representing Set method for the FileName.
        /// </summary>
        protected internal Action<String> SetFileName { protected get; init; }
        String fileNameValue = String.Empty;

        /// <summary>
        /// Function representing Get function for the FileFormatTypes.
        /// </summary>
        protected internal Func<IEnumerable<FileFormatType>> GetFileFormats { protected get; init; }
        List<FileFormatType> fileFormatValues = new List<FileFormatType>() { FileFormatType.PlainText };

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="BindingPropertyChanged.OnPropertyChanged"/>
        protected virtual void OnPropertyChanged(String propertyName)
        { this.OnPropertyChanged(PropertyChanged, nameof(propertyName)); }

        /// <summary>
        /// Create an Instance of a FileValue.
        /// </summary>
        /// <remarks>
        /// The initial State is not linked to a base class.
        /// Use Init Get/Set properties to link the base class.
        /// </remarks>
        public FileValue() : base()
        {
            String contentValue = String.Empty; // Internal holder of the content during initialization

            GetFileName = () => fileNameValue;
            SetFileName = (v) => fileNameValue = v;

            GetFileFormats = () => fileFormatValues;

            GetContent = () => contentValue;
            SetContent = (v) => contentValue = v;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();
            Boolean cancel = false;

            if (!String.IsNullOrWhiteSpace(FileName))
            {
                work.Add(new WorkItem()
                {
                    WorkName = String.Format("Opening {0}", FileName),
                    DoWork = OnWork,
                    IsCanceling = () => cancel
                });
            }

            return work;

            void OnWork()
            {
                if (file.Exists)
                {
                    try
                    {   SetContent(File.ReadAllText(file.FullName)); }
                    catch (Exception ex)
                    {
                        cancel = true;
                        ex.Data.Add(nameof(file), file.FullName);
                        throw;
                    }
                }
                else
                {
                    cancel = true;
                    Exception ex = new FileNotFoundException();
                    ex.Data.Add(nameof(file), file.FullName);
                    throw ex;
                }
            }
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        {
            List<WorkItem> work = new List<WorkItem>();
            Boolean cancel = false;

            work.Add(new WorkItem()
            {
                WorkName = String.Format("Saving {0}", FileName),
                DoWork = OnWork,
                IsCanceling = () => cancel
            });

            return work;

            void OnWork()
            {
                try
                {
                    // Detect if the data is XML and use XML save instead of normal text.
                    // TODO: This is still adding the Byte Order Mark (BOM) to the file.
                    // This is not necessary an in some cases, may cause issues with other tools.
                    if (GetContent().TryParse(out XDocument? document, out Exception? _))
                    { document.Save(file.FullName); }
                    else // Save the file as Text. This is expected to have a BOM.
                    { File.WriteAllText(file.FullName, GetContent()); }
                }
                catch (Exception ex)
                {
                    cancel = true;
                    ex.Data.Add(nameof(file), file.FullName);
                    throw;
                }
            }
        }

         /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        {
            exception = null;
            String fileName = Path.GetFileName(FileName);
            List<String> directories;

            if (String.IsNullOrWhiteSpace(Path.GetPathRoot(FileName)))
            {
                directories = (Path.GetDirectoryName(FileName) ?? String.Empty).
                    Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries).
                    ToList();
            }
            else
            {
                directories = (Path.GetRelativePath(Path.GetPathRoot(FileName) ?? Path.DirectorySeparatorChar.ToString(), FileName)).
                    Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries).
                    ToList();
            }

            if (String.IsNullOrWhiteSpace(fileName))
            { exception = new ArgumentNullException(nameof(FileName)); }
            else if (fileName.Length > 255) // TODO: OS handles longer paths, but the database is restricted by indexing limits.
            { exception = new PathTooLongException(); }
            else if (fileName.Any(a => Path.GetInvalidFileNameChars().Contains(a)))
            { exception = new ArgumentException("Invalid FileName Character(s)"); }
            else if (directories.Any(a => a.Any(b => Path.GetInvalidFileNameChars().Contains(b))))
            { exception = new ArgumentException("Invalid Directory Character(s)"); }
            else
            {
                try
                { var file = new FileInfo(FileName); }
                catch (Exception ex)
                { exception = ex; }
            }

            if (exception is not null)
            {
                exception.Data.Add(nameof(FileName), FileName);
                exception.Data.Add(nameof(fileName), fileName);
                exception.Data.Add(nameof(directories), String.Join("//", directories));
            }

            return exception is null;
        }
    }
}
