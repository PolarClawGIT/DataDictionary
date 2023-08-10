using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
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

namespace DataDictionary.Main.Forms
{
    partial class DomainProperty : ApplicationFormBase
    {
        class FormData
        {
            public IDomainAttributePropertyKey? PropertyKey { get; set; }
            public IDomainAttributePropertyItem? AttributeProperty { get; set; }
        }
        FormData data = new FormData();

        public Object? OpenItem { get; }

        public DomainProperty()
        {
            InitializeComponent();
            this.Icon = Resources.DomainProperty;
        }

        public DomainProperty(IDomainAttributePropertyItem item) : this()
        {
            data.PropertyKey = new DomainAttributePropertyKey(item);
            OpenItem = item;
            this.Text = data.PropertyKey.ToString();
        }
    }
}
