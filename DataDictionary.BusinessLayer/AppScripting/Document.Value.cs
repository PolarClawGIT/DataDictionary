using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentValue : IDocumentItem, IDocumentIndex, ITemplateIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Speical Directory Name used to determine the Root Directory.
        /// </summary>
        DirectoryType SpecialDirectory { get; }

        /// <summary>
        /// Input XML data
        /// </summary>
        /// 
        String? InputData { get; }

        /// <summary>
        /// Results of the XML Transform
        /// </summary>
        String? ResultData { get; }

        /// <summary>
        /// Exception to the XML Transform or other processing errors.
        /// </summary>
        String? ExceptionData { get; }

        /// <summary>
        /// Executes the XML Transform, filling Results and Exception.
        /// </summary>
        void DoTransform();
    }

    /// <inheritdoc/>
    public class DocumentValue : DocumentItem, IDocumentValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }

        /// <inheritdoc/>
        public DirectoryType SpecialDirectory
        {
            get
            {
                String? value = GetValue(nameof(SpecialFolder));
                if (value.TryParse(out DirectoryType result))
                { return result; }
                else { return DirectoryType.Null; }
            }
            set { SetValue(nameof(SpecialFolder), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? InputData { get; set; }

        /// <inheritdoc/>
        public String? ResultData { get; }

        /// <inheritdoc/>
        public String? ExceptionData { get; }

        /// <inheritdoc/>
        public DocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(DocumentTitle),
                GetScope = () => Scope,
                GetTitle = () => DocumentTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DocumentTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DocumentTitle)
            };
        }

        /// <inheritdoc/>
        public void DoTransform()
        {

        }
    }
}
