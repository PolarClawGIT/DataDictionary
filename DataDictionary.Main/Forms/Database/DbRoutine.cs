using DataDictionary.DataLayer.DatabaseData.Routine;
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

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutine : ApplicationBase, IApplicationDataForm
    {
        public DbRoutineKey DataKey { get; private set; }

        public DbRoutine()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Procedure; //TODO: Need to change this on Binding, depending on type.
            DataKey = new DbRoutineKey(new DbRoutineItem());
        }

        public DbRoutine(IDbRoutineKey routineItem) : this()
        {
            DataKey = new DbRoutineKey(routineItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }
    }
}
