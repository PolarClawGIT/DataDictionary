using DataDictionary.Main.Messages;
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
    [Obsolete("do not use", true)]
    partial class DomainAttributeView : ApplicationFormBase
    {
        public DomainAttributeView() : base()
        {
            InitializeComponent();
        }

        private void DomainAttributeView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        { attributeData.DataSource = Program.Data.DomainAttributes; }

        void UnBindData() { attributeData.DataSource = null; }

        #region IColleague
        #endregion
    }
}
