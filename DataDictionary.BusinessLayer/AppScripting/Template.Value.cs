using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateValue : ITemplateItem, ITemplateIndex,
        IScopeType, ITemporal
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

        /// <summary>
        /// Speical Directory Name used to determine the Root Directory.
        /// </summary>
        TemplateDirectoryType TemplateDirectory { get; }

        /// <summary>
        /// The Scripting Break On Scope.
        /// </summary>
        ScopeType TemplateBreakOn { get; }
    }

    /// <inheritdoc/>
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
        public TemplateDirectoryType TemplateDirectory
        {
            get
            {
                String? value = GetValue(nameof(RootDirectory));
                if (TemplateDirectoryEnumeration.TryParse(value, null, out TemplateDirectoryEnumeration? result))
                { return result.Value; }
                else { return TemplateDirectoryType.Null; }
            }
            set
            {
                if (value is TemplateDirectoryType.Null)
                { SetValue(nameof(RootDirectory), null); }
                else { SetValue(nameof(RootDirectory), TemplateDirectoryEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public ScopeType TemplateBreakOn
        {
            get
            {
                String value = GetValue(nameof(BreakOnScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(BreakOnScope), null); }
                else { SetValue(nameof(BreakOnScope), ScopeEnumeration.Cast(value).Name); }
            }
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
                GetTitle = () => TemplateTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TemplateTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TemplateTitle)
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

            if (e.PropertyName is nameof(RootDirectory))
            { OnPropertyChanged(nameof(TemplateDirectory)); }

            if (e.PropertyName is nameof(BreakOnScope))
            { OnPropertyChanged(nameof(TemplateBreakOn)); }
        }

    }
}
