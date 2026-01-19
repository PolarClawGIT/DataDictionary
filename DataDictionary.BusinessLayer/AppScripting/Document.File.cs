using DataDictionary.Resource.Enumerations;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface for Single File of a Document.
    /// </summary>
    public interface IDocumentFile : IBindingPropertyChanged
    {
        /// <summary>
        /// Gets/Sets the Directory Full Path of the File.
        /// </summary>
        String FilePath { get; set; }

        /// <summary>
        /// Directory Name for the file, relative to the Root;
        /// </summary>
        String Directory { get; set; }

        /// <summary>
        /// File Name for the File within the Directory
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// Text Content of the File
        /// </summary>
        String Content { get; set; }

        /// <summary>
        /// Generate the WorkItems to Open the file and load the Content field
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// This does not call the Property Change event to avoid threading issues.
        /// On completion call: bindingSource.ResetCurrentItem();
        /// </remarks>
        IReadOnlyList<WorkItem> Open();

        /// <summary>
        /// Generate the WorkItems to Save the Content field to the File.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save();
    }


    /// <summary>
    /// Represents a Single File of a Document.
    /// Used to hold Input, Transform, and Output file information.
    /// This is a Wrapper around the fields in the main Document so that they can be treated as a single unit.
    /// </summary>
    public class DocumentFile : IDocumentFile
    {
        /// <summary>
        /// Function that returns the Current root Directory Type.
        /// </summary>
        internal Func<DirectoryType> GetRootFolder { private get; init; } = () => DirectoryType.Null;

        /// <inheritdoc/>
        public String Directory
        {
            get { return GetDirectory(); }
            set
            {
                SetDirectory(value);

                OnPropertyChanged(nameof(Directory));
                OnPropertyChanged(nameof(FilePath));
            }
        }
        internal Func<String> GetDirectory { private get; init; }
        internal Action<String> SetDirectory { private get; init; }
        String directoryValue = String.Empty;

        /// <inheritdoc/>
        public String FilePath
        {
            get
            {
                String relativeDirectory = GetDirectory();
                DirectoryType rootFolder = GetRootFolder();
                String rootPath = String.Empty;
                if (rootFolder.GetEnumeration().Directory is DirectoryInfo rootDirectory)
                { rootPath = rootDirectory.FullName; }

                if (rootFolder is DirectoryType.Null && String.IsNullOrWhiteSpace(relativeDirectory))
                { return SpecialDirectories.MyDocuments; }
                else if (rootFolder is DirectoryType.Null) { return relativeDirectory ?? String.Empty; }
                else if (String.IsNullOrWhiteSpace(relativeDirectory)) { return rootPath; }
                else { return Path.Combine(rootPath, relativeDirectory ?? String.Empty); }
            }
            set
            {
                DirectoryType rootFolder = GetRootFolder();
                String rootPath = String.Empty;
                if (rootFolder.GetEnumeration().Directory is DirectoryInfo rootDirectory)
                { rootPath = rootDirectory.FullName; }

                if (value.StartsWith(rootPath))
                {
                    String path = Path.GetRelativePath(rootPath, value);
                    if (path is "." || String.IsNullOrWhiteSpace(path))
                    { SetDirectory(String.Empty); }
                    else { SetDirectory(path); }
                }

                else { SetDirectory(value); }

                OnPropertyChanged(nameof(Directory));
                OnPropertyChanged(nameof(FilePath));
            }
        }

        /// <inheritdoc/>
        public String FileName
        {
            get { return GetFileName(); }
            set { SetFileName(value); OnPropertyChanged(nameof(FileName)); }
        }
        internal Func<String> GetFileName { private get; init; }
        internal Action<String> SetFileName { private get; init; }
        String fileNameValue = String.Empty;

        /// <inheritdoc/>
        public String Content
        {
            get { return GetContent(); }
            set { SetContent(value); OnPropertyChanged(nameof(Content)); }
        }
        internal Func<String> GetContent { private get; init; }
        internal Action<String> SetContent { private get; init; }
        String contentValue = String.Empty;

        /// <summary>
        /// Create an Instance of a DocumentFile.
        /// </summary>
        /// <remarks>
        /// The initial State is not linked to a Document class.
        /// Use Init Get/Set properties to link the Document class.
        /// </remarks>
        public DocumentFile() : base()
        {
            GetDirectory = () => directoryValue;
            SetDirectory = (v) => directoryValue = v;

            GetFileName = () => fileNameValue;
            SetFileName = (v) => fileNameValue = v;

            GetContent = () => contentValue;
            SetContent = (v) => contentValue = v;
        }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="IBindingPropertyChanged.OnPropertyChanged"/>
        /// <remarks>
        /// Do not call this on a background thread.
        /// It will cause threading issues with "FindGoodRow" method.
        /// </remarks>
        protected virtual void OnPropertyChanged(String propertyName)
        { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, propertyName); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open()
        {
            List<WorkItem> work = new List<WorkItem>();
            Boolean cancel = false;

            work.Add(new WorkItem()
            {
                WorkName = String.Format("Opening {0}", FileName),
                DoWork = OnWork,
                IsCanceling = () => cancel
            });

            return work;

            void OnWork()
            {
                FileInfo file = new FileInfo(Path.Combine(FilePath, FileName));

                if (file.Exists)
                {
                    try
                    {
                        SetContent(File.ReadAllText(file.FullName));
                        //OnPropertyChanged(nameof(Content)); // This statement causes threading issues as it occurs on the background thread.
                    }
                    catch (Exception ex)
                    {
                        cancel = true;
                        ex.Data.Add(nameof(FilePath), FilePath);
                        ex.Data.Add(nameof(FileName), FileName);
                        throw;
                    }
                }
                else
                {
                    cancel = true;
                    Exception ex = new FileNotFoundException();

                    ex.Data.Add(nameof(FilePath), FilePath);
                    ex.Data.Add(nameof(FileName), FileName);
                    throw ex;
                }
            }
        }


        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save()
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
                FileInfo file = new FileInfo(Path.Combine(FilePath, FileName));

                try
                { File.WriteAllText(Path.Combine(FilePath, FileName), GetContent()); }
                catch (Exception ex)
                {
                    cancel = true;
                    ex.Data.Add(nameof(FilePath), FilePath);
                    ex.Data.Add(nameof(FileName), FileName);
                    throw;
                }
            }
        }

        /// <summary>
        /// Try/Parse the Content into an XDocument.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exception"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public Boolean TryParse([NotNullWhen(true)] out XDocument? document, [NotNullWhen(false)] out Exception? exception, LoadOptions option = LoadOptions.PreserveWhitespace)
        {
            try
            {
                document = XDocument.Parse(Content, option);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                document = null;
                ex.Data.Add(nameof(FilePath), FilePath);
                ex.Data.Add(nameof(FileName), FileName);
                exception = ex;
                return false;
            }
        }
    }
}
