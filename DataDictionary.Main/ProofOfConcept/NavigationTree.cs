using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.Threading;

namespace DataDictionary.Main.ProofOfConcept
{
    partial class NavigationTree : ApplicationData, IApplicationDataForm
    {

        public NavigationTree()
        {
            InitializeComponent();
            mainTree.DoWork = DoWork; // Pass the work method to the control
        }

        private void NavigationTree_Load(object sender, EventArgs e)
        {
            mainTree.Reload();
        }
    }
}
