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

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateManager : ApplicationData
    {
        FormBinding formBinding;

        public TemplateManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.Scripting);
            AddCommands(templateCommands);

            formBinding = new FormBinding()
            {
                //BindingAlias = bindingAlias,
                //BindingAttribute = bindingAttribute,
                //BindingSubjectArea = bindingSubjectArea,
                //BindingProperty = bindingProperty,
                //BindingDefinition = bindingDefinition,
                DoWork = base.DoWork
            };
        }

        private void TemplateManager_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                //throw new NotImplementedException();
            }
        }

    }
}
