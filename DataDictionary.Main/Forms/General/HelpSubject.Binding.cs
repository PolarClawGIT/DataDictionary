// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject
    {
        class FormBinding
        {
            BindingSource bindingHelpSubject;

            public BindingView<HelpSubjectValue> HelpSubjects { get; private set; }
            public BindingList<ControlValue> HelpControls { get; } = new BindingList<ControlValue>();

            IHelpSubjectData subjectData = BusinessData.ApplicationData.HelpSubjects;
            HelpSubjectIndex subjectIndex = new HelpSubjectIndex();
            TemporalIndex? temporalIndex = null;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding(ref BindingSource helpBinding) : base()
            {
                bindingHelpSubject = helpBinding;
                HelpSubjects = new BindingView<HelpSubjectValue>(subjectData, w => subjectIndex.Equals(w));
                bindingHelpSubject.DataSource = HelpSubjects;
            }

            public void SetIndex(IHelpSubjectIndex helpSubject)
            {
                subjectIndex = new HelpSubjectIndex(helpSubject);
                HelpSubjects.ListChanged -= OnListChanged;

                HelpSubjects.RaiseListChangedEvents = false;
                bindingHelpSubject.RaiseListChangedEvents = false;

                HelpSubjects = new BindingView<HelpSubjectValue>(subjectData, w => subjectIndex.Equals(w));
                bindingHelpSubject.DataSource = HelpSubjects;
                bindingHelpSubject.Position = 0;
                HelpSubjects.ListChanged += OnListChanged;

                HelpSubjects.RaiseListChangedEvents = true;
                bindingHelpSubject.RaiseListChangedEvents = true;
                HelpSubjects.ResetList();
            }

            public void SetIndex(IHelpSubjectIndex helpSubject, ITemporalIndex temporal)
            {
                SetIndex(helpSubject);
                temporalIndex = new TemporalIndex(temporal);
            }

            public HelpSubjectValue NewSubject()
            {
                HelpSubjectValue newValue = new HelpSubjectValue();
                subjectData.Add(newValue);
                SetIndex(newValue);

                return newValue;
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                StopBinding();
                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    work.AddRange(subjectData.Delete(subjectIndex));
                    work.AddRange(subjectData.Load(factory, subjectIndex));
                }
                else
                {
                    subjectData = IHelpSubjectData.Create();
                    work.AddRange(subjectData.Load(factory, subjectIndex, temporalIndex));
                }

                DoWork(work, StartBinding);

                void StopBinding()
                { bindingHelpSubject.SuspendBinding(); }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    subjectData.ResetBindings();

                    HelpSubjects = new BindingView<HelpSubjectValue>(subjectData, w => subjectIndex.Equals(w));
                    bindingHelpSubject.DataSource = HelpSubjects;
                    bindingHelpSubject.Position = 0;
                    bindingHelpSubject.ResumeBinding();

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                StopBinding();
                work.Add(factory.OpenConnection());
                work.AddRange(subjectData.Save(factory, subjectIndex));

                DoWork(work, StartBinding);

                void StopBinding()
                {
                    bindingHelpSubject.SuspendBinding();
                    HelpSubjects.ListChanged -= OnListChanged;
                    HelpSubjects.RaiseListChangedEvents = false;
                    bindingHelpSubject.RaiseListChangedEvents = false;

                    temporalIndex = null;
                    subjectData = BusinessData.ApplicationData.HelpSubjects;
                }

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    bindingHelpSubject.Position = 0;
                    HelpSubjects.ListChanged += OnListChanged;

                    HelpSubjects.RaiseListChangedEvents = true;
                    bindingHelpSubject.RaiseListChangedEvents = true;
                    bindingHelpSubject.ResumeBinding();
                    bindingHelpSubject.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            private void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                {
                    // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow.
                    // The exception occurs on empty list and is triggered by the ListChanged Event.
                    // When this event occurs, all BindingSources need to set RaiseListChangedEvents to false.
                    // A related error can occur with DataGridViews when the BindingList has an empty list.

                    //HelpSubjects.RaiseListChangedEvents = false;
                    bindingHelpSubject.RaiseListChangedEvents = false;
                }
            }

            /// <summary>
            /// Try/Get the current Help Subject.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetSubject([NotNullWhen(true)] out HelpSubjectValue? result)
            {
                if (bindingHelpSubject.Position >= 0
                    && bindingHelpSubject.Current is HelpSubjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void RemoveSubject()
            {
                if (TryGetSubject(out HelpSubjectValue? value))
                {
                    bindingHelpSubject.SuspendBinding();
                    HelpSubjects.ListChanged -= OnListChanged;
                    HelpSubjects.RaiseListChangedEvents = false;
                    bindingHelpSubject.RaiseListChangedEvents = false;

                    HelpSubjects.Remove(value);
                }
            }

            public Boolean GetAuthorization(CommandImageType command)
            {
                Boolean isGrant = false;

                if (TryGetSubject(out HelpSubjectValue? helpSubject))
                {
                    SecurableIndex securable = new HelpSubjectIndex(helpSubject);
                    isGrant = BusinessData.Authorization.IsGrant(securable);
                }

                switch (command)
                {
                    case CommandImageType.Default: return true;
                    case CommandImageType.Add: return BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandImageType.Delete: return BusinessData.Authorization.IsHelpAdmin;
                    case CommandImageType.OpenDatabase: return isGrant || BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandImageType.SaveDatabase: return isGrant || BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandImageType.DeleteDatabase: return BusinessData.Authorization.IsHelpAdmin;
                    case CommandImageType.SecurityDatabase: return BusinessData.Authorization.IsSecurityAdmin;
                    default: return false;
                }
            }
        }
    }
}
