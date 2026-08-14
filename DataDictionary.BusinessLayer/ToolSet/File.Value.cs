using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

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
        String? FileContent { get; }

        /// <summary>
        /// List of FileFormats supported. Normally only one.
        /// </summary>
        IEnumerable<FileFormatType> FileFormats { get; }

        /// <summary>
        /// Opens the File and loads it to the FileContent property
        /// </summary>
        /// <param name="directory"></param>
        void Open(IDirectoryValue directory);

        /// <summary>
        /// Saves the FileContent property to the file
        /// </summary>
        /// <param name="directory"></param>
        void Save(IDirectoryValue directory);
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
        public String? FileContent { get; set; }

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
        }

        /// <inheritdoc/>
        public void Open(IDirectoryValue directory)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IDirectoryValue directory)
        {
            throw new NotImplementedException();
        }



    }
}
