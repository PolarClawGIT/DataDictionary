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

namespace DataDictionary.Main.ProofOfConcept
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
}
