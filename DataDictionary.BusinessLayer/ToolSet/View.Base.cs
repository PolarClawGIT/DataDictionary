// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ToolSet
{
    // TODO: POC, trying to create a generic class to implement IView

    public abstract class View<TValue> : IView<TValue>
        where TValue : class, IBindingPropertyChanged, IBindingRowState, new()
    {
        /// <summary>
        /// Internal interface of the View.
        /// </summary>
        protected interface IViewData :
            IBindingData<TValue>, ILoadData, ISaveData, ILoadHistoryData
        { }

        /// <summary>
        /// The base Data that the class works with.
        /// </summary>
        protected abstract IViewData ViewData { get; set; }

        /// <summary>
        /// The current value setup for DataBinding.
        /// </summary>
        public BindingView<TValue> Values { get; protected set; } = new BindingView<TValue>(new BindingList<TValue>());

        /// <summary>
        /// Resets the BindingView(s) and enabled change events.
        /// </summary>
        protected virtual void StartChangedEvents()
        {
            SetBindingBase();

            Values.RaiseListChangedEvents = true;
            Values.ResetList();

            Values.ListChanged += Values_ListChanged;
        }

        /// <summary>
        /// Called by StartChangedEvents to reset the BindingViews.
        /// </summary>
        protected virtual void SetBindingBase()
        { Values = new BindingView<TValue>(ViewData); }

        /// <summary>
        /// Event fires when the Values BindingView list changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Values_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list.
            if (e.ListChangedType is ListChangedType.ItemDeleted
                && sender is IBindingList values
                && values.Count is 0)
            { StopChangedEvents(); }
        }

        /// <summary>
        /// Disables change events.
        /// </summary>
        protected virtual void StopChangedEvents()
        {
            Values.RaiseListChangedEvents = false;

            Values.ListChanged -= Values_ListChanged;
        }

        /// <summary>
        /// Loads the view by creating a reference to the specified data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load<TSource>(TSource data)
            where TSource : IBindingData<TValue>, ILoadData, ISaveData, ILoadHistoryData
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(LoadBase(data));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <summary>
        /// Called by Load(TData data) and returns the work to do the load.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>Override this method to add additional BindingViews</remarks>
        protected virtual IReadOnlyList<WorkItem> LoadBase<TSource>(TSource data)
            where TSource : IBindingData<TValue>, ILoadData, ISaveData, ILoadHistoryData
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = () => { ViewData = (IViewData)data; } });
            return work;
        }

        /// <summary>
        /// Loads the current value from the Database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(LoadBase(factory));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <summary>
        /// Called by Load(IDatabaseWork factory) and returns the work to do the load.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        protected virtual IReadOnlyList<WorkItem> LoadBase(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(ViewData.Load(factory));
            return work;
        }

        /// <summary>
        /// Saves the current value to the database.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return ViewData.Save(factory); }


        /// <summary>
        /// Removes the current value (does not delete it from the Database directly).
        /// </summary>
        /// <remarks>
        /// Calling Save after Remove causes the database to delete the items.
        /// </remarks>
        public virtual void Remove()
        {
            foreach (TValue item in Values.ToList())
            { Values.Remove(item); }
        }
    }


    public abstract class View<TValue, TIndex> : View<TValue>
        where TValue : class, IBindingPropertyChanged, IBindingRowState, new()
        where TIndex : class, IKey, new()
    {
        protected interface IViewIndexData : IViewData,
            ILoadData<TIndex>, ISaveData<TIndex>, ILoadHistoryData<TIndex>
        { }

        /// <inheritdoc/>
        //sealed protected override IViewData ViewData
        //{
        //    get { return (IViewData)ViewIndexData; }
        //    set { ViewIndexData = (IViewIndexData)value; }
        //}

        protected override IViewData ViewData {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException(); 
        }

        /// <summary>
        /// Replacement for ViewData containing the current data of the View.
        /// </summary>
        protected abstract IViewIndexData ViewIndexData { get; set; }

        /// <summary>
        /// Index of the current Value
        /// </summary>
        public TIndex Index { get; protected set; } = new TIndex();

        /// <inheritdoc cref="ITemporal.CreatedOn"/>
        public TemporalIndex AsOfUtcDate { get; protected set; } = new TemporalIndex();

        /// <summary>
        /// The current value (not for Binding).
        /// </summary>
        public TValue Value { get { return Values.FirstOrDefault() ?? defaultValue; } }
        TValue defaultValue = new TValue();

        /// <inheritdoc/>
        protected override void SetBindingBase()
        { Values = new BindingView<TValue>(ViewData, w => Index.Equals(w)); }

        /// <summary>
        /// Loads the current value from the Database as of a specific date.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="asOfUtcDate"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex(asOfUtcDate);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(LoadBase(factory, AsOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <summary>
        /// Called by Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate) and returns the work to do the load.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="asOfUtcDate"></param>
        /// <returns></returns>
        /// <remarks>Override this method to add additional BindingViews</remarks>
        protected virtual IReadOnlyList<WorkItem> LoadBase(IDatabaseWork factory, TemporalIndex asOfUtcDate)
        { return ViewIndexData.Load(factory, Index, asOfUtcDate); }

        /// <inheritdoc/>
        public override IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return ViewIndexData.Save(factory, Index); }
    }

    public class Test : View<AppGeneral.HelpSubjectValue, AppGeneral.HelpSubjectIndex>
    {
        AppGeneral.IHelpSubjectData data { get; set; } = new AppGeneral.HelpSubjectData();


        protected override IViewIndexData ViewIndexData
        {
            get { return (IViewIndexData)data; }
            set { data = (AppGeneral.IHelpSubjectData)value; }
        }

    }
}
