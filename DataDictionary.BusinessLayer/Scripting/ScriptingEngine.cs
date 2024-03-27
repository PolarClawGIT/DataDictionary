using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface representing Scripting Engine data
    /// </summary>
    public interface IScriptingEngine: 
        ISaveData, ILoadData
    {
        /// <summary>
        /// List of Scripting Engine Schemta.
        /// </summary>
        ISchemaData Schemta { get; }

        /// <summary>
        /// List of Scripting Engine Schema Elements.
        /// </summary>
        IElementData Elements { get; }

        /// <summary>
        /// List of Scripting Engine Transforms.
        /// </summary>
        ITransformData Transforms { get; }

        /// <summary>
        /// List of Scripting Engine Column definitions
        /// </summary>
        IColumnData Columns { get; }
    }

    /// <summary>
    /// Implementation for Scripting Engine data
    /// </summary>
    class ScriptingEngine : IScriptingEngine, IDataTableFile, INamedScopeData<IModelKey>
    {
        /// <inheritdoc/>
        public ISchemaData Schemta { get { return schemtaValues; } }
        private readonly SchemaData schemtaValues;

        public IElementData Elements { get { return elementValues; } }
        private readonly ElementData elementValues;

        public ITransformData Transforms { get { return transformValues; } }
        private readonly TransformData transformValues;

        public IColumnData Columns { get { return columnValues; } }
        private readonly ColumnData columnValues;

        public ScriptingEngine() : base()
        {
            schemtaValues = new SchemaData();
            elementValues = new ElementData();
            transformValues = new TransformData();
            columnValues = new ColumnData();
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Load(factory));
            work.AddRange(elementValues.Load(factory));
            work.AddRange(transformValues.Load(factory));
            work.Add(new WorkItem() { DoWork = columnValues.Load });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Save(factory));
            work.AddRange(elementValues.Save(factory));
            work.AddRange(transformValues.Save(factory));

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<DataTable> Export()
        {
            List<System.Data.DataTable> result = new List<System.Data.DataTable>();
            result.Add(schemtaValues.ToDataTable());
            result.Add(elementValues.ToDataTable());
            result.Add(transformValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public void Import(DataSet source)
        {
            schemtaValues.Load(source);
            elementValues.Load(source);
            transformValues.Load(source);
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target, Func<IModelKey?> parent)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Export(target, parent));
            work.AddRange(transformValues.Export(target, parent));

            return work;
        }
    }
}
