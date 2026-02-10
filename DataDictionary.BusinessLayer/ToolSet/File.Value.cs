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
        /// </summary>
        String FileName { get; set; }

        /// <summary>
        /// Relative File Path from the Root Folder and File Name.
        /// </summary>
        /// <remarks>Computed from Root Folder, Relative Directory and FileName</remarks>
        String RelativeFileName { get; }
    }

    /// <summary>
    /// Represents a Single File.
    /// Used to hold file information.
    /// This is a Wrapper around the fields in the base table so that they can be treated as a single unit.
    /// </summary>
    public abstract class FileValue : DirectoryValue, IFileValue
    {
        /// <inheritdoc/>
        public override String DirectoryPath
        {
            get { return base.DirectoryPath; }
            set
            {
                base.DirectoryPath = value;
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
        }

    }
}
