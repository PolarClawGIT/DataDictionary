using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateValue : ITemplateItem, ITemplateIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Document File Pattern data
        /// </summary>
        TemplateFile DocumentValue { get; }

        /// <summary>
        /// Script File Pattern data
        /// </summary>
        TemplateFile ScriptValue { get; }

        /// <summary>
        /// Transform Script as XDocument
        /// </summary>
        XDocument? TransformXml { get; }

        /// <summary>
        /// Template Script Exception
        /// </summary>
        Exception? TransformException { get; }

        /// <summary>
        /// Exception encountered while Scripting.
        /// </summary>
        IEnumerable<Exception> TemplateException { get; }

        /// <summary>
        /// The Scripting Break On Scope.
        /// </summary>
        ScopeType TemplateBreakOn { get; }
    }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateValue : TemplateItem, ITemplateValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTemplate; } }

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        public XDocument? TransformXml { get; private set; } = null;

        /// <inheritdoc/>
        public Exception? TransformException { get; private set; } = null;

        /// <inheritdoc/>
        public IEnumerable<Exception> TemplateException { get { return templateException; } }

        /// <inheritdoc/>
        public TemplateFile DocumentValue { get; }

        /// <inheritdoc/>
        public TemplateFile ScriptValue { get; }

        /// <inheritdoc/>
        public ScopeType TemplateBreakOn
        {
            get
            {
                String value = GetValue(nameof(BreakOnScope)) ?? String.Empty;
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(BreakOnScope), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public TemplateValue() : base()
        {
            PropertyChanged += TemplateValue_PropertyChanged;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new TemplateIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TemplateTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TemplateTitle)
            };


            DocumentValue = new TemplateFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => DocumentDirectory ?? String.Empty,
                SetDirectory = (v) => DocumentDirectory = v, 
                GetExtension = () => DocumentExtension ?? String.Empty,
                SetExtension = (v) => DocumentExtension = v,
                GetPrefix = () => DocumentPrefix ?? String.Empty,
                SetPrefix = (v) => DocumentPrefix = v,
                GetSuffix = () => DocumentSuffix ?? String.Empty,
                SetSuffix = (v) => DocumentSuffix = v,
            };

            ScriptValue = new TemplateFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => ScriptDirectory ?? String.Empty,
                SetDirectory = (v) => ScriptDirectory = v,
                GetExtension = () => ScriptExtension ?? String.Empty,
                SetExtension = (v) => ScriptExtension = v,
                GetPrefix = () => ScriptPrefix ?? String.Empty,
                SetPrefix = (v) => ScriptPrefix = v,
                GetSuffix = () => ScriptSuffix ?? String.Empty,
                SetSuffix = (v) => ScriptSuffix = v,
            };
        }

        List<Exception> templateException = new List<Exception>();

        internal void AddException(Exception ex)
        { templateException.Add(ex); }

        private void TemplateValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(TransformScript))
            {
                try
                {
                    TransformXml = XDocument.Parse(TransformScript ?? String.Empty, LoadOptions.PreserveWhitespace);
                    TransformException = null;
                    OnPropertyChanged(nameof(TransformXml));
                    OnPropertyChanged(nameof(TransformException));
                }
                catch (Exception ex)
                {
                    TransformXml = null;
                    TransformException = ex;
                    OnPropertyChanged(nameof(TransformXml));
                    OnPropertyChanged(nameof(TransformException));
                }
            }

            if (e.PropertyName is nameof(BreakOnScope))
            { OnPropertyChanged(nameof(TemplateBreakOn)); }
        }

    }
}
