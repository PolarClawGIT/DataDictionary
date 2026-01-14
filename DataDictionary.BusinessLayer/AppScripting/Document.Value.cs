using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using Microsoft.VisualBasic.FileIO;
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
        /// Input XML data
        /// </summary>
        /// 
        String? InputText { get; }

        /// <summary>
        /// Results of the XML Transform
        /// </summary>
        String? ResultText { get; }

        /// <summary>
        /// Exception to the XML Transform or other processing errors.
        /// </summary>
        String? DocumentException { get; }

        /// <summary>
        /// Root Path determined by the RootFolder value.
        /// </summary>
        String RootPath { get; }

        /// <summary>
        /// Full Path to the Input File (not including file name)
        /// </summary>
        String InputPath { get; }

        /// <summary>
        /// Full Path to the OutputPath File (not including file name)
        /// </summary>
        String OutputPath { get; }

        /// <summary>
        /// Directory that the Transform was Saved to (Not saved to database)
        /// </summary>
        String? TransformPath { get; }

        /// <summary>
        /// File Name of the Transformed that was Saved to (Not saved to database)
        /// </summary>
        String? TransformFile { get; }

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
        public String RootPath
        {
            get
            {

                if (RootFolder.GetEnumeration().Directory is DirectoryInfo directory)
                { return directory.FullName; }
                else { return String.Empty; }
            }
        }

        /// <inheritdoc/>
        public String InputPath
        {
            get
            {
                if (RootFolder is DirectoryType.Null && String.IsNullOrWhiteSpace(InputDirectory))
                { return SpecialDirectories.MyDocuments; }
                else if (RootFolder is DirectoryType.Null) { return InputDirectory ?? String.Empty; }
                else if (String.IsNullOrWhiteSpace(InputDirectory)) { return RootPath; }
                else { return Path.Combine(RootPath, InputDirectory ?? String.Empty); }
            }
            set
            {
                if (value.StartsWith(RootPath))
                {
                    String path = Path.GetRelativePath(RootPath, value);
                    if (path is "." || String.IsNullOrWhiteSpace(path))
                    { InputDirectory = null; }
                    else { InputDirectory = path; }
                }

                else { InputDirectory = value; }

                OnPropertyChanged(nameof(InputPath));
            }
        }

        /// <inheritdoc/>
        public String OutputPath
        {
            get
            {
                if (RootFolder is DirectoryType.Null && String.IsNullOrWhiteSpace(OutputDirectory))
                { return SpecialDirectories.MyDocuments; }
                else if (RootFolder is DirectoryType.Null) { return OutputDirectory ?? String.Empty; }
                else if (String.IsNullOrWhiteSpace(OutputDirectory)) { return RootPath; }
                else { return Path.Combine(RootPath, InputDirectory ?? String.Empty); }
            }
            set
            {
                if (value.StartsWith(RootPath))
                {
                    String path = Path.GetRelativePath(RootPath, value);
                    if (path is "." || String.IsNullOrWhiteSpace(path))
                    { OutputDirectory = null; }
                    else { OutputDirectory = path; }
                }
                else { OutputDirectory = value; }

                OnPropertyChanged(nameof(OutputPath));
            }

        }

        /// <inheritdoc/>
        public String? TransformPath
        {
            get
            {
                if (String.IsNullOrEmpty(transformDirectory))
                { return RootPath; }
                else { return transformDirectory; }
            }
            set { transformDirectory = value; OnPropertyChanged(nameof(TransformPath)); }
        }
        String? transformDirectory { get; set; } = String.Empty;

        /// <inheritdoc/>
        public String? TransformFile
        {
            get
            {
                if (String.IsNullOrWhiteSpace(transformFile))
                { return Path.ChangeExtension(InputFile, "XSL"); }
                else { return transformFile; }
            }

            set { transformFile = value; OnPropertyChanged(nameof(TransformFile)); }
        }
        String? transformFile = String.Empty;


        /// <inheritdoc/>
        public String? InputText { get; set; }

        /// <inheritdoc/>
        public String? ResultText { get; }

        /// <inheritdoc/>
        public String? DocumentException { get; }

        /// <inheritdoc/>
        public DocumentValue() : base()
        {
            PropertyChanged += DocumentValue_PropertyChanged;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(DocumentTitle),
                GetScope = () => Scope,
                GetTitle = () => DocumentTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DocumentTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DocumentTitle)
            };

            void DocumentValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(RootFolder))
                {
                    OnPropertyChanged(nameof(RootPath));
                    OnPropertyChanged(nameof(InputPath));
                    OnPropertyChanged(nameof(OutputPath));
                }
            }
        }


        /// <inheritdoc/>
        public void DoTransform()
        {
            try
            {

                OnPropertyChanged(nameof(ResultText));
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
                    OnPropertyChanged(nameof(InputText));
                    OnPropertyChanged(nameof(ResultText));
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
