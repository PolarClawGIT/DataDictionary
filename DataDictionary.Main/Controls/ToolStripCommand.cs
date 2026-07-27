using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// Wrapper class to hold a Dictionary of CommandTools
    /// </summary>
    /// <remarks>
    /// This is intended to enhance a ToolStrip by creating a list of Command Buttons that can be accessed from child forms.<br/>
    /// The buttons do not have to be part of the same Toolstrip.
    /// </remarks>
    class ToolStripCommandCollection : Dictionary<ButtonType, ToolStripCommand>
    {
        /// <inheritdoc cref="Dictionary{TKey, TValue}.Dictionary()"/>
        public ToolStripCommandCollection() : base() { }


        public void Add(ToolStripCommand item)
        { base.Add(item.Command, item); }

        public void AddRange(params IEnumerable<ToolStripCommand> items)
        {
            foreach (var item in items)
            { Add(item); }
        }
    }

    /// <summary>
    /// Wrapper class around a ToolStripItem for a Command Button.
    /// </summary>
    class ToolStripCommand
    {
        /// <summary>
        /// Base control being that this class is a wrapper for.
        /// </summary>
        protected readonly ToolStripItem Control;

        /// <summary>
        /// Check to see if Enabled is allowed. Called by Enabled and Refresh.
        /// </summary>
        /// <remarks>Allows the Enabled behavior to be enhanced by adding way to verify that a command is valid.</remarks>
        public Func<Boolean> AllowEnabled
        {
            get { return field; }
            init { field = value; Control.Enabled = value(); }
        } = () => true;

        /// <summary>
        /// The Button Command Type that this item represents.
        /// </summary>
        public ButtonType Command
        {
            get;
            init
            {
                field = value;
                if (Scope is not ScopeType.Null)
                { Control.Image = Scope.GetImage(value); }
            }
        } = ButtonType.Default;

        /// <summary>
        /// The Command Scope that this item represents. ScopeType.Null = not specific.
        /// </summary>
        public ScopeType Scope
        {
            get;
            set
            {
                field = value;
                if (value is not ScopeType.Null)
                { Control.Image = value.GetImage(Command); }
            }
        } = ScopeType.Null;

        /// <inheritdoc cref="ToolStripItem.Enabled"/>
        /// <remarks>If AllowEnabled is false, this can return true as the "intended" state while the button remains disabled.</remarks>
        public Boolean Enabled
        {
            get { return field; }
            set
            {
                field = value;
                Refresh();
            }
        }

        /// <inheritdoc cref="ToolStripItem.Text"/>
        public String Text
        {
            get { return Control.Text ?? String.Empty; }
            set { Control.Text = value; }
        }

        /// <inheritdoc cref="ToolStripItem.ToolTipText"/>
        public String ToolTipText
        {
            get { return Control.ToolTipText ?? String.Empty; }
            set { Control.ToolTipText = value; }
        }

        /// <inheritdoc cref="ToolStripItem.Visible"/>
        public Boolean Visible
        {
            get { return field; }
            set
            {
                Control.Visible = value;
                field = value;

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

        /// <inheritdoc cref="ToolStripDropDownItem.DropDown"/>
        /// <remarks>Applies to ToolStripDropDownButton and ToolStripSplitButton </remarks>
        public ToolStripDropDown? DropDown
        {
            get
            {
                if (Control is ToolStripDropDownItem dropDownItem)
                { return dropDownItem.DropDown; }
                else { return null; }
            }
            set
            {
                if (Control is ToolStripDropDownItem dropDownItem)
                { dropDownItem.DropDown = value; }

                if (Control is ToolStripDropDownButton dropDownButton)
                {
                    if (value is null)
                    { dropDownButton.ShowDropDownArrow = false; }
                    else { dropDownButton.ShowDropDownArrow = true; }
                }
            }
        }

        /// <inheritdoc cref="ToolStripItem.Click"/>
        public event EventHandler? Click
        {
            add { Control.Click += value; }
            remove { Control.Click -= value; }
        }

        /// <summary>
        /// Constructor for the CommandTool.
        /// </summary>
        /// <param name="toolStrip"></param>
        public ToolStripCommand(ToolStripItem toolStrip) : base()
        { Control = toolStrip; }

        public ToolStripCommand(ToolStripItem toolStrip, EventHandler? onClick = null) : this(toolStrip)
        {
            if (onClick is EventHandler)
            { Click += onClick; }
        }

        public ToolStripCommand(ButtonType command, ToolStripItem toolStrip, EventHandler? onClick = null) : this(toolStrip, onClick)
        { Command = command; }

        public ToolStripCommand(ScopeType scope, ButtonType command, ToolStripItem toolStrip, EventHandler? onClick = null) : this(command, toolStrip, onClick)
        { Scope = scope; }

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

        /// <summary>
        /// Used to refresh the state of the Command by re-evaluating the AllowEnabled
        /// </summary>
        public void Refresh()
        {
            if (AllowEnabled())
            { Control.Enabled = true && Enabled; }
            else { Control.Enabled = false; }
        }

        /// <inheritdoc cref="ToString"/>
        public override String ToString()
        { return Control.ToString(); }
    }
}
