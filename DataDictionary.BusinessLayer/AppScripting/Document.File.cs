using DataDictionary.BusinessLayer.ToolSet;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface for Single File of a Document.
    /// </summary>
    [Obsolete("POC code")]
    public interface IDocumentFile : IFileValue, IDirectoryValue, IBindingPropertyChanged
    {
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
    [Obsolete("POC code")]
    public class DocumentFile : FileValue, IDocumentFile
    {
        /// <inheritdoc/>
        public String Content
        {
            get { return GetContent(); }
            set
            {
                SetContent(value);
                OnPropertyChanged(nameof(Content));
            }
        }
        internal Func<String> GetContent { private get; init; }
        internal Action<String> SetContent { private get; init; }

        /// <inheritdoc/>
        public Environment.SpecialFolder RootFolder => throw new NotImplementedException();

        /// <inheritdoc/>
        public String InitialDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            GetContent = () => contentValue;
            SetContent = (v) => contentValue = v;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open()
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
                FileInfo file = new FileInfo(Path.Combine(InitialDirectory, FileName));

                if (file.Exists)
                {
                    try
                    {
                        SetContent(File.ReadAllText(file.FullName));
                        OnPropertyChanged(nameof(Content));
                    }
                    catch (Exception ex)
                    {
                        cancel = true;
                        ex.Data.Add(nameof(InitialDirectory), InitialDirectory);
                        ex.Data.Add(nameof(FileName), FileName);
                        throw;
                    }
                }
                else
                {
                    cancel = true;
                    Exception ex = new FileNotFoundException();

                    ex.Data.Add(nameof(InitialDirectory), InitialDirectory);
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
                FileInfo file = new FileInfo(Path.Combine(InitialDirectory, FileName));

                try
                {
                    // Detect if the data is XML and use XML save instead of normal text.
                    // TODO: This is still adding the Byte Order Mark (BOM) to the file.
                    // This is not necessary an in some cases, may cause issues with other tools.
                    if (TryParse(out XDocument? document, out Exception? _))
                    { document.Save(Path.Combine(InitialDirectory, FileName)); }
                    else // Save the file as Text. This is expected to have a BOM.
                    { File.WriteAllText(Path.Combine(InitialDirectory, FileName), GetContent()); }
                }
                catch (Exception ex)
                {
                    cancel = true;
                    ex.Data.Add(nameof(InitialDirectory), InitialDirectory);
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
                ex.Data.Add(nameof(InitialDirectory), InitialDirectory);
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
    }
}
