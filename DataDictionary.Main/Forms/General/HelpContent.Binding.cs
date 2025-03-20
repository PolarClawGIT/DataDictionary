using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

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
            public Form? SubjectForm { get; set; } = null;

            public BindingSubject(Form form)
            {
                Path = form.ToHelpSubjectPath();
                Title = String.Format("(new Subject: {0})", Path.Member);
                SubjectForm = form;
            }

            public BindingSubject(HelpSubjectValue helpSubject)
            {
                Path = new HelpSubjectIndexPath(helpSubject);
                Title = helpSubject.HelpSubject ?? Path.Member;
                Description = helpSubject.HelpText ?? String.Empty;
                ToolTip = helpSubject.HelpToolTip ?? String.Empty;
                SubjectIndex = new HelpSubjectIndex(helpSubject);

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

        /// <summary>
        ///  Helper class that helps manage the binding class and associated data.
        /// </summary>
        class FormBinding
        {
            BindingSource bindingHelpSubject;

            public IEnumerable<BindingSubject> HelpSubjects { get { return subjects; } }
            BindingList<BindingSubject> subjects = new BindingList<BindingSubject>();
            IHelpSubjectData subjectData = BusinessData.ApplicationData.HelpSubjects;

            public FormBinding(ref BindingSource helpBinding)
            {
                bindingHelpSubject = helpBinding;
                subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));
                bindingHelpSubject.DataSource = subjects;

                subjectData.ListChanged += SubjectData_ListChanged;

                void SubjectData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    TryGetSubject(out BindingSubject? current);

                    if (e.ListChangedType is ListChangedType.Reset
                        or ListChangedType.ItemAdded
                        or ListChangedType.ItemDeleted
                        or ListChangedType.ItemChanged
                        && SubjectsChanged is EventHandler handler)
                    {
                        subjects.Clear();
                        subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));

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
                HelpSubjectIndexPath helpSubject = form.ToHelpSubjectPath();

                if (!subjects.Any(w => helpSubject.Equals(w.Path)))
                { subjects.Add(new BindingSubject(form)); }

                foreach (BindingSubject item in subjects.Where(w => w.Path.ChildOf(helpSubject)))
                { item.SubjectForm = form; }
            }

            public void SetPosition(IHelpSubjectIndex helpSubject)
            {
                HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);

                if (subjects.FirstOrDefault(w => key.Equals(w.SubjectIndex)) is BindingSubject value)
                {
                    var x = subjects.IndexOf(value);
                    bindingHelpSubject.Position = subjects.IndexOf(value); }
            }

            public void SetPosition(HelpSubjectIndexPath helpSubject)
            {
                if (subjects.FirstOrDefault(w => helpSubject.Equals(w.Path)) is BindingSubject value)
                { bindingHelpSubject.Position = subjects.IndexOf(value); }
            }

            public void SetPosition(String helpSubject)
            { SetPosition(new HelpSubjectIndexPath(helpSubject)); }

            public void SetPosition(Form helpSubject)
            { SetPosition(helpSubject.ToHelpSubjectPath()); }

            public void SetPosition(BindingSubject subject)
            {
                if (subject.SubjectIndex is HelpSubjectIndex index)
                { SetPosition(index); }
                else if (subject.SubjectForm is Form form)
                { SetPosition(form); }
            }

            public HelpSubjectValue NewSubject()
            {
                HelpSubjectValue result = new HelpSubjectValue();
                subjectData.Add(result);
                SetPosition(result);

                return result;
            }

            public HelpSubjectValue NewSubject(BindingSubject source)
            {
                if (source.SubjectIndex is not null && subjectData.FirstOrDefault(w => source.SubjectIndex.Equals(w)) is HelpSubjectValue value)
                { return value; } // Subject already exists, return it.

                HelpSubjectValue result = new HelpSubjectValue();

                if (source.SubjectForm is Form)
                {
                    result.HelpSubject = String.Format("(new Help Subject: {0})", source.Path.Member);
                    result.Path = source.Path;
                    source.SubjectIndex = new HelpSubjectIndex(result);
                }

                subjectData.Add(result);
                SetPosition(result);

                return result;
            }

            public Boolean TryGetSubject([NotNullWhen(true)] out BindingSubject? result)
            {
                if (bindingHelpSubject.Position >= 0
                    && bindingHelpSubject.Current is BindingSubject value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetSubject([NotNullWhen(true)] out HelpSubjectValue? result)
            {
                if (TryGetSubject(out BindingSubject? subject)
                    && subjectData.FirstOrDefault(w =>
                        subject.SubjectIndex is not null
                        && subject.SubjectIndex.Equals(w))
                        is HelpSubjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public ITemporalData GetTemporal()
            { return subjectData.GetTemporal(); }
        }
    }
}
