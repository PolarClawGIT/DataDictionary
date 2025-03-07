using DataDictionary.BusinessLayer.AppGeneral;
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
        class BindingSubject : INotifyPropertyChanged
        {
            public HelpSubjectIndexPath Path { get; }
            public String Title { get; }
            public String Description { get; } = String.Empty;
            public String ToolTip { get; } = String.Empty;
            public HelpSubjectIndex? SubjectIndex { get; } = null;
            public Form? SubjectForm { get; set; } = null;

            public BindingSubject(Form form)
            {
                Path = form.ToNameSpaceKey();
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
        }

        /// <summary>
        ///  Helper class that helps manage the binding class and associated data.
        /// </summary>
        class FormBinding
        {
            BindingSource bindingHelpSubject;
            HelpSubjectIndexPath initialSubject = new HelpSubjectIndexPath(Settings.Default.DefaultSubject);

            public IEnumerable<BindingSubject> HelpSubjects { get { return subjects; } }
            BindingList<BindingSubject> subjects = new BindingList<BindingSubject>();
            IHelpSubjectData subjectData = BusinessData.ApplicationData.HelpSubjects;

            public FormBinding(ref BindingSource helpBinding)
            {
                bindingHelpSubject = helpBinding;
                subjects.AddRange(subjectData.Select(s => new BindingSubject(s)));
                bindingHelpSubject.DataSource = subjects;
            }

            public void AddForm(Form form)
            {
                HelpSubjectIndexPath helpSubject = form.ToNameSpaceKey();

                if (!subjects.Any(w => helpSubject.Equals(w.Path)))
                { subjects.Add(new BindingSubject(form)); }

                var x = subjects.Where(w => w.Path.ChildOf(helpSubject));

                foreach (BindingSubject item in subjects.Where(w => w.Path.ChildOf(helpSubject)))
                { item.SubjectForm = form; }
            }

            public void SetPosition(HelpSubjectIndexPath helpSubject)
            {
                initialSubject = helpSubject;

                if (subjects.FirstOrDefault(w => helpSubject.Equals(w.Path)) is BindingSubject value)
                { bindingHelpSubject.Position = subjects.IndexOf(value); }
            }

            public void SetPosition(String helpSubject)
            { SetPosition(new HelpSubjectIndexPath(helpSubject)); }

            public void SetPosition(Form helpSubject)
            { SetPosition(helpSubject.ToNameSpaceKey()); }

            public Boolean TryCurrent([NotNullWhen(true)] out BindingSubject? result)
            {
                if (bindingHelpSubject.Position >= 0
                    && bindingHelpSubject.Current is BindingSubject value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryCurrent([NotNullWhen(true)] out HelpSubjectValue? result)
            {
                if (TryCurrent(out BindingSubject? subject)
                    && subjectData.FirstOrDefault(w =>
                        subject.SubjectIndex is not null
                        && subject.SubjectIndex.Equals(w))
                        is HelpSubjectValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }
        }
    }
}
