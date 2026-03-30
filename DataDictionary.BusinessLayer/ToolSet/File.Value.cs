using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for Single File.
    /// </summary>
    public interface IFileValue : IDirectoryValue, IBindingPropertyChanged
    {
        /// <summary>
        /// File Name for the File within the File Path.
        /// dialog.FileName = IFileValue.FileName
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// Relative File Path from the Root Folder and File Name.
        /// dialog.InitialDirectory = IFileValue.InitialDirectory
        /// </summary>
        /// <remarks>Computed from Root Folder, Relative Directory and FileName</remarks>
        [Obsolete ("redundant with InitialDirectory")]
        String RelativeFileName { get; }

        /// <summary>
        /// List of FileFormats supported. Normally only one.
        /// </summary>
        IEnumerable<FileFormatType> FileFormats { get; }
    }

    /// <summary>
    /// Represents a Single File.
    /// Used to hold file information for use with the File Open/Save Dialog.
    /// This is a Wrapper around the fields in the base table so that they can be treated as a single unit.
    /// </summary>
    public class FileValue : DirectoryValue, IFileValue
    {
        /// <inheritdoc/>
        public override String InitialDirectory
        {
            get { return base.InitialDirectory; }
            set
            {
                base.InitialDirectory = value;
                OnPropertyChanged(nameof(RelativeFileName));
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
                OnPropertyChanged(nameof(FileName));
                OnPropertyChanged(nameof(RelativeFileName));
            }
        }

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
        }

    }
}
