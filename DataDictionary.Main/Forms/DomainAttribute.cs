using DataDictionary.DataLayer.DomainData;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms
{
    partial class DomainAttribute : ApplicationFormBase
    {
        class FormData
        {
            public IDomainAttributeItem? DomainAttribute { get; set; }
            public IDomainAttributeItem? ParentAttribute { get; set; }
        }

        FormData data = new FormData();

        public DomainAttribute() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainAttribute;
        }

        public DomainAttribute(IDomainAttributeItem domainAttributeItem) : this()
        {
            data.DomainAttribute = domainAttributeItem;
            if (Program.DomainData.DomainAttributes.GetParentAttribute(data.DomainAttribute) is IDomainAttributeItem parent)
            { data.ParentAttribute = parent; }
            else { data.ParentAttribute = new DomainAttributeItem(); }
        }

        private void DomainAttribute_Load(object sender, EventArgs e)
        {
            attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), data.DomainAttribute, nameof(data.DomainAttribute.AttributeTitle)));
            attributeTextData.DataBindings.Add(new Binding(nameof(attributeTextData.Rtf), data.DomainAttribute, nameof(data.DomainAttribute.AttributeText)));
            attributeParentTitleData.DataBindings.Add(new Binding(nameof(attributeParentTitleData.Text), data.ParentAttribute, nameof(data.ParentAttribute.AttributeTitle)));
        }
    }
}
