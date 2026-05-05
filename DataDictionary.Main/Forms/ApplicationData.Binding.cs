using DataDictionary.BusinessLayer;
using DataDictionary.Main.Controls;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {   // POC: build a Binding Helper using the pattern used by the application

        /// <summary>
        /// DataBinding Helper.
        /// </summary>
        protected abstract class DataBinding
        {
            /// <summary>
            /// Command that performs the DoWork function.
            /// </summary>
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            /// <summary>
            /// Gets the Authorization of a Button passed.
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            public abstract Boolean GetAuthorization(Enumerations.ButtonType command);

            /// <summary>
            /// Gets if the form should be Locked.
            /// </summary>
            /// <returns></returns>
            public abstract Boolean GetLocked();
        }

        /// <summary>
        /// DataBinding Helper.
        /// </summary>
        /// <typeparam name="TRow"></typeparam>
        protected class DataBinding<TRow>
            where TRow : class, IBindingPropertyChanged, IBindingRowState
        {
            /// <summary>
            /// BindingSource used by this class
            /// </summary>
            public BindingSource BindingData { get; private set; }

            /// <summary>
            /// Function called during Load to get the data.
            /// </summary>
            /// <remarks>Use LoadBinding to reset the data.</remarks>
            public Func<IBindingData<TRow>> GetData { get; set; }

            /// <summary>
            /// Backing field for the data.
            /// Initialized as an empty BindingView not associated with any external entity.
            /// </summary>
            BindingView<TRow> bindingValues = new BindingView<TRow>(new List<TRow>());

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="binding">Binding Source linked with this data.</param>
            /// <param name="getData">Functions used called to get data.</param>
            /// <remarks>The getData called but filtered to no rows. Allows AddValue to work.</remarks>
            public DataBinding(BindingSource binding, Func<IBindingData<TRow>> getData)
            {
                GetData = getData;
                bindingValues = new BindingView<TRow>(getData(), w => 1 == 2);
                BindingData = binding;
                BindingData.DataSource = bindingValues;

                LoadBindingStart += OnLoadBindingStart;
                LoadBindingComplete += OnLoadBindingComplete;
                binding.Disposed += Binding_Disposed;

                void Binding_Disposed(Object? sender, EventArgs e)
                {
                    LoadBindingStart -= OnLoadBindingStart;
                    LoadBindingComplete -= OnLoadBindingComplete;
                    binding.Disposed -= Binding_Disposed;
                }
            }

            /// <summary>
            /// Try/Get to find the Value that is the Current Value for the BindingSource.
            /// </summary>
            /// <param name="result"></param>
            /// <returns></returns>
            public virtual Boolean TryGetValue([NotNullWhen(true)] out TRow? result)
            {
                if (BindingData.Position >= 0
                    && BindingData.Current is TRow value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            /// <summary>
            /// Add a Value to the Binding Values.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            /// <remarks>Uses the BindingSource backing field.</remarks>
            public virtual void AddValue(TRow value)
            { bindingValues.Add(value); }

            [Obsolete("not needed?", true)]
            public virtual void StopBinding()
            {
                BindingData.RaiseListChangedEvents = false;
                bindingValues.RaiseListChangedEvents = false;
            }

            /// <summary>
            /// Event occurs as part of LoadBinding, before the Data is connected to the Binding Source.
            /// </summary>
            /// <remarks>Stops RaiseListChangedEvents</remarks>
            public event EventHandler LoadBindingStart;

            /// <summary>
            /// Event occurs as part of the LoadBinding, after the Data is connected.
            /// </summary>
            /// <remarks>Starts RaiseListChangedEvents</remarks>
            public event EventHandler LoadBindingComplete;

            /// <summary>
            /// Part of the Load process linking to the data values and restarts binding.
            /// </summary>
            /// <remarks>Call StopBinding First.</remarks>
            public virtual void LoadBinding(Func<TRow, Boolean>? filter)
            {
                if (LoadBindingStart is EventHandler startHandler)
                { startHandler(this, new EventArgs()); }

                bindingValues = new BindingView<TRow>(GetData(), filter);
                BindingData.DataSource = bindingValues;

                if (LoadBindingComplete is EventHandler completeHandler)
                { completeHandler(this, new EventArgs()); }
            }

            private void OnLoadBindingStart(Object? sender, EventArgs e)
            {
                BindingData.RaiseListChangedEvents = false;
                bindingValues.RaiseListChangedEvents = false;
            }

            private void OnLoadBindingComplete(Object? sender, EventArgs e)
            {
                BindingData.RaiseListChangedEvents = true;
                bindingValues.RaiseListChangedEvents = true;

                BindingData.ResetBindings(false);
                BindingData.MoveFirst();
            }

            /// <summary>
            /// Helper Method to Bind a Control to a DataField
            /// </summary>
            /// <param name="controlField"></param>
            /// <param name="dataField"></param>
            /// <param name="nullValue"></param>
            /// <returns></returns>
            protected virtual Binding CreateBinding(String controlField, String dataField, Object? nullValue = null)
            {
                if (BindingData.DataSource is null)
                { throw new ArgumentNullException(nameof(BindingData.DataSource), "DataSource is Null. Binding has not been loaded"); }

                if (BindingData.GetItemProperties(null).Find(dataField, false) is PropertyDescriptor bindField)
                {
                    return new Binding(controlField, BindingData, bindField.Name)
                    { DataSourceNullValue = nullValue }
                        ;
                }
                else
                {
                    Exception ex = new ArgumentException("Data Field Not Found", dataField);

                    foreach (PropertyDescriptor item in BindingData.GetItemProperties(null).OfType<PropertyDescriptor>())
                    { ex.Data.Add(item.Name, item.PropertyType); }

                    throw ex;
                }
            }

            /// <summary>
            /// Helper Method that binds a data field to a Control
            /// </summary>
            /// <param name="formControl"></param>
            /// <param name="dataField"></param>
            /// <remarks>
            /// This covers common scenarios for consistent binding setups.<br/>
            /// Common exceptions are also caught to help identify issues.
            /// </remarks>
            public virtual void AddBinding(Control formControl, String dataField)
            { formControl.DataBindings.Add(CreateBinding(nameof(Control.Text), dataField)); }

            /// <inheritdoc cref="AddBinding(Control, string)"/>
            public virtual void AddBinding(TextBox textBoxControl, String dataField)
            { textBoxControl.DataBindings.Add(CreateBinding(nameof(TextBox.Text), dataField)); }

            /// <inheritdoc cref="AddBinding(Control, string)"/>
            public virtual void AddBinding(TextBoxData textBoxControl, String dataField)
            { textBoxControl.DataBindings.Add(CreateBinding(nameof(TextBox.Text), dataField)); }

            /// <inheritdoc cref="AddBinding(Control, string)"/>
            public virtual void AddBinding(ComboBox comboBoxControl, String dataField)
            { comboBoxControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), dataField)); }

            /// <inheritdoc cref="AddBinding(Control, string)"/>
            public virtual void AddBinding(ComboBoxData comboBoxControl, String dataField, Object nullValue)
            { comboBoxControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), dataField, nullValue)); }

            /// <inheritdoc cref="AddBinding(Control, string)"/>
            public virtual void AddBinding(CheckBox checkBoxControl, String dataField)
            {
                if (BindingData.GetItemProperties(null).Find(dataField, false) is PropertyDescriptor bindField
                    && bindField.PropertyType != typeof(Boolean))
                {
                    Exception ex = new ArgumentException("Data Field not the expected type", dataField);
                    ex.Data.Add(nameof(bindField.PropertyType), bindField.PropertyType);
                    throw ex;
                }
                else
                { checkBoxControl.DataBindings.Add(CreateBinding(nameof(CheckBox.Checked), dataField)); }
            }
        }

    }
}
