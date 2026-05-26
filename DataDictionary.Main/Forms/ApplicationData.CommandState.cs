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
                get { return field; }
                set
                {
                    field = value;
                    Control.Enabled = value && AllowEnabled() && IsAuthorized(Command);
                }
            }

            public Func<Boolean> AllowEnabled { get; init; } = () => true;
            public Func<ButtonType, Boolean> IsAuthorized { get; init; } = (command) => true;

            public void Refresh()
            { Control.Enabled = IsEnabled && AllowEnabled() && IsAuthorized(Command); }

            /// <summary>
            /// Is the Command Button Visible
            /// </summary>
            public Boolean IsVisible
            {
                get { return Control.Visible; }
                set
                {
                    Control.Visible = value;

                    // Make the first separator after this control also visible.
                    if (value
                        && Control.GetCurrentParent() is ToolStrip tools
                        && tools.Items.
                            OfType<ToolStripSeparator>().
                            FirstOrDefault(w => tools.Items.IndexOf(w) > tools.Items.IndexOf(Control))
                                is ToolStripSeparator separator
                        && !separator.Visible)
                    { separator.Visible = true; }
                }
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
                get { return field; }
                set
                {
                    field = value;
                    Image = value.GetImage(Command);
                }
            }

            /// <summary>
            /// Command Type associated with the CommandState
            /// </summary>
            public ButtonType Command
            {
                get { return field; }
                set
                {
                    field = value;
                    Image = Scope.GetImage(value);
                }
            }

            public CommandState(ToolStripItem control) : base()
            { Control = control; }

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
        }


    }
}
