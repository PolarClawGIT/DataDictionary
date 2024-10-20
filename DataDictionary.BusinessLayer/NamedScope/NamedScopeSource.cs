﻿using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for Data objects capable of generating NamedScope data.
    /// </summary>
    /// <remarks>
    /// This interface and the inherited interfaces are used with the internal data classes.
    /// They are not intended to be exposed to the UI layer.
    /// </remarks>
    interface INamedScopeSourceData
    {
        // These interfaces addresses language limitations on how interfaces & abstract classes are inherited and enforced.
        // These methods are hidden by the Data objects.
        // The Data objects do not expose the class, just an interface for the class.
        // In effect, this is used to implement multi-inheritance manually.
        // CS0737: Interface items must be public
        // CS0060: Base class must be at least as accessible then inherited classes.

        /// <summary>
        /// Creates WorkItems that invoke a method to add items to NamedScopes.
        /// </summary>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        [Obsolete("Think this is the old and should not be used")]
        IReadOnlyList<WorkItem> LoadNamedScope(Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope);

        //TODO: Update all object to use this method.
        //TODO: Remove old version

        /// <summary>
        /// Creates WorkItems that invoke a method to add items to NamedScopes, generic version.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="data">A list of TValue</param>
        /// <param name="addNamedScope">Action that loads the Named Scope item. This is: NamedScopeData.Add.</param>
        /// <param name="getParent">Function that returns the Parent for the given TValue. Example: </param>
        /// <returns></returns>
        /// <remarks>
        /// This covers the most common use case.
        /// Each TValue in TData has a simple parent/child relationship.
        /// </remarks>
        static IReadOnlyList<WorkItem> LoadNamedScope<TData, TValue>(
            TData data,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope,
            Func<TValue, INamedScopeSourceValue?>? getParent = null)
            where TValue : INamedScopeSourceValue
            where TData : IList<TValue>, INamedScopeSourceData
        {
            List<WorkItem> work = new List<WorkItem>();
            Action<Int32, Int32> progressChanged = (completed, total) => { }; // Progress function.

            work.Add(new WorkItem(ref progressChanged)
            {
                WorkName = String.Format("Adding NamedScopes ({0})", typeof(TData).Name),
                DoWork = () =>
                {
                    Int32 completed = 0;
                    Int32 total = data.Count();
                    foreach (TValue item in data)
                    {
                        INamedScopeSourceValue? parent = null;
                        if (getParent is not null) { parent = getParent(item); }

                        NamedScopeValue newItem = new NamedScopeValue(item);
                        addNamedScope(parent, newItem);

                        progressChanged(completed++, total);
                    }
                }
            });

            return work;
        }
    }

    /// <summary>
    /// Interface for the NamedScope Source Value.
    /// </summary>
    public interface INamedScopeSourceValue : IPathValue, IBindingPropertyChanged
    { }

    /// <summary>
    /// Represents NameSpace items that does not have a specific Scope for the node.
    /// </summary>
    class NameSpaceSource : IPathValue, INamedScopeSourceValue
    {
        protected Guid SystemId;
        protected PathIndex SystemPath;

        public ScopeType Scope { get; } = ScopeType.ModelNameSpace;

        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public NameSpaceSource(PathIndex path)
        {
            SystemId = Guid.NewGuid();
            SystemPath = path;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new DataIndex() { SystemId = SystemId },
                GetPath = () => SystemPath,
                GetScope = () => Scope,
                GetTitle = () => SystemPath.MemberFullPath,
                IsPathChanged = (e) => false,
                IsTitleChanged = (e) => false
            };
        }

        public override String ToString()
        { return SystemPath.MemberFullPath; }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(String propertyName)
        { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, propertyName); }
    }
}
