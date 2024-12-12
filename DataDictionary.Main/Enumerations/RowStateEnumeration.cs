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

                if (item.Current is IBindingRowState rowState)
                {
                    stateValue = String.Format("{0}: {1}",
                        item.Current.GetType().Name,
                        Members[rowState.RowState().AsBindingRowState()].DisplayName);
                }
                else { stateValue = String.Empty; }

                if (item.Current is ITemporalValue temporal)
                {
                    temporalValue = String.Format("{0}", DbModificationEnumeration.Cast(temporal.Temporal.Modification).DisplayName);

                    if (temporal.Temporal.IsCurrent == false)
                    { temporalValue = String.Format("{0}/Historic", temporalValue); }

                    if (temporal.Temporal.CreatedOn is DateTime createdOn && temporal.Temporal.IsDeleted == false)
                    { temporalValue = String.Format("{0} on {1}", temporalValue, createdOn); }

                    if (temporal.Temporal.CreatedOn is DateTime removedOn && temporal.Temporal.IsDeleted == true)
                    { temporalValue = String.Format("{0} on {1}", temporalValue, removedOn); }

                    if (temporal.Temporal.CreatedBy is String createdBy && temporal.Temporal.IsDeleted == false)
                    { temporalValue = String.Format("{0} by {1}", temporalValue, createdBy); }

                    if (temporal.Temporal.CreatedBy is String removedBy && temporal.Temporal.IsDeleted == true)
                    { temporalValue = String.Format("{0} by {1}", temporalValue, removedBy); }
                }
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
        { return Members[GetRowState(bindings)].Image; }

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
