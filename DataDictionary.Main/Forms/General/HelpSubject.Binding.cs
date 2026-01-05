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
            public required BindingSource BindingHelpSubject { private get; init; }
            public BindingView<HelpSubjectValue> HelpSubjects { get; private set; } =
                new BindingView<HelpSubjectValue>([]) 
                { AllowEdit = false, AllowNew = false, AllowRemove = false };
            public BindingList<ControlValue> HelpControls { get; } = new BindingList<ControlValue>();

            IHelpSubjectData subjectData = BusinessData.ApplicationData.HelpSubjects;
            HelpSubjectIndex subjectIndex = new HelpSubjectIndex();
            TemporalIndex? temporalIndex = null;

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding() : base()
            { }

            public void Init()
            {
                // Note: C# 13 adds "field".
                // This code could then be moved to the BindingHelpSubject init.

                subjectData = BusinessData.ApplicationData.HelpSubjects;
                HelpSubjects = new BindingView<HelpSubjectValue>(subjectData, w => subjectIndex.Equals(w));

                if (HelpSubjects.Count > 0)
                {
                    BindingHelpSubject.DataSource = HelpSubjects;
                    BindingHelpSubject.Position = 0;
                    HelpSubjects.ListChanged += OnListChanged;

                    HelpSubjects.RaiseListChangedEvents = true;
                    BindingHelpSubject.RaiseListChangedEvents = true;
                    HelpSubjects.ResetList();
                }
            }

            public void Load(IHelpSubjectIndex helpSubject)
            {
                subjectIndex = new HelpSubjectIndex(helpSubject);
                HelpSubjects.ListChanged -= OnListChanged;

                HelpSubjects.RaiseListChangedEvents = false;
                BindingHelpSubject.RaiseListChangedEvents = false;

                HelpSubjects = new BindingView<HelpSubjectValue>(subjectData, w => subjectIndex.Equals(w));

                if (HelpSubjects.Count > 0)
                {
                    BindingHelpSubject.DataSource = HelpSubjects;
                    HelpSubjects.ListChanged += OnListChanged;

                    HelpSubjects.RaiseListChangedEvents = true;
                    BindingHelpSubject.RaiseListChangedEvents = true;
                }

                HelpSubjects.ResetList();
                BindingHelpSubject.MoveFirst();  // For some reason this must be done last or it does not work.
            }

            public void Load(IHelpSubjectIndex helpSubject, ITemporalIndex temporal)
            {
                Load(helpSubject);
                temporalIndex = new TemporalIndex(temporal);
            }

            public HelpSubjectValue NewValue()
            {
                HelpSubjectValue newValue = new HelpSubjectValue();
                subjectData.Add(newValue);
                Load(newValue);

                return newValue;
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    subjectData = BusinessData.ApplicationData.HelpSubjects;
                    work.AddRange(subjectData.Delete(subjectIndex));
                    work.AddRange(subjectData.Load(factory, subjectIndex));
                }
                else
                {
                    subjectData = IHelpSubjectData.Create();
                    work.AddRange(subjectData.Load(factory, subjectIndex, temporalIndex));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    Load(subjectIndex);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(subjectData.Save(factory, subjectIndex));

                DoWork(work, onComplete);
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
                    BindingHelpSubject.RaiseListChangedEvents = false;
                }
            }

            /// <summary>
            /// Try/Get the current Help Subject.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public Boolean TryGetValue([NotNullWhen(true)] out HelpSubjectValue? result)
            {
                if (BindingHelpSubject.Position >= 0
                    && BindingHelpSubject.Current is HelpSubjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void RemoveValue()
            {
                if (TryGetValue(out HelpSubjectValue? value))
                {
                    HelpSubjects.Remove(value);
                    Load(value);
                }
            }

            public Boolean GetAuthorization(CommandType command)
            {
                Boolean isGrant = false;

                if (TryGetValue(out HelpSubjectValue? helpSubject))
                {
                    SecurableIndex securable = new HelpSubjectIndex(helpSubject);
                    isGrant = BusinessData.Authorization.IsGrant(securable);
                }

                switch (command)
                {
                    case CommandType.Default: return true;
                    case CommandType.Add: return BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandType.Delete: return BusinessData.Authorization.IsHelpAdmin;
                    case CommandType.OpenDatabase: return isGrant || BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandType.SaveDatabase: return isGrant || BusinessData.Authorization.IsHelpAdmin || BusinessData.Authorization.IsHelpOwner;
                    case CommandType.DeleteDatabase: return BusinessData.Authorization.IsHelpAdmin;
                    case CommandType.SecurityDatabase: return BusinessData.Authorization.IsSecurityAdmin;
                    default: return false;
                }
            }
        }
    }
}
