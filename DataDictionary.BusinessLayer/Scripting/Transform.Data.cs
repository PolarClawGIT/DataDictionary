﻿using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Transform;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Transform
    /// </summary>
<<<<<<< HEAD
    public interface ITransformData<TValue> : IBindingData<TValue>,
=======
    public interface ITransformData :
        IBindingData<TransformValue>,
>>>>>>> RenameIndexValue
        ILoadData, ILoadData<ITransformKey>,
        ISaveData, ISaveData<ITransformKey>
        where TValue : TransformValue, ITransformValue
    { }

<<<<<<< HEAD
    class TransformData<TValue> : TransformCollection<TValue>,
        ITransformData<TValue>, IGetNamedScopes
        where TValue : TransformValue, ITransformValue, new()
=======
    class TransformData : TransformCollection<TransformValue>, ITransformData, IGetNamedScopes
>>>>>>> RenameIndexValue
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITransformKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITransformKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        { return this.Select(s => new NamedScopePair(s)); }
<<<<<<< HEAD
=======

>>>>>>> RenameIndexValue
    }
}
