using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ScriptingData;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : IScriptingTemplateItem, ITemplateIndex, ITemplateIndexName
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
    public class TemplateValue : ScriptingTemplateItem, ITemplateValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public TemplateValue() : base()
        {
            PropertyChanged += TemplateValue_PropertyChanged;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new TemplateIndex(this),
                GetPath = () => new PathIndex(TemplateTitle),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? ScopeEnumeration.Cast(Scope).Name,
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
