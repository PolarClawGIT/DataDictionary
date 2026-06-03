using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.DataLayer;
using DataDictionary.Main.Controls;
using DataDictionary.Resource;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        /// <summary>
        /// DataBinding Helper.<br/>
        /// Provides functionality for managing a set of Data used by DataBinding.
        /// </summary>
        /// <typeparam name="TRow"></typeparam>
        protected class DataBinding<TRow> : ICollection<TRow>
            where TRow : class, IBindingPropertyChanged, IBindingRowState
        {
            /// <summary>
            /// Function called during Load to get the data.
            /// </summary>
            /// <remarks>Use LoadBinding to reset the data.</remarks>
            public Func<IBindingList<TRow>> GetData { get; set; }

            /// <summary>
            /// Backing field for the data.
            /// Initialized as an empty BindingView not associated with any external entity.
            /// </summary>
            protected BindingView<TRow> bindingValues = new BindingView<TRow>(new List<TRow>());

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="binding">Binding Source linked with this data.</param>
            /// <param name="getData">Functions used called to get data. Use "() => GetData()" syntax.</param>
            /// <remarks>The getData called but filtered to no rows. Allows AddValue to work.</remarks>
            /// <example><![CDATA[TemplateData = new DataBinding<TemplateValue>(templateBinding, () => GetData());]]></example>
            public DataBinding(BindingSource binding, Func<IBindingList<TRow>> getData)
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

            #region BindingSource Support
            /// <summary>
            /// BindingSource used by this class
            /// </summary>
            protected BindingSource BindingData { get; private set; }

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
            /// Removes the Value that is the Current Value for the BindingSource.
            /// </summary>
            /// <returns></returns>
            public virtual Boolean Remove()
            {
                if (TryGetValue(out TRow? value))
                { return bindingValues.Remove(value); }
                else { return false; }
            }
            #endregion
            #region Security
            /// <summary>
            /// Get if the Current Value should be Locked (ReadOnly).
            /// </summary>
            /// <returns></returns>
            /// <remarks>Use with DataModel{TKey}.GetLocked</remarks>
            public virtual Boolean GetLocked()
            {
                if (TryGetValue(out TRow? value) && value is IBindingRowState rowState)
                { return value.RowState() is DataRowState.Detached or DataRowState.Deleted; }
                else { return true; }
            }

            /// <summary>
            /// Get the Authorization data for the Current Value.
            /// If the Current Value does not support IAuthorization, false is returned.
            /// </summary>
            /// <param name="authorizations"></param>
            /// <returns></returns>
            /// <remarks>Use with DataModel{TKey}.GetAuthorization</remarks>
            public virtual (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
            {
                if (TryGetValue(out TRow? value) && value is IAuthorization authorization)
                { return authorization.GetAuthorization(authorizations); }
                else { return (false, false, false); }
            }
            #endregion
            #region Binding Helpers
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
            public virtual void LoadBinding(Func<TRow, Boolean>? filter = null)
            {
                if (LoadBindingStart is EventHandler startHandler)
                { startHandler(this, new EventArgs()); }

                bindingValues = new BindingView<TRow>(GetData(), filter);
                BindingData.DataSource = bindingValues;

                if (LoadBindingComplete is EventHandler completeHandler)
                { completeHandler(this, new EventArgs()); }
            }

            /// <summary>
            /// Called by LoadBinding as part of the LoadBindingStart event.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected virtual void OnLoadBindingStart(Object? sender, EventArgs e)
            {
                BindingData.RaiseListChangedEvents = false;
                bindingValues.RaiseListChangedEvents = false;
            }

            /// <summary>
            /// Called by LoadBinding as part of the LoadBindingComplete event.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected virtual void OnLoadBindingComplete(Object? sender, EventArgs e)
            {
                BindingData.RaiseListChangedEvents = true;
                bindingValues.RaiseListChangedEvents = true;

                BindingData.ResetBindings(false);
                BindingData.MoveFirst();
            }

            /// <summary>
            /// Helper Method to create the Binding class for a DataField
            /// </summary>
            /// <param name="controlField"></param>
            /// <param name="dataField"></param>
            /// <param name="nullValue"></param>
            /// <returns></returns>
            /// <remarks>
            /// When GetGoodRow executes (Microsoft code) it can throw exceptions but rarely identifies the issue.
            /// This tries to catch some of those issues and provide information to help resolve the actual issue.
            /// </remarks>
            [Obsolete]
            protected virtual Binding CreateBinding(String controlField, String dataField, Object? nullValue = null)
            {
                if (BindingData.DataSource is null)
                { throw new ArgumentNullException(nameof(BindingData.DataSource), "DataSource is Null. Binding has not been loaded"); }

                if (BindingData.GetItemProperties(null).Find(dataField, false) is PropertyDescriptor bindField)
                {
                    return new Binding(controlField, BindingData, bindField.Name)
                    { DataSourceNullValue = nullValue };
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
            /// Helper Method to create the Binding class for a DataField
            /// </summary>
            /// <typeparam name="TClass">The Class Type of the expected BindingData DataSource</typeparam>
            /// <typeparam name="TProperty">Data Type of the property of the expression.</typeparam>
            /// <param name="controlField">Property of the Control that is too be bound</param>
            /// <param name="expression">The Expression that returns the name of the property.</param>
            /// <param name="nullValue">Value that represents Null</param>
            /// <returns>Binding object</returns>
            /// <exception cref="ArgumentNullException">The DataSource of the BindingData is null. Assign a DataSource before calling this method.</exception>
            /// <exception cref="ArgumentException">The property could not be found in the BindingData.</exception>
            protected virtual Binding CreateBinding<TClass, TProperty>(String controlField, Expression<Func<TClass, TProperty>> expression, Object? nullValue = null)
            {
                if (BindingData.DataSource is null)
                { throw new ArgumentNullException(nameof(BindingData.DataSource), "DataSource is Null. Binding has not been loaded"); }

                Stack<String> members = new Stack<string>();
                MemberExpression? memberExpr = expression.Body as MemberExpression;
                var properties = BindingData.GetItemProperties(null);

                while (memberExpr != null)
                {
                    members.Push(memberExpr.Member.Name);
                    memberExpr = memberExpr.Expression as MemberExpression;
                }

                String bindField = String.Join(".", members);

                while (members.Count > 0)
                {
                    String fieldName = members.Pop();

                    // Property Name check
                    if (properties.Find(fieldName, false) is PropertyDescriptor property)
                    {

                        // Property Type Check
                        if (members.Count == 0 && property.PropertyType != typeof(TProperty))
                        {
                            Exception ex = new ArgumentException("Type does not match expected", bindField);
                            ex.Data.Add(nameof(TProperty), typeof(TProperty).ToString());
                            ex.Data.Add(nameof(property.PropertyType), property.PropertyType.ToString());
                            throw ex;
                        }

                        properties = property.GetChildProperties(); 
                    }
                    else
                    {
                        Exception ex = new ArgumentException("Data Field Not Found", bindField);
                        foreach (PropertyDescriptor item in BindingData.GetItemProperties(null).OfType<PropertyDescriptor>())
                        { ex.Data.Add(item.Name, item.PropertyType); }
                        throw ex;
                    }
                }

                return new Binding(controlField, BindingData, bindField)
                { DataSourceNullValue = nullValue };
            }


            /// <summary>
            /// Helper Method that binds a data field to a Control
            /// </summary>
            /// <typeparam name="TClass">The Class Type of the expected BindingData DataSource</typeparam>
            /// <typeparam name="TProperty">Data Type of the property of the expression.</typeparam>
            /// <param name="formControl">Control that is to be Bound</param>
            /// <param name="expression">The Expression that returns the name of the property.</param>
            /// <example><![CDATA[dataBinding.AddBinding<Interface, propertyType>(control, e => e.PropertyName);]]></example>
            public virtual void AddBinding<TClass, TProperty>(Control formControl, Expression<Func<TClass, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(Control.Text), expression)); }

            /// <inheritdoc cref="AddBinding{TClass, TProperty}(Control, Expression{Func{TClass, TProperty}})"/>
            public virtual void AddBinding<TClass, TProperty>(TextBoxData formControl, Expression<Func<TClass, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(TextBox.Text), expression)); }

            /// <inheritdoc cref="AddBinding{TClass, TProperty}(Control, Expression{Func{TClass, TProperty}})"/>
            public virtual void AddBinding<TClass, TProperty>(ComboBoxData comboBoxControl, Expression<Func<TClass, TProperty>> expression)
            { comboBoxControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), expression)); }

            /// <inheritdoc cref="AddBinding{TClass, TProperty}(Control, Expression{Func{TClass, TProperty}})"/>
            public virtual void AddBinding<TClass, TProperty>(ComboBoxData comboBoxControl, Expression<Func<TClass, TProperty>> expression, TProperty nullValue)
            { comboBoxControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), expression, nullValue)); }

            //--------------------------------------------

            /// <summary>
            /// Helper Method that binds a data field to a Control
            /// </summary>
            /// <param name="formControl"></param>
            /// <param name="dataField"></param>
            /// <remarks>
            /// This covers common scenarios for consistent binding setups.<br/>
            /// Common exceptions are also caught to help identify issues.
            /// </remarks>
            [Obsolete]
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
            #endregion
            #region ICollection
            /// <inheritdoc/>
            public void Add(TRow item)
            { ((ICollection<TRow>)bindingValues).Add(item); }

            /// <inheritdoc/>
            public void Clear()
            { ((ICollection<TRow>)bindingValues).Clear(); }

            /// <inheritdoc/>
            public Boolean Contains(TRow item)
            { return ((ICollection<TRow>)bindingValues).Contains(item); }

            /// <inheritdoc/>
            public void CopyTo(TRow[] array, Int32 arrayIndex)
            { ((ICollection<TRow>)bindingValues).CopyTo(array, arrayIndex); }

            /// <inheritdoc/>
            public Boolean Remove(TRow item)
            { return ((ICollection<TRow>)bindingValues).Remove(item); }

            /// <inheritdoc/>
            public IEnumerator<TRow> GetEnumerator()
            { return ((IEnumerable<TRow>)bindingValues).GetEnumerator(); }

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator()
            { return ((IEnumerable)bindingValues).GetEnumerator(); }

            /// <inheritdoc/>
            public Int32 Count => ((ICollection<TRow>)bindingValues).Count;

            /// <inheritdoc/>
            public Boolean IsReadOnly => ((ICollection<TRow>)bindingValues).IsReadOnly;
            #endregion
        }

        [Obsolete("POC, not currently used", true)]
        protected class DataBinding<TKey, TRow> : DataBinding<TRow>
            where TKey : IKey, IEquatable<TKey>
            where TRow : class, IBindingPropertyChanged, IBindingRowState,
                IEquatable<TKey>, IRemoveItem<TKey>
        {
            public DataBinding(BindingSource binding, Func<IBindingList<TRow>> getData) : base(binding, getData)
            { }

            /// <summary>
            /// Try to get the Value by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public virtual Boolean TryGetValue(TKey key, [NotNullWhen(true)] out TRow? result)
            {
                if (bindingValues.FirstOrDefault(w => key.Equals(w)) is TRow value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            /// <summary>
            /// Remove a value by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public virtual Boolean Remove(TKey key)
            {
                if (TryGetValue(key, out TRow? value))
                { return bindingValues.Remove(value); }
                else { return false; }
            }
        }
    }
}
