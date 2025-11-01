namespace DataDictionary.Main.Controls
{
    /// <summary>
    /// For use on User Defined Controls that wrapper a TextBoxBase control.
    /// This is used when passing clipboard/Edit menu commands.
    /// </summary>
    internal interface ISupportEditMenu
    {
        void Cut();
        void Copy();
        void Paste();
        void SelectAll();
        void Undo();
    }
}
