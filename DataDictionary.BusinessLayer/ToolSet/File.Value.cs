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
        /// The File Content
        /// </summary>
        String FileContent { get; }

        /// <summary>
        /// List of FileFormats supported. Normally only one.
        /// </summary>
        IEnumerable<FileFormatType> FileFormats { get; }

        /// <summary>
        /// Generate the WorkItems for Opens the File and loads it to the FileContent property
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Open(IDirectoryValue directory);

        /// <summary>
        /// Generate the WorkItems for Saves the FileContent property to the file
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(IDirectoryValue directory);

        /// <summary>
        /// Validates the File info and returns an Exception if there is an issue.
        /// </summary>
        /// <returns></returns>
        Boolean IsInvalid([NotNullWhen(true)] out Exception? exception);
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

        /// <inheritdoc/>
        public String FileContent
        {
            get { return GetContent(); }
            set
            {
                SetContent(value);
                OnPropertyChanged(nameof(FileContent));
            }
        }
        internal Func<String> GetContent { private get; init; }
        internal Action<String> SetContent { private get; init; }

        String contentValue = String.Empty;

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
            GetFileName = () => fileNameValue;
            SetFileName = (v) => fileNameValue = v;

            GetFileFormats = () => fileFormatValues;

            GetContent = () => contentValue;
            SetContent = (v) => contentValue = v;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(IDirectoryValue directory)
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
                FileInfo file = new FileInfo(Path.Combine(directory.InitialDirectory, FileName));

                if (file.Exists)
                {
                    try
                    {
                        SetContent(File.ReadAllText(file.FullName));
                        OnPropertyChanged(nameof(FileContent));
                    }
                    catch (Exception ex)
                    {
                        cancel = true;
                        ex.Data.Add(nameof(directory.InitialDirectory), directory.InitialDirectory);
                        ex.Data.Add(nameof(FileName), FileName);
                        throw;
                    }
                }
                else
                {
                    cancel = true;
                    Exception ex = new FileNotFoundException();

                    ex.Data.Add(nameof(directory.InitialDirectory), directory.InitialDirectory);
                    ex.Data.Add(nameof(FileName), FileName);
                    throw ex;
                }
            }


        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDirectoryValue directory)
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
                FileInfo file = new FileInfo(Path.Combine(directory.InitialDirectory, FileName));

                try
                {
                    // Detect if the data is XML and use XML save instead of normal text.
                    // TODO: This is still adding the Byte Order Mark (BOM) to the file.
                    // This is not necessary an in some cases, may cause issues with other tools.
                    if (TryParse(out XDocument? document, out Exception? _))
                    { document.Save(Path.Combine(directory.InitialDirectory, FileName)); }
                    else // Save the file as Text. This is expected to have a BOM.
                    { File.WriteAllText(Path.Combine(directory.InitialDirectory, FileName), GetContent()); }
                }
                catch (Exception ex)
                {
                    cancel = true;
                    ex.Data.Add(nameof(directory.InitialDirectory), directory.InitialDirectory);
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
                document = XDocument.Parse(FileContent, option);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                document = null;
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
            //Other solutions run the XDocument thru several more steps that also alter the Declaration or require
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

        /// <inheritdoc/>
        public Boolean IsInvalid([NotNullWhen(true)] out Exception? exception)
        {
            exception = null;

            if (String.IsNullOrWhiteSpace(FileName))
            { exception = new ArgumentNullException(nameof(FileName)); }
            else if (FileName.Any(a => Path.GetInvalidFileNameChars().Contains(a)))
            {
                exception = new ArgumentException("Invalid FileName Character(s)");
                exception.Data.Add(nameof(FileName), FileName);
            }
            else
            {
                try
                { var file = new FileInfo(FileName); }
                catch (Exception ex)
                {
                    exception = ex;
                    exception.Data.Add(nameof(FileName), FileName);
                }
            }

            return exception is not null;
        }
    }
}
