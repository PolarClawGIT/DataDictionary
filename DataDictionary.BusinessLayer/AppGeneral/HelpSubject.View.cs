// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppGeneral
{

    /// <summary>
    /// Wrapper class that returns the BindingViews for the HelpSubjects
    /// </summary>
    public class HelpSubjectView 
    {
        /// <inheritdoc cref="ApplicationData.HelpSubjects"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<HelpSubjectValue> HelpSubjects { get; private set; }
            = new BindingView<HelpSubjectValue>(new BindingList<HelpSubjectValue>())
            { AllowEdit = false, AllowNew = false, AllowRemove = false };

        /// <summary>
        /// The current set of data being worked with.
        /// </summary>
        IHelpSubjectData currentData = new HelpSubjectData();
        IHelpSubjectData modelData = new HelpSubjectData();

        /// <summary>
        /// Create an instance of the HelpSubjectView
        /// </summary>
        /// <param name="helpSubjects"></param>
        public HelpSubjectView(IHelpSubjectData helpSubjects)
        {
            currentData = helpSubjects;
            modelData = helpSubjects;

            CreateViews(new HelpSubjectIndex());
            StartChangedEvents();
        }

        /// <summary>
        /// Event is raised when the Attribute list becomes empty.
        /// </summary>
        /// <remarks>
        /// This addresses invalid operation exception fired by CurrencyManager.FindGoodRow.
        /// The exception occurs on empty list and is triggered by the ListChanged Event.
        /// When this event occurs, all BindingSources need to set RaiseListChangedEvents to false.
        /// A related error can occur with DataGridViews when the BindingList has an empty list.
        /// The code in this class handles RaiseListChangedEvents on the BindingLists.
        /// </remarks>
        public event EventHandler? ListEmpty;

        void CreateViews(IHelpSubjectIndex helpSubject)
        {
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);
            HelpSubjects = new BindingView<HelpSubjectValue>(currentData, w => key.Equals(w));

            HelpSubjects.ListChanged += OnListChanged;

            void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (ListEmpty is EventHandler handler
                    && e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                { handler(sender, new EventArgs()); }
            }
        }

        void StartChangedEvents()
        {
            HelpSubjects.RaiseListChangedEvents = true;
            HelpSubjects.ResetList();
        }

        void StopChangedEvents()
        { HelpSubjects.RaiseListChangedEvents = false; }

        /// <summary>
        /// Returns the Temporal Data object for the HelpSubjects
        /// </summary>
        /// <returns></returns>
        public ITemporalView GetTemporal()
        {
            return new TemporalData<HelpSubjectData, HelpSubjectValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data) };
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load()
        {
            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            { return Load(helpSubject); }
            else { throw new InvalidOperationException("No HelpSubject found"); }
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IHelpSubjectIndex helpSubject)
        {
            List<WorkItem> work = new List<WorkItem>();
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = modelData });
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            { return Load(factory, helpSubject); }
            else { throw new InvalidOperationException("No Attribute found"); }
        }

        /// <inheritdoc cref="ILoadData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex helpSubject)
        {
            List<WorkItem> work = new List<WorkItem>();
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = new HelpSubjectData() });
            work.AddRange(currentData.Load(factory, key));
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ILoadHistoryData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemporalIndex asOfUtcDate)
        {
            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            { return Load(factory, helpSubject, asOfUtcDate); }
            else { throw new InvalidOperationException("No HelpSubject found"); }
        }

        /// <inheritdoc cref="ILoadHistoryData{TKey}"/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IHelpSubjectIndex helpSubject, ITemporalIndex asOfUtcDate)
        {
            List<WorkItem> work = new List<WorkItem>();
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = new HelpSubjectData() });
            work.AddRange(currentData.Load(factory, key, asOfUtcDate));
            work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc cref="ISaveData{TKey}"/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        {
            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            { return currentData.Save(factory, new HelpSubjectIndex(helpSubject)); }
            else { throw new InvalidOperationException("No HelpSubject found"); }
        }

        /// <inheritdoc cref="IDeleteData"/>
        public IReadOnlyList<WorkItem> Delete(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            {
                HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

                work.Add(new WorkItem() { DoWork = StopChangedEvents });
                work.AddRange(currentData.Delete(key));
                work.AddRange(currentData.Save(factory, key));

                work.Add(new WorkItem() { DoWork = () => CreateViews(key) });
                work.Add(new WorkItem() { DoWork = StartChangedEvents });
            }
            else { throw new InvalidOperationException("No HelpSubject found"); }

            return work;
        }

        /// <inheritdoc cref="IRemoveItem{TKey}"/>
        public void Remove()
        {
            if (HelpSubjects.FirstOrDefault() is IHelpSubjectIndex helpSubject)
            {
                HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

                foreach (HelpSubjectValue item in HelpSubjects.Where(w => key.Equals(w)).ToList())
                { HelpSubjects.Remove(item); }
            }
        }
    }
}
