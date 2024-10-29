using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Enumerations
{
    class RowStateEnumeration : Enumeration<BindingRowState, RowStateEnumeration>
    {
        /// <summary>
        /// Image used for the RowState
        /// </summary>
        public Image Image { get; init; } = Resources.Row;

        RowStateEnumeration(BindingRowState rowState) : base()
        {
            BindingRowStateEnumeration source = BindingRowStateEnumeration.Cast(rowState);
            DisplayName = source.DisplayName;
            Name = source.Name;
            Value = source.Value;
        }

        RowStateEnumeration(BindingRowState rowState, Image image) : this(rowState)
        { this.Image = image; }

        static RowStateEnumeration()
        {
            List<RowStateEnumeration> data = new List<RowStateEnumeration>()
            {
                new RowStateEnumeration(BindingRowState.Null,      Resources.Row),
                new RowStateEnumeration(BindingRowState.Detached,  Resources.RowDetached),
                new RowStateEnumeration(BindingRowState.Unchanged, Resources.Row),
                new RowStateEnumeration(BindingRowState.Added,     Resources.RowAdded),
                new RowStateEnumeration(BindingRowState.Deleted,   Resources.RowDeleted),
                new RowStateEnumeration(BindingRowState.Modified,  Resources.RowModified),
                new RowStateEnumeration(BindingRowState.Historic,  Resources.RowHistory),
            };

            BuildDictionary(data);
        }

        /// <summary>
        /// Returns the RowState
        /// </summary>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public static BindingRowState GetRowState(params BindingSource[] bindings)
        {
            BindingRowState result = BindingRowState.Null;

            foreach (BindingSource item in bindings)
            {
                if (item.Current is IBindingRowState rowState
                    && result is BindingRowState.Null or BindingRowState.Unchanged)
                { result = rowState.RowState().AsBindingRowState(); }

                if (item.Current is ITemporalValue temporal
                    && temporal.IsCurrent == false
                    && result is BindingRowState.Null or BindingRowState.Unchanged)
                { return BindingRowState.Historic; }
            }

            return result;
        }

        /// <summary>
        /// Returns the ToolTip Text for the RowState.
        /// </summary>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public static String GetToolTip(params BindingSource[] bindings)
        {
            StringBuilder result = new StringBuilder();

            foreach (BindingSource item in bindings)
            {
                String stateValue;
                String temporalValue;

                if (item.Current is IBindingRowState rowState)
                {
                    stateValue = String.Format("{0}: {1}",
                        item.Current.GetType().Name,
                        Members[rowState.RowState().AsBindingRowState()].DisplayName);
                }
                else
                {
                    stateValue = String.Format("{0}: {1}",
                        item.Current.GetType().Name,
                        Members[BindingRowState.Null].DisplayName);
                }

                if (item.Current is ITemporalValue temporal)
                {
                    temporalValue = String.Format("{0}", DbModificationEnumeration.Cast(temporal.Modification).DisplayName);

                    if (temporal.ModifiedOn is DateTime modifiedOn)
                    { temporalValue = String.Format("{0}, Modified On {1}", temporalValue, modifiedOn); }

                    if (temporal.ModifiedBy is String modifiedBy)
                    { temporalValue = String.Format("{0}, By {1}", temporalValue, modifiedBy); }
                }
                else
                { temporalValue = String.Empty; }

                if (String.IsNullOrEmpty(temporalValue))
                { result.AppendLine(stateValue); }
                else { result.AppendLine(String.Format("{0}- {1}", stateValue, temporalValue)); }
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns the Image for the RowState
        /// </summary>
        /// <param name="bindings"></param>
        /// <returns></returns>
        public static Image GetImage(params BindingSource[] bindings)
        { return Members[GetRowState(bindings)].Image; }

        /// <summary>
        /// Sets up Binding to the targeted control.
        /// </summary>
        /// <param name="bindings"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void SetBinding(params BindingSource[] bindings)
        {
            foreach (BindingSource item in bindings)
            {
                item.CurrentChanged += Item_CurrentChanged;
                item.DataSourceChanged += Item_DataSourceChanged;
                item.Disposed += Item_Disposed;
            }

            void Item_CurrentChanged(Object? sender, EventArgs e)
            { }

            void Item_DataSourceChanged(Object? sender, EventArgs e)
            { }

            void Item_Disposed(Object? sender, EventArgs e)
            {
                if (sender is BindingSource item && bindings.Contains(item))
                {
                    item.CurrentChanged -= Item_CurrentChanged;
                    item.DataSourceChanged -= Item_DataSourceChanged;
                    item.Disposed -= Item_Disposed;
                }
            }
        }
    }
}
