using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.Main.Controls;
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
            public DataBinding(BindingSource binding, Func<IBindingList<TRow>> getData) : base()
            {
                GetData = getData;
                bindingValues = new BindingView<TRow>(getData(), w => 1 == 2);

                BindingData = binding;
                BindingData.RaiseListChangedEvents = false; // No data so don't call change event during Init.
                BindingData.DataSource = bindingValues;

                LoadBindingStart += OnLoadBindingStart;
                LoadBindingComplete += OnLoadBindingComplete;
                binding.DataError += Binding_DataError;
                binding.Disposed += Binding_Disposed;

                void Binding_Disposed(Object? sender, EventArgs e)
                {
                    LoadBindingStart -= OnLoadBindingStart;
                    LoadBindingComplete -= OnLoadBindingComplete;
                    binding.DataError -= Binding_DataError;
                    binding.Disposed -= Binding_Disposed;
                }

                void Binding_DataError(Object? sender, BindingManagerDataErrorEventArgs e)
                {
                    Exception ex = e.Exception;

                    if(sender is not null)
                    {
                        var senderData = sender.GetType();
                        ex.Data.Add(nameof(senderData.Name), senderData.Name);
                    }

                    if(sender is BindingSource source)
                    {
                        
                    }
                    
                    throw ex;
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
            /// Try to set the Position of the current row matching the Condition past.
            /// </summary>
            /// <param name="condition"></param>
            /// <returns></returns>
            public virtual Boolean TrySetValue(Func<TRow, Boolean> condition)
            {
                //bindingValues.IndexOf()
                IEnumerable<TRow> target = bindingValues.Where(condition);
                if (target.Count() == 1)
                {
                    Int32 position = bindingValues.IndexOf(target.First());
                    if (position >= 0)
                    { BindingData.Position = position; return true; }
                    else { return false; }
                }
                else { return false; }
            }

            /// <inheritdoc cref="BindingSource.ResetCurrentItem"/>
            public void ResetCurrent()
            { BindingData.ResetCurrentItem(); }

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
            /// <typeparam name="TProperty">Data Type of the property of the expression.</typeparam>
            /// <param name="controlField">Property of the Control that is too be bound.</param>
            /// <param name="expression">The Expression that is the name of the property that the control is bound to.</param>
            /// <param name="nullValue">Value that represents Null</param>
            /// <returns>Binding object</returns>
            /// <exception cref="ArgumentNullException">The DataSource of the BindingData is null. Assign a DataSource before calling this method.</exception>
            /// <exception cref="ArgumentException">Other issues with the data binding.</exception>
            /// <remarks>
            /// This method is called by the overloads of AddBinding methods.
            /// Each AddBinding method is wired to handle a specific Control type.
            /// This allows for specialized handling, as needed.
            /// </remarks>
            protected virtual Binding CreateBinding<TProperty>(String controlField, Expression<Func<TRow, TProperty>> expression, Object? nullValue = null)
            {
                if (BindingData.DataSource is null)
                { throw new ArgumentNullException(nameof(BindingData.DataSource), "DataSource is Null. Binding has not been loaded"); }

                IReadOnlyList<String> members = ParseExpression(expression);
                PropertyDescriptorCollection properties = BindingData.GetItemProperties(null);
                Exception? memberException = ValidateProperty<TProperty>(members);

                if (memberException is null)
                {
                    return new Binding(controlField, BindingData, String.Join(".", members))
                    { DataSourceNullValue = nullValue };
                }
                else { throw memberException; }
            }

            /// <summary>
            /// Parses a Expression Tree to return the list of Strings that represents the Path to the Property.
            /// </summary>
            /// <typeparam name="TProperty"></typeparam>
            /// <param name="bindingField"></param>
            /// <returns></returns>
            /// <remarks>
            /// The method parses an Expression tree to get the name of the property to be bound.
            /// This can cause performance issues but the results can be validated against the data.<br/>
            /// </remarks>
            protected virtual IReadOnlyList<String> ParseExpression<TProperty>(Expression<Func<TRow, TProperty>> bindingField)
            {
                // Base code was derived from Google AI search. The original source is unknown.
                //
                // public static class PathHelper
                // {
                //     public static string GetPath<T, TProperty>(Expression<Func<T, TProperty>> expression)
                //     {
                //         var members = new Stack<string>();
                //         var memberExpr = expression.Body as MemberExpression;
                // 
                //         while (memberExpr != null)
                //         {
                //             members.Push(memberExpr.Member.Name);
                //             memberExpr = memberExpr.Expression as MemberExpression;
                //         }
                // 
                //         return string.Join(".", members);
                //     }
                // }
                //
                // T: Becomes TRow as the base type is already known to this class.
                //    This makes the call simpler and reduces the chance that the wrong property is bound.


                List<String> members = new List<string>();
                MemberExpression? memberExpr = bindingField.Body as MemberExpression;
                var properties = BindingData.GetItemProperties(null);

                // Parse the Expression to build the field name to be bound.
                while (memberExpr != null)
                {
                    members.Insert(0, memberExpr.Member.Name);
                    memberExpr = memberExpr.Expression as MemberExpression;
                }

                return members.AsReadOnly();
            }

            /// <summary>
            /// Validate the binding property against the object and return any exceptions if found.
            /// </summary>
            /// <typeparam name="TProperty"></typeparam>
            /// <param name="bindingProperty"></param>
            /// <returns></returns>
            /// <remarks>
            /// This is intended to trap issues with the FindGoodRow method, before that method raises an error.<br/>
            /// The FindGoodRow method (Microsoft code: System.Windows.Forms.CurrencyManager) traps errors but returns very little useful information.
            /// This method catches common issues and throws exceptions during binding rather then when data is retrieved.<br/>
            /// </remarks>
            public virtual Exception? ValidateProperty<TProperty>(IReadOnlyList<String> bindingProperty)
            {
                PropertyDescriptorCollection properties = BindingData.GetItemProperties(null);
                String bindField = String.Join(".", bindingProperty);

                foreach (String fieldName in bindingProperty)
                {
                    // Property Name check
                    if (properties.Find(fieldName, false) is PropertyDescriptor property)
                    {

                        // Property Type Check
                        if (bindingProperty.Count == 0 && property.PropertyType != typeof(TProperty))
                        {
                            Exception ex = new ArgumentException("Type does not match what is expected", bindField);
                            ex.Data.Add(nameof(bindField), bindField);
                            ex.Data.Add(nameof(TRow), typeof(TRow).ToString());
                            ex.Data.Add(nameof(TProperty), typeof(TProperty).ToString());
                            ex.Data.Add(nameof(property.PropertyType), property.PropertyType.ToString());
                            return ex;
                        }

                        properties = property.GetChildProperties();
                    }
                    else
                    {
                        Exception ex = new ArgumentException("Property Not Found", bindField);
                        ex.Data.Add(nameof(bindField), bindField);
                        ex.Data.Add(nameof(TRow), typeof(TRow).ToString());
                        ex.Data.Add(nameof(TProperty), typeof(TProperty).ToString());
                        foreach (PropertyDescriptor item in BindingData.GetItemProperties(null).OfType<PropertyDescriptor>())
                        { ex.Data.Add(item.Name, item.PropertyType); }
                        return ex;
                    }
                }

                return null;
            }

            /// <summary>
            /// Helper Method that binds a data field to a Control
            /// </summary>
            /// <typeparam name="TProperty">Data Type of the property of the expression.</typeparam>
            /// <param name="formControl">Control that is to be Bound</param>
            /// <param name="expression">The LINQ expression that is the name of the property.</param>
            /// <example><![CDATA[dataBinding.AddBinding(control, e => e.PropertyName);]]></example>
            public virtual void AddBinding<TProperty>(
                Control formControl,
                Expression<Func<TRow, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(Control.Text), expression)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                TextBox formControl,
                Expression<Func<TRow, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(TextBox.Text), expression)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                TextBoxData formControl,
                Expression<Func<TRow, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(TextBox.Text), expression)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                ComboBox formControl,
                Expression<Func<TRow, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), expression)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                ComboBoxData formControl,
                Expression<Func<TRow, TProperty>> expression)
            { formControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), expression)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                ComboBoxData formControl,
                Expression<Func<TRow, TProperty>> expression,
                TProperty nullValue)
            { formControl.DataBindings.Add(CreateBinding(nameof(ComboBox.SelectedValue), expression, nullValue)); }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                CheckBox formControl,
                Expression<Func<TRow, TProperty>> expression)
            {
                Type propertyType = typeof(TProperty);

                if (propertyType == typeof(Boolean))
                { formControl.DataBindings.Add(CreateBinding(nameof(CheckBox.Checked), expression)); }
                else
                {
                    Exception ex = new ArgumentException("Checkbox TProperty needs to be of Type Boolean");
                    ex.Data.Add(nameof(TRow), typeof(TRow).ToString());
                    ex.Data.Add(nameof(TProperty), typeof(TProperty).ToString());
                    throw ex;
                }
            }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            public virtual void AddBinding<TProperty>(
                RadioButton formControl,
                Expression<Func<TRow, TProperty>> expression)
            {
                Type propertyType = typeof(TProperty);

                if (propertyType == typeof(Boolean))
                { formControl.DataBindings.Add(CreateBinding(nameof(CheckBox.Checked), expression)); }
                else
                {
                    Exception ex = new ArgumentException("RadioButton TProperty needs to be of Type Boolean");
                    ex.Data.Add(nameof(TRow), typeof(TRow).ToString());
                    ex.Data.Add(nameof(TProperty), typeof(TProperty).ToString());
                    throw ex;
                }
            }

            /// <inheritdoc cref="AddBinding{TProperty}(Control, Expression{Func{TRow, TProperty}})"/>
            /// <remarks>Specialized for DataGridViewComboBoxColumn, does not call CreateBinding.</remarks>
            public virtual void AddBinding<TProperty>(
                DataGridViewComboBoxColumn formControl,
                Expression<Func<TRow, TProperty>> expression)
            {
                IReadOnlyList<String> bindingMember = ParseExpression(expression);
                Exception? bindingException = ValidateProperty<TProperty>(bindingMember);

                if (bindingException is null)
                { formControl.DataPropertyName = String.Join(".", bindingMember); }
                else { throw bindingException; }
            }


            /// <summary>
            /// Helper method that loads a ComboBox with the values of Binding DataSource.
            /// </summary>
            /// <typeparam name="TValueMember"></typeparam>
            /// <typeparam name="TDisplayMember"></typeparam>
            /// <param name="setValueMember"></param>
            /// <param name="setDisplayMember"></param>
            /// <param name="setDataSource"></param>
            /// <param name="valueExpression"></param>
            /// <param name="displayExpression"></param>
            /// <exception cref="InvalidOperationException"></exception>
            /// <remarks>Base method for LoadCombBox methods</remarks>
            protected virtual void LoadCombBoxCore<TValueMember, TDisplayMember>(
                Action<String> setValueMember,
                Action<String> setDisplayMember,
                Action<BindingView<TRow>> setDataSource,
                Expression<Func<TRow, TValueMember>> valueExpression,
                Expression<Func<TRow, TDisplayMember>> displayExpression)
            {
                IReadOnlyList<String> valueMember = ParseExpression(valueExpression);
                IReadOnlyList<String> displayMember = ParseExpression(displayExpression);
                Exception? valueException = ValidateProperty<TValueMember>(valueMember);
                Exception? displayException = ValidateProperty<TValueMember>(displayMember);

                if (valueException is null && displayException is null)
                {
                    setValueMember(String.Join(".", valueMember));
                    setDisplayMember(String.Join(".", valueMember));
                    setDataSource(bindingValues);
                }
                else if (valueException is not null) { throw valueException; }
                else if (displayException is not null) { throw displayException; }
                else { throw new InvalidOperationException("Untrap If/Else"); } // This should never happen.
            }

            /// <summary>
            /// Helper method that loads a ComboBox with the values of Binding DataSource.
            /// </summary>
            /// <typeparam name="TValueMember"></typeparam>
            /// <typeparam name="TDisplayMember"></typeparam>
            /// <param name="comboBox"></param>
            /// <param name="valueExpression"></param>
            /// <param name="displayExpression"></param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="InvalidOperationException"></exception>
            public virtual void LoadCombBox<TValueMember, TDisplayMember>(
                ComboBox comboBox,
                Expression<Func<TRow, TValueMember>> valueExpression,
                Expression<Func<TRow, TDisplayMember>> displayExpression)
            {
                LoadCombBoxCore(
                    (v) => comboBox.ValueMember = v,
                    (d) => comboBox.DisplayMember = d,
                    (s) => comboBox.DataSource = s,
                    valueExpression,
                    displayExpression);
            }

            /// <inheritdoc cref="LoadCombBox{TValueMember, TDisplayMember}(ComboBox, Expression{Func{TRow, TValueMember}}, Expression{Func{TRow, TDisplayMember}})"/>
            public virtual void LoadCombBox<TValueMember, TDisplayMember>(
                DataGridViewComboBoxColumn comboBox,
                Expression<Func<TRow, TValueMember>> valueExpression,
                Expression<Func<TRow, TDisplayMember>> displayExpression)
            {
                LoadCombBoxCore(
                    (v) => comboBox.ValueMember = v,
                    (d) => comboBox.DisplayMember = d,
                    (s) => comboBox.DataSource = s,
                    valueExpression,
                    displayExpression);
            }

            /// <inheritdoc cref="LoadCombBox{TValueMember, TDisplayMember}(ComboBox, Expression{Func{TRow, TValueMember}}, Expression{Func{TRow, TDisplayMember}})"/>
            public virtual void LoadCombBox<TValueMember, TDisplayMember>(
                ComboBoxData comboBox,
                Expression<Func<TRow, TValueMember>> valueExpression,
                Expression<Func<TRow, TDisplayMember>> displayExpression)
            {
                LoadCombBoxCore(
                    (v) => comboBox.ValueMember = v,
                    (d) => comboBox.DisplayMember = d,
                    (s) => comboBox.DataSource = s,
                    valueExpression,
                    displayExpression);
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

    }
}
