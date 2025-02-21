using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
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
    /// Wrapper class that returns the BindingViews for the Help Subject Content
    /// </summary>
    public class HelpContentView : IView<HelpSubjectValue>
    {
        /// <summary>
        /// The current set of data being worked with.
        /// </summary>
        IHelpSubjectData currentData = new HelpSubjectData();

        /// <inheritdoc cref="ApplicationData.HelpSubjects"/>
        /// <remarks>One or Zero values</remarks>
        public BindingView<HelpSubjectValue> HelpSubjects { get; private set; }
            = new BindingView<HelpSubjectValue>(new BindingList<HelpSubjectValue>());

        /// <inheritdoc/>
        BindingView<HelpSubjectValue> IView<HelpSubjectValue>.Values
        { get { return HelpSubjects; } }

        /// <summary>
        /// Creates a instance of HelpContentView
        /// </summary>
        public HelpContentView() : base()
        { }

        void StartChangedEvents()
        {
            HelpSubjects = new BindingView<HelpSubjectValue>(currentData);

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
            { StopChangedEvents(); }
        }

        void StopChangedEvents()
        {
            HelpSubjects.RaiseListChangedEvents = false;

            HelpSubjects.ListChanged -= HelpSubjects_ListChanged;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IHelpSubjectData values)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.Add(new WorkItem() { DoWork = () => currentData = values });
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork = StopChangedEvents });
            work.AddRange(currentData.Load(factory));
            work.Add(new WorkItem() { DoWork = StartChangedEvents });

            return work;
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return currentData.Save(factory); }

        /// <inheritdoc/>
        public ITemporalView GetTemporal()
        {
            return new TemporalData<HelpSubjectData, HelpSubjectValue>()
            { CreateLoad = (factory, data) => factory.CreateHistory(data) };
        }

        /// <inheritdoc/>
        void IView<HelpSubjectValue>.Remove()
        { throw new NotImplementedException(); }
    }
}
