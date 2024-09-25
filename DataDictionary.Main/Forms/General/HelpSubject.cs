using DataDictionary.BusinessLayer.Application;
using DataDictionary.Main.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.General
{
    partial class HelpSubject : ApplicationData
    {
        public HelpSubject() : base()
        {
            InitializeComponent();

            Setup(
                helpBinding,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);

        }

        public HelpSubject(IHelpSubjectValue? helpSubjectItem) : this() 
        { }
    }
}
