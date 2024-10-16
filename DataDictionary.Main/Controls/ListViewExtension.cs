using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ListViewExtension
    {
        // Used to store additional information about the ListView
        static Dictionary<ListView, Dictionary<ColumnHeader, Int32>> baseWidths = new Dictionary<ListView, Dictionary<ColumnHeader, Int32>>();

        /// <summary>
        /// Resizes all column proportionally based on the original column sizes.
        /// </summary>
        /// <param name="listView"></param>
        /// <remarks>Use with the ListView Resize event</remarks>
        static public void ResizeColumns(this ListView listView)
        {
            // The +3 is a fudge factor to account for the bar in the header
            Single scrollBarWidth = SystemInformation.VerticalScrollBarWidth + 3;

            if (!baseWidths.ContainsKey(listView))
            {   // Store the original widths so the recalculation uses a consistent value.
                baseWidths.Add(listView, new Dictionary<ColumnHeader, Int32>());

                listView.Disposed += ListView_Disposed;

                foreach (ColumnHeader item in listView.Columns)
                {
                    baseWidths[listView].Add(item, item.Width);
                    item.Disposed += Column_Disposed;
                }
            }

            Single sumWidth = baseWidths[listView].Values.Sum();

            if (sumWidth >= listView.Width - scrollBarWidth)
            {
                foreach (var item in baseWidths[listView])
                { item.Key.Width = item.Value; }
            }
            else
            {
                foreach (var item in baseWidths[listView])
                {
                    item.Key.Width = (Int32)(
                        ((Single)listView.Width - scrollBarWidth) *
                        ((Single)item.Value / sumWidth));
                }
            }

            void ListView_Disposed(Object? sender, EventArgs e)
            {   // Make sure the List View is cleaned up
                if (baseWidths.ContainsKey(listView))
                {

                    while (baseWidths[listView].Count > 0)
                    {
                        ColumnHeader header = baseWidths[listView].First().Key;
                        header.Disposed -= Column_Disposed;
                        baseWidths[listView].Remove(header);
                    }

                    listView.Disposed -= ListView_Disposed;
                    baseWidths.Remove(listView);
                }
            }

            void Column_Disposed(Object? sender, EventArgs e)
            {   // Make sure the List View Columns are cleaned up

                if (sender is ColumnHeader header && baseWidths.ContainsKey(listView) && baseWidths[listView].ContainsKey(header))
                {
                    header.Disposed -= Column_Disposed;
                    baseWidths[listView].Remove(header);
                }
            }
        }
    }
}
