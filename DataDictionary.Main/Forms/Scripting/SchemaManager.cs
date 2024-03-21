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
    partial class SchemaManager : ApplicationData
    {
        public SchemaManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(schemaToolStrip, 0);
        }


        private void SchemaManager_Load(object sender, EventArgs e)
        {

        }

        private void addSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void openSchemaElements_Click(object sender, EventArgs e)
        {

        }

    }
}
