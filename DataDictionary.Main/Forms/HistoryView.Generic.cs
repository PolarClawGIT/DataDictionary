using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    class HistoryView<TValue, TForm> : HistoryView
        where TValue : class, ITemporalValue
        where TForm : ApplicationBase
    {
        /// <summary>
        /// The Constructor for the Detail/Selected Form.
        /// </summary>
        public Func<TValue, TForm>? SelectedForm { get; init; }

        public HistoryView(ScopeType scope, ILoadHistoryData loader) : base(loader)
        {
            Setup(scope,
                CommandImageType.Browse,
                CommandImageType.Open);
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (SelectedValue is TValue value && SelectedForm is not null)
            { Activate(() => SelectedForm(value)); }
        }

        protected override void BrowseCommand_Click(Object? sender, EventArgs e)
        {
            base.BrowseCommand_Click(sender, e);

            if (SelectedValue is not null)
            {
                BindingList<TValue> values = new BindingList<TValue>(GetHistoryDetail(SelectedValue).OfType<TValue>().ToList());

                Form form = Activate((data) => new Forms.DetailDataView(data), values);
                //form.Icon = ImageEnumeration.GetIcon(SelectedValue.Scope);
                //form.Text = SelectedValue.Title;
            }

        }
    }
}
