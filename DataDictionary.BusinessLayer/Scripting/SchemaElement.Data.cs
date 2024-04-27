using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Schema;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Element
    /// </summary>
<<<<<<<< HEAD:DataDictionary.BusinessLayer/Scripting/Element.Data.cs
    public interface IElementData<TValue> : IBindingData<TValue>,
        ILoadData, ILoadData<ISchemaKey>,
        ISaveData, ISaveData<ISchemaKey>
        where TValue : ElementValue, IElementValue
    { }

    class ElementData<TValue> : ElementCollection<TValue>,
        IElementData<TValue>, IGetNamedScopes
        where TValue : ElementValue, IElementValue, new()
========
    public interface ISchemaElementData : IBindingData<SchemaElementValue>
    { }

    class SchemaElementData : ElementCollection<SchemaElementValue>, ISchemaElementData,
        ILoadData, ILoadData<ISchemaKey>,
        ISaveData, ISaveData<ISchemaKey>
>>>>>>>> RenameIndexValue:DataDictionary.BusinessLayer/Scripting/SchemaElement.Data.cs
    {
        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISchemaKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Element</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
    }
}
