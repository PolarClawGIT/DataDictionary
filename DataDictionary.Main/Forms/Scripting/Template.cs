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
    partial class Template : ApplicationData
    {
        public Template()
        {
            InitializeComponent();
            toolStrip.TransferItems(templateCommands, 0);

        }

        private void Template_Load(object sender, EventArgs e)
        {

        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        private void deleteTemplateCommand_Click(object sender, EventArgs e)
        {

        }

        private void dataPrevieweBuild_Click(object sender, EventArgs e)
        {

        }

        private void dataPreviewSave_Click(object sender, EventArgs e)
        {

        }

        private void transformFormat_Click(object sender, EventArgs e)
        {

        }

        private void transformRenderXML_Click(object sender, EventArgs e)
        {

        }

        private void transformRenderText_Click(object sender, EventArgs e)
        {

        }

        private void documentsBuild_Click(object sender, EventArgs e)
        {

        }

        private void documentSave_Click(object sender, EventArgs e)
        {

        }

        private void documentSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void documentDirectorySet_Click(object sender, EventArgs e)
        {

        }

        private void documentSaveAll_Click(object sender, EventArgs e)
        {

        }


    }
}
