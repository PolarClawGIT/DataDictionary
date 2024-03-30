using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ToolStripExtension
    {
        /// <summary>
        /// Transfers/Moves the Context Menu Strip items and puts them into the ToolStrip
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="index">
        /// 0 = place at start, otherwise the location specified or at the end
        /// </param>
        public static void TransferItems(this ToolStrip target, ContextMenuStrip source, Int32 index)
        {
            // There is an undocumented behavior with ToolStripItemCollection.Insert (and Add).
            // If the ToolStripItem is part of another ToolStripItemCollection,
            // the ToolStripItem is removed from the other collection and added to the
            // target collection.

            if (target.Items.Count > index)
            { target.Items.Insert(index, new ToolStripSeparator()); }

            if (index > target.Items.Count)
            { index = target.Items.Count; }

            while (source.Items.Count > 0)
            {
                ToolStripItem item = source.Items[0];
                source.Items.Remove(item); // Because I cannot guarantee the behavior of ToolStripItemCollection.Insert

                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                if (String.IsNullOrWhiteSpace(item.ToolTipText) && !String.IsNullOrWhiteSpace(item.Text))
                { item.AutoToolTip = true; }

                target.Items.Insert(index, item); // Insert will remove the item from the source ToolStripItemCollection.
                index = index + 1;
            }
        }

    }
}
