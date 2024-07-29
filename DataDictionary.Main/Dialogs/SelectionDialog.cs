using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
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

namespace DataDictionary.Main.Dialogs
{
    partial class SelectionDialog : Form
    {
        public SelectionDialog() : base()
        {
            InitializeComponent();

            titleColumn.Width = selectionData.Width - SystemInformation.VerticalScrollBarWidth;
        }

        /// <summary>
        /// Shows the dialog initially position over the form that called it.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(Form source)
        {
            if (source.IsMdiChild
                && source.MdiParent is Form parent
                && parent.Controls.Cast<Control>().FirstOrDefault(w => w is MdiClient) is Control mdiControl)
            {
                Point mdiTopLeft = mdiControl.Location;
                Point formOffset = source.Location; // The Forms location is offset from the MDI container

                // Get the sum of the controls that are on the top and left edges of the MDI Container
                // Note: Menu Strips and some other controls are above or to the left of the MDI container.
                // This places them outside of the MDI container.
                // We are interested in controls that take up space within the MDI container.
                // ToolStrips are within the MDI, but other controls may need to be accounted for.
                Int32 topSum = parent.Controls.
                    Cast<Control>().
                    Where(w => w.Dock == DockStyle.Top && w is ToolStrip).
                    Sum(s => s.Bottom);
                Int32 leftSum = parent.Controls.
                    Cast<Control>().
                    Where(w => w.Dock == DockStyle.Left && w is ToolStrip).
                    Sum(s => s.Right);

                var topLeft = Point.Add(Point.Add(mdiControl.Location, new Size(leftSum, topSum)), new Size(source.Location));

                this.StartPosition = FormStartPosition.Manual;
                this.Location = topLeft;
            }


            return base.ShowDialog(source);
        }

        private void SelectionData_SizeChanged(object sender, EventArgs e)
        { titleColumn.Width = selectionData.Width - SystemInformation.VerticalScrollBarWidth; }
    }

    class SelectionDialog<TValue, TIndex> : SelectionDialog
        where TValue : INamedScopeSourceValue
        where TIndex : class, IKey//, IKeyEquality<TIndex>
    {
        public IEnumerable<TValue> DataSource { get; init; } = new List<TValue>();
        public IList<TIndex> Selected { get; init; } = new List<TIndex>();

        public Func<TValue, TIndex?> AsIndex { get; init; } = (value) => { return null; };

        public Func<TValue, String> GetDescription { get; init; } = (value) => { return String.Empty; };

        public Func<TValue, String> GetTitle { get; init; } = (value) => { return value.Title; };

        Dictionary<ListViewItem, TValue> listViewItems = new Dictionary<ListViewItem, TValue>();


        public SelectionDialog() : base()
        {
            selectionData.SmallImageList = ImageEnumeration.AsImageList();

            this.Load += SelectionDialog_Load;
            selectionData.ItemChecked += SelectionData_ItemChecked;
            selectionData.SelectedIndexChanged += SelectionData_SelectedIndexChanged;
        }

        private void SelectionDialog_Load(Object? sender, EventArgs e)
        {
            foreach (TValue item in DataSource)
            {
                ListViewItem newItem = new ListViewItem(item.Title);
                selectionData.Items.Add(newItem);
                listViewItems.Add(newItem, item);
                newItem.ImageKey = ImageEnumeration.Cast(item.Scope).Name;

                if (Selected.FirstOrDefault(w => w.Equals(AsIndex(item))) is TIndex)
                { newItem.Checked = true; }
            }
        }

        private void SelectionData_ItemChecked(Object? sender, ItemCheckedEventArgs e)
        {
            if (listViewItems.ContainsKey(e.Item))
            {
                if (e.Item.Checked
                    && AsIndex(listViewItems[e.Item]) is TIndex addKey
                    && !Selected.Contains(addKey))
                { Selected.Add(addKey); }

                if (!e.Item.Checked
                    && AsIndex(listViewItems[e.Item]) is TIndex removeKey
                    && Selected.Contains(removeKey))
                { Selected.Remove(removeKey); }
            }
        }

        private void SelectionData_SelectedIndexChanged(Object? sender, EventArgs e)
        {
            if (selectionData.SelectedItems.Count > 0
                && listViewItems.ContainsKey(selectionData.SelectedItems[0]))
            {
                titleData.Text = listViewItems[selectionData.SelectedItems[0]].Title;
                descriptionData.Text = GetDescription(listViewItems[selectionData.SelectedItems[0]]);
                pathData.Text = listViewItems[selectionData.SelectedItems[0]].Path.MemberFullPath;
            }

        }

    }
}
