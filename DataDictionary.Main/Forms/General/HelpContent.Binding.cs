using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpContent
    {
        class BindingSubject : INotifyPropertyChanged, IEquatable<BindingSubject>
        {
            public HelpSubjectIndexPath Path { get; }
            public String Title { get; }
            public String Description { get; } = String.Empty;
            public String ToolTip { get; } = String.Empty;
            public HelpSubjectIndex? SubjectIndex { get; set; } = null;

            public List<HelpControlValue> SubjectControls { get; } = new List<HelpControlValue>();
            public HelpControlValue? SubjectForm
            {
                get
                {
                    if (SubjectControls.FirstOrDefault(w => w.IsForm) is HelpControlValue value)
                    { return value; }
                    else { return null; }
                }
            }

            public BindingSubject(Form form)
            {
                Path = form.ToHelpSubjectPath();
                Title = String.Format("(new Subject: {0})", Path.Member);
                SubjectControls = HelpControlValue.Create(form).ToList();
            }

            public BindingSubject(HelpSubjectValue helpSubject)
            {
                Path = new HelpSubjectIndexPath(helpSubject);
                Title = helpSubject.HelpSubject ?? Path.Member;
                Description = helpSubject.HelpText ?? String.Empty;
                ToolTip = helpSubject.HelpToolTip ?? String.Empty;
                SubjectIndex = new HelpSubjectIndex(helpSubject);
                SubjectControls = new List<HelpControlValue>();

                helpSubject.PropertyChanged += PropertyChanged;
                //helpSubject.RowStateChanged += RowStateChanged;

                void PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    switch (e.PropertyName)
                    {
                        case nameof(helpSubject.HelpSubject):
                            OnPropertyChanged(nameof(Title)); break;
                        case nameof(helpSubject.NameSpace):
                            OnPropertyChanged(nameof(Path)); break;
                        case nameof(helpSubject.HelpText):
                            OnPropertyChanged(nameof(Path)); break;
                        case nameof(helpSubject.HelpToolTip):
                            OnPropertyChanged(nameof(ToolTip)); break;
                        default:
                            break;
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            void OnPropertyChanged(String property)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(property)); }
            }

            public Boolean Equals(BindingSubject? other)
            {
                if (other is null) { return false; }

                if (SubjectIndex is not null
                    && other.SubjectIndex is not null
                    && SubjectIndex.Equals(other.SubjectIndex))
                { return true; }

                return Path.Equals(other.Path);
            }
        }

        class FormBinding
        {
            public required BindingSource BindingHelpSubject { private get; init ; }

            public IEnumerable<BindingSubject> HelpSubjects { get { return subjects; } }
            BindingList<BindingSubject> subjects = new BindingList<BindingSubject>();
            IHelpSubjectData subjectData = BusinessData.ApplicationData.HelpSubjects;

            static Dictionary<HelpSubjectIndexPath, List<HelpControlValue>> subjectForms = new Dictionary<HelpSubjectIndexPath, List<HelpControlValue>>();

            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public FormBinding()
            { }

            public void Load()
            {
                subjectData = BusinessData.ApplicationData.HelpSubjects;
                subjects.Clear();
                subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));
                BindingHelpSubject.DataSource = subjects;

                // restore the subject forms already known.
                foreach (var item in subjectForms)
                { SetForm(item.Key, item.Value); }

                subjectData.ListChanged += SubjectData_ListChanged;

                void SubjectData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    TryGetValue(out BindingSubject? current);

                    if (e.ListChangedType is ListChangedType.Reset
                        or ListChangedType.ItemAdded
                        or ListChangedType.ItemDeleted
                        or ListChangedType.ItemChanged
                        && SubjectsChanged is EventHandler handler)
                    {
                        subjects.Clear();
                        subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));

                        // restore the subject forms already known.
                        foreach (var item in subjectForms)
                        { SetForm(item.Key, item.Value); }

                        if (e.NewIndex >= 0)
                        { SetPosition(subjectData[e.NewIndex]); }
                        else if (current is BindingSubject subject)
                        { SetPosition(subject); }
                        else { SetPosition(Settings.Default.DefaultSubject); }

                        handler(sender, new EventArgs());
                    }
                }
            }

            public event EventHandler? SubjectsChanged;

            public void AddForm(Form form)
            {
                HelpSubjectIndexPath key = form.ToHelpSubjectPath();

                if (subjectForms.ContainsKey(key))
                {
                    subjectForms[key].Clear();
                    subjectForms[key].AddRange(HelpControlValue.Create(form));
                }
                else { subjectForms.Add(key, HelpControlValue.Create(form).ToList()); }

                if (!subjects.Any(w => key.Equals(w.Path)))
                { subjects.Add(new BindingSubject(form)); }
            }

            public void SetForm(IHelpSubjectIndexPath helpPath, Form form)
            { SetForm(helpPath, HelpControlValue.Create(form)); }

            public void SetForm(IHelpSubjectIndex helpSubject, Form form)
            {   SetForm(helpSubject, HelpControlValue.Create(form)); }

            void SetForm(IHelpSubjectIndexPath helpPath, IEnumerable<HelpControlValue> controls)
            {
                HelpSubjectIndexPath key = new HelpSubjectIndexPath(helpPath);

                foreach (BindingSubject item in subjects.Where(w => key.Equals(w.Path) || w.Path.ChildOf(key)))
                {
                    item.SubjectControls.Clear();
                    item.SubjectControls.AddRange(controls);
                }
            }

            void SetForm(IHelpSubjectIndex helpSubject, IEnumerable<HelpControlValue> controls)
            {
                HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

                foreach (BindingSubject item in subjects.Where(w => key.Equals(w.Path)))
                {
                    item.SubjectControls.Clear();
                    item.SubjectControls.AddRange(controls);
                }
            }

            public void SetPosition(IHelpSubjectIndex helpSubject)
            {
                HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

                if (subjects.FirstOrDefault(w => key.Equals(w.SubjectIndex)) is BindingSubject value)
                { BindingHelpSubject.Position = subjects.IndexOf(value); }
            }

            public void SetPosition(HelpSubjectIndexPath helpSubject)
            {
                if (subjects.FirstOrDefault(w => helpSubject.Equals(w.Path)) is BindingSubject value)
                { BindingHelpSubject.Position = subjects.IndexOf(value); }
            }

            public void SetPosition(String helpSubject)
            { SetPosition(new HelpSubjectIndexPath(helpSubject)); }

            public void SetPosition(BindingSubject subject)
            {
                if (subject.SubjectIndex is HelpSubjectIndex index)
                { SetPosition(index); }
                else if (subject.SubjectForm is HelpControlValue form)
                { SetPosition(form.Path); }
            }

            public HelpSubjectValue NewValue()
            {
                HelpSubjectValue result = new HelpSubjectValue();
                subjectData.Add(result);
                SetPosition(result);

                return result;
            }

            public HelpSubjectValue NewValue(BindingSubject source)
            {
                HelpSubjectValue result = new HelpSubjectValue();

                if (source.SubjectForm is HelpControlValue)
                {
                    result.HelpSubject = String.Format("(new Help Subject: {0})", source.Path.Member);

                    if (source.SubjectIndex is not null
                        && subjectData.FirstOrDefault(
                            w => source.SubjectIndex.Equals(w))
                            is HelpSubjectValue value)
                    { result.Path = new HelpSubjectIndexPath(value.Path.Merge(source.Path)); }
                    else { result.Path = source.Path; }

                    source.SubjectIndex = new HelpSubjectIndex(result);

                    subjectData.Add(result);
                    SetForm(result, source.SubjectControls);
                }
                else { subjectData.Add(result); }

                SetPosition(result);
                return result;
            }

            public Boolean TryGetValue([NotNullWhen(true)] out BindingSubject? result)
            {
                if (BindingHelpSubject.Position >= 0
                    && BindingHelpSubject.Current is BindingSubject value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out HelpSubjectValue? result)
            {
                if (TryGetValue(out BindingSubject? subject)
                    && subjectData.FirstOrDefault(w =>
                        subject.SubjectIndex is not null
                        && subject.SubjectIndex.Equals(w))
                        is HelpSubjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public ITemporalData GetTemporal()
            { return subjectData.GetTemporal(); }

            public Boolean GetAuthorization(CommandType command)
            {
                switch (command)
                {
                    case CommandType.Default: return true;
                    case CommandType.Browse: return true;
                    case CommandType.Select: return true;
                    case CommandType.Add:
                        return BusinessData.Authorization.IsHelpAdmin
                            || BusinessData.Authorization.IsHelpOwner;
                    case CommandType.Open: return true;
                    case CommandType.OpenDatabase: return true;
                    case CommandType.SaveDatabase:
                        return BusinessData.Authorization.IsHelpAdmin;
                    case CommandType.SecurityDatabase:
                        return BusinessData.Authorization.IsSecurityAdmin;
                    default:
                        return false;
                }
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(subjectData.Delete());
                work.AddRange(subjectData.Load(factory));

                DoWork(work, onCompleteing);

                void onCompleteing(RunWorkerCompletedEventArgs args)
                {
                    subjects.Clear();
                    subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));

                    // restore the subject forms already known.
                    foreach (var item in subjectForms)
                    { SetForm(item.Key, item.Value); }

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(subjectData.Save(factory));

                DoWork(work, onCompleteing);

                void onCompleteing(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
            }
        }
    }
}
