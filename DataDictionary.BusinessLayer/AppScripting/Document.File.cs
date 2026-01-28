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
        /// Full File Path of the File (includes root).
        /// </summary>
        /// <remarks>Use this to set the directory.</remarks>
        String FilePath { get; set; }

        /// <summary>
        /// File Name for the File within the File Path.
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// Relative File Path from the Root Folder and File Name.
        /// </summary>
        /// <remarks>Computed from FilePath, Root Folder and FileName</remarks>
        String RelativeFileName { get; }

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
                { return String.Empty; }
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

                this.OnPropertyChanged(PropertyChanged, nameof(FilePath));
                this.OnPropertyChanged(PropertyChanged, nameof(RelativeFileName));
            }
        }

        /// <inheritdoc/>
        public String RelativeFileName
        { get { return Path.Combine(GetDirectory(), FileName); } }

        /// <inheritdoc/>
        public String FileName
        {
            get { return GetFileName(); }
            set
            {
                SetFileName(value);
                this.OnPropertyChanged(PropertyChanged, nameof(FileName));
                this.OnPropertyChanged(PropertyChanged, nameof(RelativeFileName));
            }
        }
        internal Func<String> GetFileName { private get; init; }
        internal Action<String> SetFileName { private get; init; }
        String fileNameValue = String.Empty;

        /// <inheritdoc/>
        public String Content
        {
            get { return GetContent(); }
            set
            {
                SetContent(value);
                this.OnPropertyChanged(PropertyChanged, nameof(Content));
            }
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
            GetDirectory = () =>
            {
                if (String.IsNullOrWhiteSpace(directoryValue)
                && GetRootFolder().GetEnumeration().Directory is DirectoryInfo directory)
                { return directory.FullName; }
                else { return directoryValue; }
            };
            SetDirectory = (v) => directoryValue = v;

            GetFileName = () => fileNameValue;
            SetFileName = (v) => fileNameValue = v;

            GetContent = () => contentValue;
            SetContent = (v) => contentValue = v;
        }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual event PropertyChangedEventHandler? PropertyChanged;

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
                        this.OnPropertyChanged(PropertyChanged, (nameof(Content)));
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
                {
                    // Detect if the data is XML and use XML save instead of normal text.
                    // TODO: This is still adding the Byte Order Mark (BOM) to the file.
                    // This is not necessary an in some cases, may cause issues with other tools.
                    if (TryParse(out XDocument? document, out Exception? _))
                    { document.Save(Path.Combine(FilePath, FileName)); }
                    else // Save the file as Text. This is expected to have a BOM.
                    { File.WriteAllText(Path.Combine(FilePath, FileName), GetContent()); }
                }
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

        /// <summary>
        /// Try/Parse the Context into a Formatted XML String.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="exception"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public Boolean TryParse([NotNullWhen(true)] out String? document, [NotNullWhen(false)] out Exception? exception, LoadOptions option = LoadOptions.PreserveWhitespace)
        {
            //Note: Online Sources use StringWriter to convert an XDocument to String.
            //This alters the Declaration of the XDocument and forces it to UTF-16, which is the format of Windows Strings.
            //Other solutions run the XDcument thru several more steps that also alter the Declaration or require
            //that the correct Declaration to be known and that is be compatible with a String Encoding.
            //This approach is to add the Declaration using the StringBuilder as a simple string.

            if (TryParse(out XDocument? value, out Exception? xmlException, option))
            {
                StringBuilder result = new StringBuilder();

                // XDocument.ToString() does not contain the Header, put that back in.
                if (value.Declaration is XDeclaration declaration)
                { result.Append(declaration.ToString()); }
                //else { result.AppendLine(new XDeclaration(null, null, null).ToString()); }

                result.AppendLine(value.ToString());

                exception = null;
                document = result.ToString();
                return true;
            }
            else
            { document = null; exception = xmlException; return false; }
        }
    }
}
