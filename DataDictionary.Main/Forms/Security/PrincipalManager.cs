﻿using DataDictionary.Main.Enumerations;
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
    partial class PrincipalManager : ApplicationData
    {
        //TODO: Build out screen.
        // Thinking List of Principals on left.
        // Details for the selected Principal.
        // List of Role Membership and Ownership  (view only? Scoped to Model?)

        public PrincipalManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.SecurityPrinciple);
            SetCommand(ScopeType.SecurityPrinciple,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);
        }
    }
}
