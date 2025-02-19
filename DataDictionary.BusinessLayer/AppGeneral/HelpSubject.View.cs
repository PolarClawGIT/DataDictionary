// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <summary>
    /// Wrapper class that returns the BindingViews for the HelpSubjects
    /// </summary>
    public class HelpSubjectView : IView<HelpSubjectIndex, HelpSubjectValue>
    {
        /// <inheritdoc/>
        public HelpSubjectIndex Index { get; protected set; } = new HelpSubjectIndex();

        /// <inheritdoc/>
        public TemporalIndex AsOfUtcDate { get; protected set; } = new TemporalIndex();

        /// <inheritdoc cref="ApplicationData.HelpSubjects"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<HelpSubjectValue> HelpSubjects { get; private set; } 
            = new BindingView<HelpSubjectValue>(new BindingList<HelpSubjectValue>());

        /// <inheritdoc/>
        HelpSubjectValue IView<HelpSubjectIndex, HelpSubjectValue>.Value 
        { get { return HelpSubjects.FirstOrDefault() ?? new HelpSubjectValue(); } }

        /// <inheritdoc/>
        BindingView<HelpSubjectValue> IView<HelpSubjectIndex, HelpSubjectValue>.Values 
        { get { return HelpSubjects; } }

        /// <summary>
        /// The current set of data being worked with.
        /// </summary>
        IHelpSubjectData currentData = new HelpSubjectData();

        /// <summary>
        /// Creates a blank instance of HelpSubjectView that is not bound to the Application Data.
        /// </summary>
        /// <param name="helpSubject"></param>
        public HelpSubjectView(IHelpSubjectIndex helpSubject) : base()
        {
            Index = new HelpSubjectIndex(helpSubject);
            StopBinding();
        }

        /// <summary>
        /// Creates a instance of HelpSubjectView that is bound to the Application data.
        /// </summary>
        /// <param name="helpSubject"></param>
        /// <param name="values"></param>
        public HelpSubjectView(IHelpSubjectIndex helpSubject, IHelpSubjectData values) : this(helpSubject)
        {
            currentData = values;
            StartBinding();
        }

        void StartBinding()
        {
            HelpSubjects = new BindingView<HelpSubjectValue>(currentData, w => Index.Equals(w));

            HelpSubjects.RaiseListChangedEvents = true;
            HelpSubjects.ResetList();

            HelpSubjects.ListChanged += HelpSubjects_ListChanged;
        }

        private void HelpSubjects_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list.
            if (e.ListChangedType is ListChangedType.ItemDeleted
                && sender is IBindingList values
                && values.Count is 0)
            { StopBinding(); }

            if (e.ListChangedType is ListChangedType.ItemAdded
                 && sender is IEnumerable<IHelpSubjectValue> list
                 && list.FirstOrDefault() is IHelpSubjectValue value
                 && Index.HelpId == Guid.Empty)
            { Index = new HelpSubjectIndex(value); }
        }

        void StopBinding()
        {
            HelpSubjects.RaiseListChangedEvents = false;

            HelpSubjects.ListChanged -= HelpSubjects_ListChanged;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, Index));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            AsOfUtcDate = new TemporalIndex(asOfUtcDate);

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Load(factory, Index, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return currentData.Save(factory, Index); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopBinding });
            work.AddRange(currentData.Delete(Index));
            work.AddRange(currentData.Save(factory, Index));
            work.Add(new WorkItem() { DoWork = StartBinding });

            return work;
        }

        /// <inheritdoc/>
        public void Remove()
        {
            foreach (HelpSubjectValue item in HelpSubjects.Where(w => Index.Equals(w)).ToList())
            { HelpSubjects.Remove(item); }
        }
    }
}
