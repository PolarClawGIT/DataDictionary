using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Simple Binding Navigator.
    /// </summary>
    partial class NavigatorData : UserControl
    {


        // Microsoft version: System.Windows.Forms.BindingNavigator
        //   Does not appear in Toolbox. Not sure if it is supported.
        //   This is intended to fulfill similar functionality

        /// <inheritdoc cref="BindingNavigator.BindingSource"/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingSource? BindingSource
        {
            get { return bindingSource; }
            set
            {
                if (bindingSource is not null)
                {
                    bindingSource.Disposed -= BindingSource_Value_Disposed;
                    bindingSource.CurrentChanged -= BindingSource_CurrentChanged;
                }

                bindingSource = value;

                if (bindingSource is not null)
                {
                    navigationStrip.Enabled = true;
                    bindingSource.Disposed += BindingSource_Value_Disposed;
                    bindingSource.CurrentChanged += BindingSource_CurrentChanged;
                }
                else
                { navigationStrip.Enabled = false; }
            }
        }

        private void BindingSource_CurrentChanged(Object? sender, EventArgs e)
        {
            if (BindingSource is not null && BindingSource.Count > 0)
            {
                currentRowNumber.Text = String.Format("{0:N0}", BindingSource.IndexOf(BindingSource.Current) + 1);
                totalRowNumber.Text = String.Format("{0:N0}", BindingSource.Count);
            }
            else
            {
                currentRowNumber.Text = "##";
                totalRowNumber.Text = "##";
            }
        }

        private void BindingSource_Value_Disposed(Object? sender, EventArgs e)
        {
            if (bindingSource is not null)
            {
                bindingSource.Disposed -= BindingSource_Value_Disposed;
                bindingSource.CurrentChanged -= BindingSource_CurrentChanged;
                bindingSource = null;
            }
        }

        private BindingSource? bindingSource;

        public NavigatorData()
        { InitializeComponent(); }

        /// <inheritdoc cref="BindingSource.MoveFirst"/>
        /// <remarks>Default behavior is to MoveFirst on BindingSource.</remarks>
        public event EventHandler? MoveFirst;
        private void MoveFirstCommand_Click(object sender, EventArgs e)
        {
            if (MoveFirst is EventHandler handler)
            { handler(sender, e); }
            else if (BindingSource is not null)
            { BindingSource.MoveFirst(); }
        }

        /// <inheritdoc cref="BindingSource.MovePrevious"/>
        /// <remarks>Default behavior is to MovePrevious on BindingSource.</remarks>
        public event EventHandler? MovePrevious;
        private void MovePerviousCommand_Click(object sender, EventArgs e)
        {
            if (MovePrevious is EventHandler handler)
            { handler(sender, e); }
            else if (BindingSource is not null)
            { BindingSource.MovePrevious(); }
        }

        /// <inheritdoc cref="BindingSource.MoveNext"/>
        /// <remarks>Default behavior is to MoveNext on BindingSource.</remarks>
        public event EventHandler? MoveNext;
        private void MoveNextCommand_Click(object sender, EventArgs e)
        {
            if (MoveNext is EventHandler handler)
            { handler(sender, e); }
            else if (BindingSource is not null)
            { BindingSource.MoveNext(); }
        }

        /// <inheritdoc cref="BindingSource.MoveLast"/>
        /// <remarks>Default behavior is to MoveLast on BindingSource.</remarks>
        public event EventHandler? MoveLast;
        private void MoveLastCommand_Click(object sender, EventArgs e)
        {
            if (MoveLast is EventHandler handler)
            { handler(sender, e); }
            else if (BindingSource is not null)
            { BindingSource.MoveLast(); }
        }
    }
}
