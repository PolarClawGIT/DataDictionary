using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformManager : ApplicationData
    {
        public TransformManager()
        {
            InitializeComponent();
            toolStrip.TransferItems(transformToolStrip, 0);
        }

        private void addTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void TransformManager_Load(object sender, EventArgs e)
        {

        }
    }
}
