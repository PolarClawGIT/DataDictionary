using DataDictionary.BusinessLayer.DbWorkItem;
using System.Data;
using Toolbox.Threading;
using Toolbox.BindingTable;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.BusinessLayer.Model;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface representing Scripting Engine data
    /// </summary>
    public interface IScriptingEngine :
        ISaveData, ILoadData, IRemoveData,
        ILoadData<IModelKey>, ISaveData<IModelKey>
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
    class ScriptingEngine : IScriptingEngine, IDataTableFile,
        INamedScopeData
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData Models { get; init; }

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
            schemtaValues = new SchemaData() { Scripting = this };
            elementValues = new ElementData();
            transformValues = new TransformData() { Scripting = this };
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
        /// <remarks>Scripting (Currently load all)</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Load(factory));
            work.AddRange(elementValues.Load(factory));
            work.AddRange(transformValues.Load(factory));
            work.Add(new WorkItem() { DoWork = columnValues.Load });

            return work;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting (Currently saves all)</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Save(factory));
            work.AddRange(elementValues.Save(factory));
            work.AddRange(transformValues.Save(factory));

            return work;
        }

        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Scripting Schemta", DoWork = () => { schemtaValues.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Scripting Elements", DoWork = () => { elementValues.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Scripting Transforms", DoWork = () => { transformValues.Clear(); } });

            return work;
        }

        public IReadOnlyList<WorkItem> Build(NamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Build(target));
            work.AddRange(transformValues.Build(target));

            return work;
        }
    }
}
