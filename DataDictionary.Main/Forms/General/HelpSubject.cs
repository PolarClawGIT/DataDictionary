using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject : ApplicationData, IApplicationDataForm
    {
        FormBinding formData;
        Boolean needsData = false;

        public Boolean IsOpenItem(IHelpSubjectIndex helpSubject)
        {
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubject);
            return formData.TryGetSubject(out HelpSubjectValue? subject) && key.Equals(subject);
        }

        public HelpSubject() : base()
        {
            InitializeComponent();
            formData = new FormBinding(ref helpBinding) { DoWork = base.DoWork };

            SetRowState(helpBinding);
            SetTitle(helpBinding);
            SetCommand(ScopeType.ApplicationHelpPage,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.SecurityDatabase);

            // Store and recompute column sizes for List views
            controlData.ResizeColumns();
            controlData.Enabled = false;
        }

        public HelpSubject(IHelpSubjectIndex helpSubject) : this()
        { formData.SetIndex(helpSubject); }

        public HelpSubject(IHelpSubjectIndex helpSubject, ITemporalIndex temporal) : this(helpSubject)
        { formData.SetIndex(helpSubject, temporal); needsData = true; }

        public HelpSubject(IHelpSubjectIndex helpSubject, IEnumerable<HelpControlValue> source) : this(helpSubject)
        {
            formData.HelpControls.AddRange(ControlValue.Create(source));

            // Add all the forms controls to ListView
            foreach (var item in formData.HelpControls)
            {
                if (item.IsForm)
                { controlsGroup.Text = String.Format("Controls for: {0}", item.Path.Format("{0}")); }

                ListViewItem newItem = new ListViewItem(item.ControlName);
                newItem.SubItems.Add(item.ControlType);
                item.ListItem = newItem;

                if (formData.TryGetSubject(out HelpSubjectValue? helpValue)
                    && helpValue.NameSpace is not null)
                {
                    PathIndex helpPath = new PathIndex(PathIndex.Parse(helpValue.NameSpace).ToArray());

                    if (helpPath.Equals(item.Path))
                    { newItem.Checked = true; }
                }

                controlData.Items.Add(newItem);
            }

            controlData.Enabled = true;
        }

        public HelpSubject(IHelpSubjectIndex helpSubject, Form targetForm) : this(helpSubject, HelpControlValue.Create(targetForm))
        { }

        private void HelpTextData_Load(object sender, EventArgs e)
        {
            if (needsData)
            { formData.Load(onCompleting); }
            else
            { DoBinding(); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { if (args.Error is null) { DoBinding(); } }

            void DoBinding()
            {
                helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(HelpSubjectValue.HelpSubject), false, DataSourceUpdateMode.OnValidation));
                helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpBinding, nameof(HelpSubjectValue.NameSpace), false, DataSourceUpdateMode.OnValidation));
                helpToolTipData.DataBindings.Add(new Binding(nameof(helpToolTipData.Text), helpBinding, nameof(HelpSubjectValue.HelpToolTip), false, DataSourceUpdateMode.OnValidation));
                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnValidation));

                SetAuthorization(formData.GetAuthorization);
            }
        }

        [Obsolete()]
        private void BindRtfHelpText()
        {
            try // If RTF, bind to the RTF property
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnValidation)); }
            catch (Exception) // Else it is not RTF, bind to the property 
            {
                if (helpBinding.Current is HelpSubjectValue subject)
                {
                    helpTextData.Text = subject.HelpText ?? String.Empty;
                    subject.HelpText = helpTextData.Rtf;
                    subject.AcceptChanges();
                }

                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnValidation));
            }
        }

        private void ControlData_Resize(object sender, EventArgs e)
        { controlData.ResizeColumns(); }

        ListViewItem? currentItem = null; // To prevent recursive calls
        private void ControlData_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (currentItem is null && e.Item.Checked)
            {
                // ItemChecked event is called each time the Checked state changes for any item, even in code.
                // The currentItem is used to track the item currently being worked on.
                // This prevents the recursive call fired when the Check state is set by the following loop.
                currentItem = e.Item;

                foreach (ControlValue item in
                formData.HelpControls.Where(w => w.ListItem != e.Item
                    && w.ListItem is not null
                    && w.ListItem.Index >= 0
                    && w.ListItem.Checked))
                {
                    if (item.ListItem is ListViewItem viewItem && viewItem.Index >= 0)
                    { viewItem.Checked = false; }
                }

                if (formData.TryGetSubject(out HelpSubjectValue? current))
                {
                    HelpSubjectIndexPath key = new HelpSubjectIndexPath(current);
                    if (formData.HelpControls.FirstOrDefault(w => w.ListItem == e.Item) is ControlValue selected
                        && !key.Equals(selected.Path))
                    { current.NameSpace = selected.Path.MemberFullPath; }
                }

                currentItem = null;
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);

            HelpSubjectValue newSubject = formData.NewSubject();
            if (formData.HelpControls.Count > 0 && formData.HelpControls.FirstOrDefault(w => w.IsForm) is ControlValue item)
            {
                newSubject.HelpSubject = String.Format("(new Help Subject: {0})", item.Path.Member);
                newSubject.Path = item.Path;
                controlData.SelectedItems.Clear();
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            IsLocked(true);
            formData.RemoveSubject();
        }

        protected override void SecurityCommand_Click(Object sender, EventArgs e)
        {
            base.SecurityCommand_Click(sender, e);

            if (formData.TryGetSubject(out HelpSubjectValue? current))
            {
                SecurableIndex key = new HelpSubjectIndex(current);
                Activate(() => new Security.SecurableManager(key, () => BusinessData.Authorization.IsHelpAdmin));
            }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formData.Load(OnComplete);

            //if (helpBinding.Current is HelpSubjectValue current)
            //{
            //    IDatabaseWork factory = BusinessData.GetDbFactory();
            //    List<WorkItem> work = new List<WorkItem>();
            //    HelpSubjectIndex key = new HelpSubjectIndex(current);
            //    current.Remove();

            //    work.Add(factory.OpenConnection());
            //    work.AddRange(BusinessData.ApplicationData.HelpSubjects.Load(factory, current));

            //    // Unbind the RTF control to avoid errors generated by background thread
            //    helpTextData.DataBindings.Clear();

            //    IsLocked(true);
            //    IsWaitCursor(true);
            //    DoWork(work, onCompleting);

            //    void onCompleting(RunWorkerCompletedEventArgs args)
            //    {
            //        IsWaitCursor(false);

            //        if (args.Error is null)
            //        { IsLocked(false); }

            //        if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
            //        {
            //            if (subjects.FirstOrDefault(w => key.Equals(w)) is HelpSubjectValue subject)
            //            {
            //                BindRtfHelpText();

            //                helpBinding.Position = subjects.IndexOf(subject);
            //            }
            //        }
            //    }
            //}

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formData.Save(OnComplete);

            //if (helpBinding.Current is HelpSubjectValue current)
            //{
            //    HelpSubjectIndex key = new HelpSubjectIndex(current);
            //    TemporalIndex temporalKey = new TemporalIndex(current);

            //    // Remove anything for same subject that is not this subject
            //    foreach (HelpSubjectValue oldValue in BusinessData.ApplicationData.HelpSubjects.Where(w => key.Equals(w) && !ReferenceEquals(current, w)))
            //    { BusinessData.ApplicationData.HelpSubjects.Remove(oldValue); }

            //    // If there is nothing left, add the current subject
            //    if (!BusinessData.ApplicationData.HelpSubjects.Any(w => key.Equals(w)))
            //    { BusinessData.ApplicationData.HelpSubjects.Add(current); }
            //    else { } // If there is something left, it must be this subject and it should not be altered.

            //    IDatabaseWork factory = BusinessData.GetDbFactory();
            //    List<WorkItem> work = new List<WorkItem>();

            //    work.Add(factory.OpenConnection());
            //    work.AddRange(BusinessData.ApplicationData.HelpSubjects.Save(factory, current));

            //    IsLocked(true);
            //    IsWaitCursor(true);
            //    DoWork(work, onCompleting);

            //    void onCompleting(RunWorkerCompletedEventArgs args)
            //    {
            //        IsWaitCursor(false);

            //        if (args.Error is null)
            //        {
            //            current.AcceptChanges();
            //            IsLocked(false);
            //        }
            //    }
            //}

            void OnComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formData.RemoveSubject();
            formData.Save(OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        private void helpBinding_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {

        }
    }

}
