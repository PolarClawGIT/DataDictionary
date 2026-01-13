using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.Threading;

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

        /// <summary>
        /// Loads the values from the files as defined by Input and Result paths.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> LoadFiles();

        /// <summary>
        /// Saves the values to files as defined by Input and Result paths.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> SaveFiles();

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
                String? value = GetValue(nameof(RootFolder));
                if (value.TryParse(out DirectoryType result))
                { return result; }
                else { return DirectoryType.Null; }
            }
            set { SetValue(nameof(RootFolder), value.GetEnumeration().Name); }
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
            try
            {

                OnPropertyChanged(nameof(ResultData));
            }
            catch (Exception)
            {

                throw;
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadFiles()
        {
            if (String.IsNullOrEmpty(DocumentTitle))
            { throw new ArgumentNullException(nameof(DocumentTitle)); }

            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = String.Format("Load Document {0}", DocumentTitle), DoWork = LoadData });
            return work;

            void LoadData()
            {
                try
                {
                    OnPropertyChanged(nameof(InputData));
                    OnPropertyChanged(nameof(ResultData));
                }
                catch (Exception)
                {

                    throw;
                }

                

                throw new NotImplementedException();
            }
        }


        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> SaveFiles()
        {
            if (String.IsNullOrEmpty(DocumentTitle))
            { throw new ArgumentNullException(nameof(DocumentTitle)); }


            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = String.Format("Save Document {0}", DocumentTitle), DoWork = SaveData });
            return work;

            void SaveData()
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                throw new NotImplementedException();
            }
        }


    }
}
