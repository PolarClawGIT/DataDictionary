using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public interface IScriptingTemplateValue : IScriptingTemplateItem, IScriptingTemplateIndex, IScriptingTemplateName
    {
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
    }

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public class ScriptingTemplateValue : ScriptingTemplateItem, IScriptingTemplateValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScriptingTemplateValue() : base()
        {
            PropertyChanged += TemplateValue_PropertyChanged;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new ScriptingTemplateIndex(this),
                GetPath = () => new PathIndex(TemplateTitle),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TemplateTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TemplateTitle)
            };
        }

        /// <inheritdoc/>
        public XDocument? TransformXml { get; private set; } = null;

        /// <inheritdoc/>
        public Exception? TransformException { get; private set; } = null;

        /// <inheritdoc/>
        public IEnumerable<Exception> TemplateException { get { return templateException; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTemplate; } }

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
        }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(TemplateTitle); }
    }
}
