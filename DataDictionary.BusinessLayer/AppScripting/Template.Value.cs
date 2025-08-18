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
    }

    /// <inheritdoc/>
    public class TemplateValue : TemplateItem, ITemplateValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public XDocument? TransformXml { get; private set; } = null;

        /// <inheritdoc/>
        public Exception? TransformException { get; private set; } = null;

        /// <inheritdoc/>
        public IEnumerable<Exception> TemplateException { get { return templateException; } }

        /// <inheritdoc/>
        public TemplateValue() : base()
        {
            PropertyChanged += TemplateValue_PropertyChanged;

            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateIndex(this),
                GetScope = () => Scope,
                GetTitle = () => TemplateTitle ?? ScopeEnumeration.Cast(Scope).Name,
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
        }

    }
}
