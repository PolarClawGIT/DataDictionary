using DataDictionary.Main.Enumerations;
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

namespace DataDictionary.Main.Forms.Security
{
    partial class RoleManager : ApplicationData
    {
        public RoleManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.SecurityRole);
            SetCommand(ScopeType.SecurityRole,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);
        }
    }
}
