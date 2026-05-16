using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        protected class CommandState
        {
            readonly ToolStripItem Control;

            /// <summary>
            /// Is the Command Button Enabled
            /// </summary>
            public Boolean IsEnabled
            {
                get { return Control.Enabled; }
                set
                {
                    Control.Enabled = value && AllowEnabled() && IsAuthorized(Command);
                    isEnabled = value;
                }
            }
            Boolean isEnabled = false; // Intended State
            public Func<Boolean> AllowEnabled { get; init; } = () => true;
            public Func<ButtonType, Boolean> IsAuthorized { get; init; } = (command) => true;

            public void Refresh()
            { Control.Enabled = isEnabled && AllowEnabled() && IsAuthorized(Command); }

            /// <summary>
            /// Is the Command Button Visible
            /// </summary>
            public Boolean IsVisible
            {
                get { return Control.Visible; }
                set { Control.Visible = value; }
            }

            /// <summary>
            /// Image for the Command Button
            /// </summary>
            public Image? Image
            {
                get { return Control.Image; }
                set
                {
                    Control.Image = value;
                    Control.Invalidate();
                }
            }

            /// <summary>
            /// Scope associated with the CommandState
            /// </summary>
            public ScopeType Scope
            {
                get { return scopeValue; }
                set
                {
                    scopeValue = value;
                    Image = value.GetImage(Command);
                }
            }
            ScopeType scopeValue;

            /// <summary>
            /// Command Type associated with the CommandState
            /// </summary>
            public ButtonType Command
            {
                get { return commandValue; }
                set
                {
                    commandValue = value;
                    Image = Scope.GetImage(value);
                }
            }
            ButtonType commandValue;

            public CommandState(ToolStripItem control) : base()
            {
                Control = control;
                control.VisibleChanged += Control_VisibleChanged;
            }

            public void AddTo(IDictionary<ButtonType, CommandState> target)
            { target.Add(this.Command, this); }

            /// <summary>
            /// Text for the Control
            /// </summary>
            public String Text
            {
                get { return Control.Text ?? String.Empty; }
                set { Control.Text = value; }
            }

            public ToolStripDropDown? DropDown
            {
                get
                {
                    if (Control is ToolStripDropDownButton dropButton)
                    { return dropButton.DropDown; }
                    else if (Control is ToolStripSplitButton splitButton)
                    { return splitButton.DropDown; }
                    else { return null; }
                }
                set
                {
                    if (Control is ToolStripDropDownButton dropButton)
                    {
                        if (value is null)
                        { dropButton.ShowDropDownArrow = false; }
                        else { dropButton.ShowDropDownArrow = true; }

                        dropButton.DropDown = value;
                    }
                    else if (Control is ToolStripSplitButton splitButton)
                    { splitButton.DropDown = value; }
                }
            }

            /// <summary>
            /// Returns the IndexOf the control for the command with the tool
            /// </summary>
            /// <returns></returns>
            public Int32 IndexOf()
            {
                if (Control.GetCurrentParent() is ToolStrip parent)
                { return parent.Items.IndexOf(Control); }
                else { return -1; }
            }


            private void Control_VisibleChanged(Object? sender, EventArgs e)
            {
                // Detects if there is anything before the separator and if not, do not show the separator.
                if (sender is ToolStripItem caller && caller.Owner is ToolStrip tools)
                {
                    Int32 before = 0;

                    foreach (ToolStripItem item in tools.Items)
                    {
                        if (item is ToolStripSeparator)
                        {
                            if (before > 0) { item.Visible = true; }
                            else { item.Visible = false; }
                            before = 0;
                        } // Caller has not yet set the Visible flag
                        else if (item.Visible || (item == caller && !item.Visible))
                        { before++; }
                    }

                }
            }
        }


    }
}
