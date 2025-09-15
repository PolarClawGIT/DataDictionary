using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Enumerations
{
    interface IRowStateEnumeration : IEnumeration<BindingRowState>
    {
        /// <summary>
        /// Image used for the RowState
        /// </summary>
        public Image Image { get; }
    }

    static partial class RowStateExtension
    {

        partial class Enumeration : Enumeration<BindingRowState, Enumeration>,
            IRowStateEnumeration
        {
            /// <inheritdoc/>
            public Image Image { get; init; } = Resources.Row;

            Enumeration(BindingRowState rowState) : base()
            {
                IEnumeration<BindingRowState> source = rowState.GetEnumeration();
                DisplayName = source.DisplayName;
                Name = source.Name;
                Value = source.Value;
            }

            Enumeration(BindingRowState rowState, Image image) : this(rowState)
            { this.Image = image; }

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
                    if (item.Position >= item.Count)
                    { item.Position = 0; }

                    if (item.Position >= 0
                        && item.Current is IBindingRowState rowState
                        && result is BindingRowState.Null or BindingRowState.Unchanged)
                    { result = rowState.RowState().AsBindingRowState(); }

                    if (item.Position >= 0
                        && item.Current is ITemporal temporal
                        && temporal.Temporal.IsCurrent == false
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
            static String GetToolTip(params BindingSource[] bindings)
            {
                StringBuilder result = new StringBuilder();

                foreach (BindingSource item in bindings)
                {
                    String stateValue;
                    String temporalValue;

                    if (item.Position >= 0 && item.Current is IBindingRowState rowState)
                    {
                        stateValue = String.Format("{0}: {1}",
                            item.Current.GetType().Name,
                            EnumerationValues[rowState.RowState().AsBindingRowState()].DisplayName);
                    }
                    else { stateValue = String.Empty; }

                    if (item.Position >= 0 && item.Current is ITemporal temporal)
                    { temporalValue = temporal.Temporal.ToString() ?? String.Empty; }
                    else
                    { temporalValue = String.Empty; }

                    if (String.IsNullOrEmpty(stateValue)) { }
                    else if (String.IsNullOrEmpty(temporalValue))
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
            static Image GetImage(params BindingSource[] bindings)
            { return EnumerationValues[GetRowState(bindings)].Image; }

            //TODO: Modify forms that have multiple binding sources to pass the them.

            /// <summary>
            /// Sets up the Binding events for the BindingSources.
            /// </summary>
            /// <param name="setImage"></param>
            /// <param name="setToolTip"></param>
            /// <param name="setRowState"></param>
            /// <param name="bindings"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public static void SetBinding(
                Action<Image> setImage,
                Action<String> setToolTip,
                Action<DataRowState> setRowState,
                params BindingSource[] bindings)
            {
                if (bindings.Length == 0)
                { throw new ArgumentNullException(nameof(bindings), "Must have at least one BindingSource"); }

                BindingSource primary = bindings[0];
                IBindingRowState? primaryRowState = null;

                primary.CurrentChanged += Primary_CurrentChanged;
                primary.Disposed += Primary_Disposed;

                if (primary.Current is IBindingRowState value)
                {
                    setRowState(value.RowState());
                    primaryRowState = value;
                    primaryRowState.RowStateChanged += Primary_RowStateChanged;
                }

                foreach (BindingSource item in bindings)
                {
                    item.CurrentChanged += Item_CurrentChanged;
                    item.DataSourceChanged += Item_DataSourceChanged;
                    item.Disposed += Item_Disposed;
                }

                void Primary_CurrentChanged(Object? sender, EventArgs e)
                {
                    if (primaryRowState is IBindingRowState)
                    { primaryRowState.RowStateChanged -= Primary_RowStateChanged; }

                    if (primary.Current is IBindingRowState value)
                    {
                        value.RowStateChanged += Primary_RowStateChanged;
                        setRowState(value.RowState());
                        primaryRowState = value;
                    }
                }

                void Primary_RowStateChanged(Object? sender, RowStateEventArgs e)
                {
                    setRowState(e.RowState);
                    setImage(GetImage(bindings));
                    setToolTip(GetToolTip(bindings));
                }

                void Primary_Disposed(Object? sender, EventArgs e)
                {
                    primary.CurrentChanged -= Primary_CurrentChanged;
                    primary.Disposed -= Primary_Disposed;
                }

                void Item_CurrentChanged(Object? sender, EventArgs e)
                {
                    setImage(GetImage(bindings));
                    setToolTip(GetToolTip(bindings));
                }

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
}
