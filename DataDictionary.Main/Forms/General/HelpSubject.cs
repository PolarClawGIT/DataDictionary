using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject : ApplicationData, IApplicationDataForm
    {
        class ControlItem
        {
            public ListViewItem? ListItem { get; set; }
            public String ControlType { get; private set; }
            public HelpSubjectIndexPath ControlName { get; private set; }
            public Boolean IsForm { get; private set; }

            public ControlItem(Control source)
            {
                ControlName = source.ToNameSpaceKey();

                if (source is Form)
                {
                    if (source.GetType().BaseType is Type baseType)
                    { ControlType = baseType.Name; }
                    else { ControlType = source.GetType().Name; }

                    IsForm = true;
                }
                else
                {
                    Control root = source;
                    while (root is not Form && root.Parent is not null)
                    { root = root.Parent; }

                    ControlType = source.GetType().Name;
                    IsForm = false;
                }
            }

            public override string ToString()
            { return ControlName.MemberFullPath; }
        }

        BindingList<ControlItem> controlList = new BindingList<ControlItem>();

        Dictionary<ColumnHeader, Single> controlValuesWidths;

        public Boolean IsOpenItem(object? item)
        { return helpBinding.Current is IHelpSubjectValue current && ReferenceEquals(current, item); }

        public HelpSubject() : base()
        {
            InitializeComponent();

            Setup(
                helpBinding,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            // Store and recompute column sizes for List views
            controlValuesWidths = controlData.Columns.
                OfType<ColumnHeader>().
                Select(s => new
                {
                    column = s,
                    value = (Single)s.Width / (Single)(controlData.Columns.OfType<ColumnHeader>().Sum(v => v.Width))
                }).
                ToDictionary(k => k.column, v => v.value);

        }

        public HelpSubject(HelpSubjectValue? helpSubjectItem) : this()
        {
            if (helpSubjectItem is null)
            {
                helpSubjectItem = new HelpSubjectValue();
                BusinessData.ApplicationData.HelpSubjects.Add(helpSubjectItem);
            }

            HelpSubject_Binding(helpSubjectItem);
        }

        private void HelpSubject_Binding(HelpSubjectValue helpSubjectItem)
        {
            HelpSubjectIndex key = new HelpSubjectIndex(helpSubjectItem);
            TemporalIndex temporalKey = new TemporalIndex(helpSubjectItem);
            BindingView<HelpSubjectValue> bindingData = new BindingView<HelpSubjectValue>(BusinessData.ApplicationData.HelpSubjects, w => key.Equals(w) && temporalKey.Equals(w));

            if (bindingData.Count > 0)
            {
                helpBinding.DataSource = bindingData;
                helpBinding.Position = 0;
            }
            else
            {
                BindingList<HelpSubjectValue> unboundData = new BindingList<HelpSubjectValue> { helpSubjectItem };

                helpBinding.DataSource = unboundData;
                helpBinding.Position = 0;

                CommandButtons[CommandImageType.Delete].IsEnabled = false;
                CommandButtons[CommandImageType.OpenDatabase].IsEnabled = false;
                CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = false;
            }
        }

        public HelpSubject(HelpSubjectValue helpSubjectItem, Form targetForm) : this()
        {
            HelpSubject_Binding(helpSubjectItem);

            List<Control> values = targetForm.ToControlList()
                .Where(w => !String.IsNullOrWhiteSpace(w.Name)
                            && w is not Form
                            && !(w is Panel or ToolStrip or MenuStrip or SplitContainer or Splitter))
                .OrderBy(o => o is not Form)
                .ThenBy(o => o.ToNameSpaceKey())
                .ToList();

            // Add Group level for form to ListView
            ControlItem baseForm = new ControlItem(targetForm);
            ListViewItem baseItem = new ListViewItem(baseForm.ControlName.Member);

            baseForm.ListItem = baseItem;
            controlList.Add(baseForm);
            controlData.Items.Add(baseItem);
            controlsGroup.Text = String.Format("Controls for: {0}", baseForm.ControlName.Format("{0}"));

            // Add all the forms controls to ListView
            foreach (Control item in values)
            {
                ControlItem newControl = new ControlItem(item);
                String itemName = newControl.ControlName.Format("{0}")
                    .Replace(String.Format("{0}.", baseForm.ControlName.Format("{0}")), String.Empty);
                ListViewItem newItem = new ListViewItem(itemName);
                newItem.SubItems.Add(newControl.ControlType);
                newControl.ListItem = newItem;

                if (helpBinding.Current is IHelpSubjectValue helpValue
                    && helpValue.NameSpace is not null)
                {
                    PathIndex helpPath = new PathIndex(PathIndex.Parse(helpValue.NameSpace).ToArray());

                    if (helpPath.Equals(newControl.ControlName))
                    { newItem.Checked = true; }
                }

                controlList.Add(newControl);
                controlData.Items.Add(newItem);
            }
        }

        private void HelpTextData_Load(object sender, EventArgs e)
        {
            helpSubjectData.DataBindings.Add(new Binding(nameof(helpSubjectData.Text), helpBinding, nameof(HelpSubjectValue.HelpSubject), false, DataSourceUpdateMode.OnPropertyChanged));
            helpNameSpaceData.DataBindings.Add(new Binding(nameof(helpNameSpaceData.Text), helpBinding, nameof(HelpSubjectValue.NameSpace), false, DataSourceUpdateMode.OnPropertyChanged));
            helpToolTipData.DataBindings.Add(new Binding(nameof(helpToolTipData.Text), helpBinding, nameof(HelpSubjectValue.HelpToolTip), false, DataSourceUpdateMode.OnPropertyChanged));

            BindRtfHelpText();
        }

        private void BindRtfHelpText()
        {
            try // If RTF, bind to the RTF property
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnPropertyChanged)); }
            catch (Exception) // Else it is not RTF, bind to the property 
            {
                if (helpBinding.Current is HelpSubjectValue subject)
                {
                    helpTextData.Text = subject.HelpText ?? String.Empty;
                    subject.HelpText = helpTextData.Rtf;
                    subject.AcceptChanges();
                }

                helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), helpBinding, nameof(HelpSubjectValue.HelpText), false, DataSourceUpdateMode.OnPropertyChanged));
            }
        }

        private void ControlData_Resize(object sender, EventArgs e)
        {
            controlData.Columns.
                OfType<ColumnHeader>().
                ToList().
                ForEach(f => f.Width = (Int32)(
                    (controlData.Width -
                        SystemInformation.VerticalScrollBarWidth) *
                    controlValuesWidths[f]));
        }

        ListViewItem? currentItem = null; // To prevent recursive calls
        private void ControlData_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            if (currentItem is null && e.Item.Checked)
            {
                // ItemChecked event is called each time the Checked state changes for any item, even in code.
                // The currentItem is used to track the item currently being worked on.
                // This prevents the recursive call fired when the Check state is set by the following loop.
                currentItem = e.Item;

                foreach (ControlItem item in
                controlList.Where(w => w.ListItem != e.Item
                    && w.ListItem is not null
                    && w.ListItem.Index >= 0
                    && w.ListItem.Checked))
                {
                    if (item.ListItem is ListViewItem viewItem && viewItem.Index >= 0)
                    { viewItem.Checked = false; }
                }

                if (helpBinding.Current is HelpSubjectValue current)
                {
                    HelpSubjectIndexPath key = new HelpSubjectIndexPath(current);
                    if (controlList.FirstOrDefault(w => w.ListItem == e.Item) is ControlItem selected
                        && !key.Equals(selected.ControlName))
                    { current.NameSpace = selected.ControlName.MemberFullPath; }

                }

                currentItem = null;
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            helpBinding.AddNew();
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            { helpBinding.RemoveCurrent(); }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                HelpSubjectIndex key = new HelpSubjectIndex(current);
                current.Remove();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Load(factory, current));

                // Unbind the RTF control to avoid errors generated by background thread
                helpTextData.DataBindings.Clear();

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    { IsLocked(false); }

                    if (helpBinding.DataSource is IList<HelpSubjectValue> subjects)
                    {
                        if (subjects.FirstOrDefault(w => key.Equals(w)) is HelpSubjectValue subject)
                        {
                            BindRtfHelpText();

                            helpBinding.Position = subjects.IndexOf(subject);
                        }
                    }
                }
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current)
            {
                if (helpBinding.DataSource is BindingView<HelpSubjectValue>) { }
                else if (helpBinding.DataSource is BindingList<HelpSubjectValue>)
                {
                    var key = new HelpSubjectIndex(current);

                    if (BusinessData.ApplicationData.HelpSubjects.
                        FirstOrDefault(w => key.Equals(w)) is HelpSubjectValue oldValue)
                    { BusinessData.ApplicationData.HelpSubjects.Remove(oldValue); }

                    BusinessData.ApplicationData.HelpSubjects.Add(current);
                }

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Save(factory, current));

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    {
                        current.AcceptChanges();
                        RowState = current.RowState();
                        IsLocked(false);
                    }
                }
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            if (helpBinding.Current is HelpSubjectValue current
                && current.NameSpace is String
                && current.NameSpace != Settings.Default.DefaultSubject) // Cannot Delete the Default Subject
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                current.Remove();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.HelpSubjects.Save(factory, current));

                IsLocked(true);
                IsWaitCursor(true);
                DoWork(work, onCompleting);

                void onCompleting(RunWorkerCompletedEventArgs args)
                {
                    IsWaitCursor(false);

                    if (args.Error is null)
                    { IsLocked(false); }
                }
            }
        }
    }

}
