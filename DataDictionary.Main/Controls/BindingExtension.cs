namespace DataDictionary.Main.Controls
{
    static class BindingExtension
    {
        /// <summary>
        /// Adds to the Binding object Parsing and Formating functions to deal with complex data types.
        /// </summary>
        /// <typeparam name="TData">The native type of the data.</typeparam>
        /// <typeparam name="TControl">The type the Control wants.</typeparam>
        /// <param name="binding"></param>
        /// <param name="toControlType">Function that converts the TData to the TControl.</param>
        /// <param name="toBaseType">Function that converts the TControl to a TData.</param>
        /// <returns></returns>
        /// <remarks>
        /// This was built to handle types that Binding does not handle "as is" for two way binding.
        /// Binding will general handle strings, integers and other simple well known types.
        /// It does not handle custom types.
        /// The key is that there must be a why of converting the custom type
        /// to a simple type (usally a String) and back again.
        /// The general case will be that toControlType and toBaseType are static or Linq functions.
        /// Any exception handling should be resolved in the function.
        /// </remarks>
        /// <example><![CDATA[
        /// textBoxControl.DataBindings.Add(
        ///             new Binding(nameof(TextBox.Text),
        ///             bindingSource,
        ///             nameof(propertyName))
        ///             .WithParse<propertyType, String>(
        ///                 (p) => propertyType.Formater(p),
        ///                 (s) => propertyType.Parser(s)));]]>
        ///</example>
        public static Binding WithParse<TData, TControl>(this Binding binding, Func<TData, TControl>  toControlType, Func<TControl, TData> toBaseType)
        {
            binding.Format += Binding_Format;
            binding.Parse += Binding_Parse;
            binding.FormattingEnabled = true;

            return binding;

            void Binding_Parse(Object? sender, ConvertEventArgs e)
            {
                if (e.DesiredType is Type
                    && e.DesiredType.Equals(typeof(TData))
                    && e.Value is TControl value)
                { e.Value = toBaseType(value); }
                else
                {
                    Exception ex = new InvalidCastException(
                        String.Format("Could not cast {0} to {1}.", typeof(TControl).Name, typeof(TData).Name));
                    ex.Data.Add(typeof(TControl).Name, e.Value);
                    throw ex;
                }
            }

            void Binding_Format(Object? sender, ConvertEventArgs e)
            {
                if (e.DesiredType is Type
                    && e.DesiredType.Equals(typeof(TControl))
                    && e.Value is TData value)
                { e.Value = toControlType(value); }
                else
                {
                    Exception ex = new InvalidCastException(
                        String.Format("Could not cast {0} to {1}.", typeof(TData).Name, typeof(TControl).Name));
                    ex.Data.Add(typeof(TData).Name, e.Value);
                    throw ex;
                }
            }
        }
    }
}
