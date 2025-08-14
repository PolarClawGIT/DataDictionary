using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
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
    partial class Template : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return true; } // TODO: rig to current value

        FormBinding formBinding;

        public Template() : base()
        {
            InitializeComponent();
            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingTemplate);
        }

        public Template(ITemplateIndex? template) : this()
        {
            if (template is ITemplateIndex)
            { formBinding.TemplateIndex = template; }
            else { formBinding.TemplateIndex = formBinding.NewValue(); }
        }

        public Template(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        {
             
        }

        private void Template_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
            }
        }
    }
}
