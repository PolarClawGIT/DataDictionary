using DataDictionary.DataLayer.DomainData.Entity;
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

namespace DataDictionary.Main.Forms.Domain
{
     partial class DomainEntity : ApplicationBase, IApplicationDataForm
    {
        public DomainEntityKey DataKey { get; private set; }

        public DomainEntity() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_ClassPublic;
            DataKey = new DomainEntityKey(new DomainEntityItem());
        }

        public DomainEntity(IDomainEntityKey domainEntityItem) : this()
        { DataKey = new DomainEntityKey(domainEntityItem); }

        public Boolean IsOpenItem(object? item)
        { return DataKey.Equals(item); }
    }
}
