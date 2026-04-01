using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <summary>
    /// File Pattern for the Template.
    /// </summary>
    [Obsolete]
    public interface ITemplateFile: IDirectoryValue
    {
        /// <summary>
        /// Prefix to be used for the FileName
        /// </summary>
        String Prefix { get; set; }

        /// <summary>
        /// Suffix to be used for the FileName
        /// </summary>
        String Suffix { get; set; }

        /// <summary>
        /// Extension to be used for the FileName
        /// </summary>
        String Extension { get; set; }

        /// <summary>
        /// Formats the FileName for an Object
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        String FileName(String? objectName);

        /// <inheritdoc cref="FileName(String?)"/>
        String FileName(IAttributeIndexName attribute);

        /// <inheritdoc cref="FileName(String?)"/>
        String FileName(IEntityIndexName entity);

        /// <inheritdoc cref="FileName(String?)"/>
        String FileName(IProcessIndexName process);
    }

    /// <summary>
    /// File Pattern for the Template.
    /// </summary>
    [Obsolete]
    public class TemplateFile : DirectoryValue, ITemplateFile
    {
        /// <summary>
        /// Function representing Get function for the Prefix.
        /// </summary>
        protected internal Func<String> GetPrefix { protected get; init; }

        /// <summary>
        /// Function representing Set method for the Prefix.
        /// </summary>
        protected internal Action<String> SetPrefix { protected get; init; }
        String prefixValue = String.Empty;

        /// <summary>
        /// Function representing Get function for the Suffix.
        /// </summary>
        protected internal Func<String> GetSuffix { protected get; init; }

        /// <summary>
        /// Function representing Set method for the Suffix.
        /// </summary>
        protected internal Action<String> SetSuffix { protected get; init; }
        String suffixValue = String.Empty;

        /// <summary>
        /// Function representing Get function for the Extension.
        /// </summary>
        protected internal Func<String> GetExtension { protected get; init; }

        /// <summary>
        /// Function representing Set method for the Extension.
        /// </summary>
        protected internal Action<String> SetExtension { protected get; init; }
        String extensionValue = String.Empty;

        /// <inheritdoc/>
        public String Prefix
        {
            get { return GetPrefix(); }
            set
            {
                SetPrefix(value);
                OnPropertyChanged(nameof(Prefix));
            }
        }

        /// <inheritdoc/>
        public String Suffix
        {
            get { return GetSuffix(); }
            set
            {
                SetSuffix(value);
                OnPropertyChanged(nameof(Suffix));
            }
        }

        /// <inheritdoc/>
        public String Extension
        {
            get { return GetExtension(); }
            set
            {
                SetExtension(value);
                OnPropertyChanged(nameof(Extension));
            }
        }

        /// <inheritdoc/>
        public String FileName(String? objectName)
        { 
            if(String.IsNullOrWhiteSpace(objectName))
            { throw new ArgumentNullException(nameof(objectName)); }

            if (String.IsNullOrWhiteSpace(Extension))
            { throw new ArgumentNullException(nameof(Extension)); }

            return String.Format("{0}{1}{2}.{3}", Prefix, objectName, Suffix, Extension); 
        }

        /// <inheritdoc/>
        public String FileName(IAttributeIndexName attribute)
        { return FileName(attribute.AttributeTitle); }

        /// <inheritdoc/>
        public String FileName(IEntityIndexName entity)
        { return FileName(entity.EntityTitle); }

        /// <inheritdoc/>
        public String FileName(IProcessIndexName process)
        { return FileName(process.ProcessTitle); }

        /// <summary>
        /// Create an Instance of a TemplateFile.
        /// </summary>
        /// <remarks>
        /// The initial State is not linked to a base class.
        /// Use Init Get/Set properties to link the base class.
        /// </remarks>
        public TemplateFile() : base()
        {
            GetPrefix = () => prefixValue;
            SetPrefix = (v) => prefixValue = v;

            GetSuffix = () => suffixValue;
            SetSuffix = (v) => suffixValue = v;

            GetExtension = () => Extension;
            SetExtension = (v) => Extension = v;
        }
    }
}
