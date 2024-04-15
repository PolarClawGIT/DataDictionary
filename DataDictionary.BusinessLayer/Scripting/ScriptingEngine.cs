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
        ILoadData<IModelKey>, ISaveData<IModelKey>,
        INamedScopeData
    {
        /// <summary>
        /// List of Scripting Engine Schemta.
        /// </summary>
        ISchemaData Schemta { get; }

        /// <summary>
        /// List of Scripting Engine Scheme Elements.
        /// </summary>
        IElementData SchemeElements { get; }

        /// <summary>
        /// List of Scripting Engine Transforms.
        /// </summary>
        ITransformData Transforms { get; }

        /// <summary>
        /// List of Scripting Engine Selections.
        /// </summary>
        ISelectionData Selections { get; }

        /// <summary>
        /// List of Scripting Engine Selection Instances.
        /// </summary>
        ISelectionPathData SelectionInstances { get; }

        /// <summary>
        /// List of Scripting Engine Column definitions
        /// </summary>
        IColumnData Columns { get; }
    }

    /// <summary>
    /// Implementation for Scripting Engine data
    /// </summary>
    class ScriptingEngine : IScriptingEngine, IDataTableFile
    {
        /// <summary>
        /// Reference to the containing Model
        /// </summary>
        public required IModelData Models { get; init; }

        public ISchemaData Schemta { get { return schemtaValues; } }
        private readonly SchemaData schemtaValues;

        public IElementData SchemeElements { get { return elementValues; } }
        private readonly ElementData elementValues;

        public ITransformData Transforms { get { return transformValues; } }
        private readonly TransformData transformValues;

        public ISelectionData Selections { get { return selectionValues; } }
        private readonly SelectionData selectionValues;

        public ISelectionPathData SelectionInstances { get { return instanceValues; } }
        private readonly SelectionPathData instanceValues;

        public IColumnData Columns { get { return columnValues; } }
        private readonly ColumnData columnValues;

        public ScriptingEngine() : base()
        {
            schemtaValues = new SchemaData() { Scripting = this };
            elementValues = new ElementData();
            transformValues = new TransformData() { Scripting = this };
            columnValues = new ColumnData();
            selectionValues = new SelectionData() { Scripting = this };
            instanceValues = new SelectionPathData();
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Load(factory));
            work.AddRange(elementValues.Load(factory));
            work.AddRange(transformValues.Load(factory));

            //work.AddRange(selectionValues.Load(factory));
            //work.AddRange(instanceValues.Load(factory));

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

            //work.AddRange(selectionValues.Save(factory));
            //work.AddRange(instanceValues.Save(factory));

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

            //result.Add(selectionValues.ToDataTable());
            //result.Add(instanceValues.ToDataTable());
            return result;
        }

        /// <inheritdoc/>
        /// <remarks>Scripting</remarks>
        public void Import(DataSet source)
        {
            if (source.Tables.Contains(schemtaValues.BindingName)
                && source.Tables[schemtaValues.BindingName] is DataTable schemeTable)
            { schemtaValues.Load(schemeTable.CreateDataReader()); }

            if (source.Tables.Contains(elementValues.BindingName)
                && source.Tables[elementValues.BindingName] is DataTable elementTable)
            { elementValues.Load(elementTable.CreateDataReader()); }

            if (source.Tables.Contains(transformValues.BindingName)
                && source.Tables[transformValues.BindingName] is DataTable transformTable)
            { transformValues.Load(transformTable.CreateDataReader()); }
        }


        /// <inheritdoc/>
        /// <remarks>Scripting (Currently load all)</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Load(factory));
            work.AddRange(elementValues.Load(factory));
            work.AddRange(transformValues.Load(factory));

            work.AddRange(selectionValues.Load(factory));
            work.AddRange(instanceValues.Load(factory));

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

            work.AddRange(selectionValues.Save(factory));
            work.AddRange(instanceValues.Save(factory));

            return work;
        }

        public IReadOnlyList<WorkItem> Remove()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Remove Scripting Schemta", DoWork = () => { schemtaValues.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Scripting Elements", DoWork = () => { elementValues.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Scripting Transforms", DoWork = () => { transformValues.Clear(); } });

            work.Add(new WorkItem() { WorkName = "Remove Scripting Selection", DoWork = () => { selectionValues.Clear(); } });
            work.Add(new WorkItem() { WorkName = "Remove Scripting Instance", DoWork = () => { instanceValues.Clear(); } });

            return work;
        }

        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(schemtaValues.Build(target));
            work.AddRange(transformValues.Build(target));
            work.AddRange(selectionValues.Build(target));

            return work;
        }
    }
}
