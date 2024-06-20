using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Template;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : IScriptingTemplateItem, ITemplateIndex, ITemplateIndexName
    {
        /// <summary>
        /// Transform Script as XElement
        /// </summary>
        XElement? TransformXml { get; }

        /// <summary>
        /// Transform Script Exception when converted to XElement
        /// </summary>
        Exception? TransformException { get; }
    }

    /// <inheritdoc/>
    public class TemplateValue : ScriptingTemplateItem, ITemplateValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public TemplateValue() : base()
        { PropertyChanged += TemplateValue_PropertyChanged; }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new TemplateIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return this.TemplateTitle ?? Scope.ToName(); }

        /// <inheritdoc/>
        public XElement? TransformXml { get; private set; } = null;

        /// <inheritdoc/>
        public Exception? TransformException { get; private set; } = null;

        private void TemplateValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(TransformScript))
            {
                try
                {
                    TransformXml = XElement.Parse(TransformScript ?? String.Empty);
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
    }
}
