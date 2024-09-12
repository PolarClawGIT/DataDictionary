using DataDictionary.BusinessLayer.Modification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms
{
    /// <summary>
    /// Layout of the History View form
    /// </summary>
    partial class HistoryView : ApplicationData
    {
        public HistoryView() : base()
        {
            InitializeComponent();
        }

        protected virtual void HistoryView_Load(object sender, EventArgs e)
        {
            // TODO: Does not work. Databinding only work with single inheritance, not interfaces (multi-inheritance)
            // Need to send the column to be bound to. Use Properties to tell it what to bind to.
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingModification, nameof(IModificationValue.Title)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingModification, nameof(IModificationValue.Description)));
            modificationData.DataBindings.Add(new Binding(nameof(modificationData.Text), bindingModification, nameof(IModificationValue.Modification)));
            modifiedByData.DataBindings.Add(new Binding(nameof(modifiedByData.Text), bindingModification, nameof(IModificationValue.ModifiedBy)));
            modifiedOnDate.DataBindings.Add(new Binding(nameof(modifiedOnDate.Text), bindingModification, nameof(IModificationValue.ModifiedOn)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingModification, nameof(IModificationValue.Description)));

            historyData.AutoGenerateColumns = false;
            historyData.DataSource = bindingModification;
        }

        protected virtual void ViewDetailCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void RestoreCommand_Click(object sender, EventArgs e)
        {

        }

        protected virtual void ReQueryCommand_Click(object sender, EventArgs e)
        {

        }
    }
}
