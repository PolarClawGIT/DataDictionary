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
        /// <summary>
        ///  Helper class that helps manage the binding class and associated data.
        /// </summary>
        class FormBinding
        {
            BindingSource bindingHelpSubject;
            HelpSubjectIndexPath intendedSubject = new HelpSubjectIndexPath();
            public BindingView<HelpSubjectValue> HelpSubjects { get; private set; } =
                // Blank List
                new BindingView<HelpSubjectValue>(new BindingList<HelpSubjectValue>())
                { AllowNew = false, AllowEdit = false, AllowRemove = false };

            public Form? CurrentForm
            {
                get { return subjectForm; }
                set
                {
                    if (subjectForm is null && value is Form newForm)
                    {
                        subjectForm = newForm;
                        intendedSubject = newForm.ToNameSpaceKey();
                    }
                    else
                    { throw new InvalidOperationException("Once set, the target FormHelpSubject cannot change"); }
                }
            }
            Form? subjectForm;

            public FormBinding(ref BindingSource helpBinding)
            { bindingHelpSubject = helpBinding; }

            public void Bind(IHelpSubjectData source)
            {
                bindingHelpSubject.ListChanged -= ListChanged;
                bindingHelpSubject.AddingNew -= AddingNew;

                HelpSubjects = new BindingView<HelpSubjectValue>(source);

                bindingHelpSubject.DataSource = HelpSubjects;

                bindingHelpSubject.ListChanged += ListChanged;
                bindingHelpSubject.AddingNew += AddingNew;
                bindingHelpSubject.DataSourceChanged += DataSourceChanged;

                void ListChanged(Object? sender, ListChangedEventArgs e)
                {   // Deals with Invalid operation exception fired by CurrencyManager.FindGoodRow.
                    if (e.ListChangedType is ListChangedType.ItemDeleted
                        && sender is IBindingList values
                        && values.Count is 0)
                    {
                        bindingHelpSubject.RaiseListChangedEvents = false;
                        HelpSubjects.ListChanged -= ListChanged;
                        HelpSubjects.AddingNew -= AddingNew;
                    }
                }

                void AddingNew(Object? sender, AddingNewEventArgs e)
                { e.NewObject = new HelpSubjectValue(); }

                void DataSourceChanged(Object? sender, EventArgs e)
                { throw new InvalidOperationException("Do not change the DataSource once set"); }
            }

            public Boolean FindSubject(String targetSubject, out HelpSubjectValue? value)
            { return FindSubject(new HelpSubjectIndexPath(targetSubject), out value); }

            public Boolean FindSubject(Form targetForm, [NotNullWhen(true)] out HelpSubjectValue? value)
            { return FindSubject(targetForm.ToNameSpaceKey(), out value); }

            public Boolean FindSubject(HelpSubjectIndexPath targetSubject, [NotNullWhen(true)] out HelpSubjectValue? value)
            {
                if (HelpSubjects.FirstOrDefault(w => targetSubject.Equals(new HelpSubjectIndexPath(w)))
                    is HelpSubjectValue target)
                { value = target; return true; }
                else { value = null; return false; }
            }

            public void SetSubject(String targetSubject)
            { SetSubject(new HelpSubjectIndexPath(targetSubject)); }

            public void SetSubject(Form targetForm)
            { SetSubject(targetForm.ToNameSpaceKey()); }

            public void SetSubject(HelpSubjectValue helpSubject)
            { SetSubject(new HelpSubjectIndexPath(helpSubject)); }

            public void SetSubject(HelpSubjectIndexPath targetSubject)
            { intendedSubject = targetSubject; }

            public void SetForm(Form targetForm)
            { subjectForm = targetForm; }

            public Boolean SetPosition()
            {
                if (FindSubject(intendedSubject, out HelpSubjectValue? value))
                { return SetPosition(value); }
                else { return false; }

            }

            public Boolean SetPosition(HelpSubjectValue helpSubject)
            {
                if (FindSubject(new HelpSubjectIndexPath(helpSubject), out HelpSubjectValue? value))
                { bindingHelpSubject.Position = HelpSubjects.IndexOf(value);  return true; }
                else { return false; }
            }


            public Boolean TryGetCurrent([NotNullWhen(true)] out HelpSubjectValue? value)
            {
                if (bindingHelpSubject.Position >= 0 
                    && bindingHelpSubject.Current is HelpSubjectValue result)
                { value = result; return true; }
                else { value = null; return false; }
            }
        }
    }
}
