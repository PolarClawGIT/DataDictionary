using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {
        /// <summary>
        /// Specialized constructor needed to handle PropertyInfo.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="property"></param>
        private XmlBuilder(ScopeType scope, PropertyInfo property) : this(scope)
        {
            BuilderPath = new XmlBuilderIndex(scope, property.Name);
            RenderValueAs = NodeRenderAsType.ElementText;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        /// <summary>
        /// Specialized GetValue function that using reflection.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private String? GetValueDelegate(Object value, PropertyInfo property)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); }
            else { return null; }
        }

        /// <summary>
        /// Sub-Type of the XmlBuilder intended to handle the Properties of an Class.
        /// Uses reflection to get the PropertyInfo.
        /// </summary>
        public class ValueType : XmlBuilder
        {
            //TODO: This is messy. How do I get rid of this? What value does it add?

            /// <summary>
            /// List of Child Properties of the Class and the builders to go with them.
            /// </summary>
            public Dictionary<PropertyInfo, XmlBuilder> Properties = new Dictionary<PropertyInfo, XmlBuilder>();

            /// <summary>
            /// Specialized constructor for handling generic classes.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="scope"></param>
            /// <param name="properties"></param>
            public ValueType(Type source, ScopeType scope, params IEnumerable<String> properties) : base(scope)
            {
                //GetValue = (value) => GetValueDelegate((dynamic)value);

                foreach (PropertyInfo item in source.GetProperties().
                    Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item)
                    {
                        ObjectProperty = item.Name,
                        RenderType = TryConvert(item, out XmlTypeCode? xmlValue) ? xmlValue.Value : XmlTypeCode.None,
                        ObjectType = TryConvert(item, out ObjectValueType? objectValue) ? objectValue.Value : ObjectValueType.Null
                    };

                    Properties.Add(item, child);
                }
            }

            /// <inheritdoc cref="XmlBuilder(XmlBuilder)"/>
            public ValueType(ValueType source) : base(source)
            {
                foreach (var item in source.Properties)
                { Properties.Add(item.Key, new XmlBuilder(item.Value)); }
            }

            /// <inheritdoc/>
            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element)
                {
                    foreach (var item in Properties.Values)
                    { element.Add(item.Build(value)); }

                    return result;
                }
                else if (result is null) { return result; }
                else
                {
                    Exception ex = new InvalidOperationException("XmlBuilder.Build returned something other then an XElement");
                    ex.Data.Add(nameof(base.Build), result.GetType().Name);
                    throw ex;
                }
            }

            /// <summary>
            /// Try to Convert the PropertyInfo into an XmlTypeCode.
            /// </summary>
            /// <param name="type"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public virtual Boolean TryConvert(PropertyInfo type, [NotNullWhen(true)] out XmlTypeCode? result)
            {
                result = null;

                // I included but the .Net name and the C# name of the Type.
                // This is mostly for clarity and to remind myself that these things are the same.
                // Based on: https://learn.microsoft.com/en-us/dotnet/standard/data/xml/mapping-xml-data-types-to-clr-types

                switch (type)
                {
                    case PropertyInfo value when // Should never occur
                        value.PropertyType == null:
                        result = XmlTypeCode.None; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(String)
                        || value.PropertyType == typeof(string):
                        result = XmlTypeCode.String; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(Boolean)
                        || value.PropertyType == typeof(Nullable<Boolean>)
                        || value.PropertyType == typeof(bool)
                        || value.PropertyType == typeof(Nullable<bool>):
                        result = XmlTypeCode.Boolean; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(Decimal)
                        || value.PropertyType == typeof(Nullable<Decimal>):
                        result = XmlTypeCode.Decimal; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(float)
                        || value.PropertyType == typeof(Nullable<float>)
                        || value.PropertyType == typeof(Single)
                        || value.PropertyType == typeof(Nullable<Single>):
                        result = XmlTypeCode.Float; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(double)
                        || value.PropertyType == typeof(Nullable<double>)
                        || value.PropertyType == typeof(Double)
                        || value.PropertyType == typeof(Nullable<Double>):
                        result = XmlTypeCode.Double; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(TimeSpan)
                        || value.PropertyType == typeof(Nullable<TimeSpan>):
                        result = XmlTypeCode.Duration; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(DateTime)
                        || value.PropertyType == typeof(Nullable<DateTime>):
                        result = XmlTypeCode.DateTime; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(TimeOnly)
                        || value.PropertyType == typeof(Nullable<TimeOnly>):
                        result = XmlTypeCode.Time; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(DateOnly)
                        || value.PropertyType == typeof(Nullable<DateOnly>):
                        result = XmlTypeCode.Date; break;

                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.GYearMonth; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.GYear; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.GMonthDay; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.GDay; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.GMonth; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.HexBinary; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Base64Binary; break;

                    case PropertyInfo value when
                        value.PropertyType == typeof(System.Uri):
                        result = XmlTypeCode.AnyUri; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(System.Xml.XmlQualifiedName):
                        result = XmlTypeCode.QName; break;

                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Notation; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NormalizedString; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Token; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Language; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NmToken; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Name; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NCName; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Id; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Idref; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.Entity; break;

                    case PropertyInfo value when // Unhandled Integers
                        value.PropertyType == typeof(Int128)
                        || value.PropertyType == typeof(Nullable<Int128>):
                        result = XmlTypeCode.Integer; break;

                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NonPositiveInteger; break;
                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NegativeInteger; break;

                    case PropertyInfo value when
                        value.PropertyType == typeof(long)
                        || value.PropertyType == typeof(Nullable<long>)
                        || value.PropertyType == typeof(Int64)
                        || value.PropertyType == typeof(Nullable<Int64>):
                        result = XmlTypeCode.Long; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(int)
                        || value.PropertyType == typeof(Nullable<int>)
                        || value.PropertyType == typeof(Int32)
                        || value.PropertyType == typeof(Nullable<Int32>):
                        result = XmlTypeCode.Int; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(short)
                        || value.PropertyType == typeof(Nullable<short>)
                        || value.PropertyType == typeof(Int16)
                        || value.PropertyType == typeof(Nullable<Int16>):
                        result = XmlTypeCode.Short; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(sbyte)
                        || value.PropertyType == typeof(Nullable<sbyte>):
                        result = XmlTypeCode.Byte; break;

                    case PropertyInfo value when// Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.NonNegativeInteger; break;

                    case PropertyInfo value when
                        value.PropertyType == typeof(UInt64)
                        || value.PropertyType == typeof(Nullable<UInt64>):
                        result = XmlTypeCode.UnsignedLong; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(UInt32)
                        || value.PropertyType == typeof(Nullable<UInt32>):
                        result = XmlTypeCode.UnsignedInt; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(UInt16)
                        || value.PropertyType == typeof(Nullable<UInt16>):
                        result = XmlTypeCode.UnsignedShort; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(byte)
                        || value.PropertyType == typeof(Nullable<byte>):
                        result = XmlTypeCode.UnsignedByte; break;

                    case PropertyInfo value when // Never occurs, no mapping
                        value.PropertyType == null:
                        result = XmlTypeCode.PositiveInteger; break;

                    default:
                        break;
                }

                if (result is null)
                { return false; }
                else { return true; }
            }

            /// <summary>
            /// Try to Convert the PropertyInfo into an ObjectValueType.
            /// </summary>
            /// <param name="type"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            public virtual Boolean TryConvert(PropertyInfo type, [NotNullWhen(true)] out ObjectValueType? result)
            {
                result = null;

                // I included but the .Net name and the C# name of the Type.
                // This is mostly for clarity and to remind myself that these things are the same.

                switch (type)
                {
                    case PropertyInfo value when // Should never occur
                        value.PropertyType == null:
                        result = ObjectValueType.Null; break;

                    // Special Cases
                    case PropertyInfo value when
                        value.PropertyType == typeof(PathIndex):
                        result = ObjectValueType.NameSpace; break;

                    // General cases
                    case PropertyInfo value when
                        value.PropertyType == typeof(String)
                        || value.PropertyType == typeof(string):
                        result = ObjectValueType.String; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(Boolean)
                        || value.PropertyType == typeof(Nullable<Boolean>)
                        || value.PropertyType == typeof(bool)
                        || value.PropertyType == typeof(Nullable<bool>):
                        result = ObjectValueType.Boolean; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(Decimal)
                        || value.PropertyType == typeof(Nullable<Decimal>)
                        || value.PropertyType == typeof(float)
                        || value.PropertyType == typeof(Nullable<float>)
                        || value.PropertyType == typeof(Single)
                        || value.PropertyType == typeof(Nullable<Single>)
                        || value.PropertyType == typeof(double)
                        || value.PropertyType == typeof(Nullable<double>)
                        || value.PropertyType == typeof(Double)
                        || value.PropertyType == typeof(Nullable<Double>)
                        || value.PropertyType == typeof(Int128)
                        || value.PropertyType == typeof(Nullable<Int128>)
                        || value.PropertyType == typeof(long)
                        || value.PropertyType == typeof(Nullable<long>)
                        || value.PropertyType == typeof(Int64)
                        || value.PropertyType == typeof(Nullable<Int64>)
                        || value.PropertyType == typeof(int)
                        || value.PropertyType == typeof(Nullable<int>)
                        || value.PropertyType == typeof(Int32)
                        || value.PropertyType == typeof(Nullable<Int32>)
                        || value.PropertyType == typeof(short)
                        || value.PropertyType == typeof(Nullable<short>)
                        || value.PropertyType == typeof(Int16)
                        || value.PropertyType == typeof(Nullable<Int16>)
                        || value.PropertyType == typeof(sbyte)
                        || value.PropertyType == typeof(Nullable<sbyte>)
                        || value.PropertyType == typeof(UInt64)
                        || value.PropertyType == typeof(Nullable<UInt64>)
                        || value.PropertyType == typeof(UInt32)
                        || value.PropertyType == typeof(Nullable<UInt32>)
                        || value.PropertyType == typeof(UInt16)
                        || value.PropertyType == typeof(Nullable<UInt16>)
                        || value.PropertyType == typeof(byte)
                        || value.PropertyType == typeof(Nullable<byte>):
                        result = ObjectValueType.Numeric; break;
                    case PropertyInfo value when
                        value.PropertyType == typeof(Guid)
                        || value.PropertyType == typeof(Nullable<Guid>):
                        result = ObjectValueType.GUID; break;
                    case PropertyInfo value when
                        value.PropertyType.IsEnum:
                        result = ObjectValueType.Enumeration; break;
                    case PropertyInfo value when
                        value.PropertyType.IsClass:
                        result = ObjectValueType.Class; break;
                    case PropertyInfo value when
                        value.PropertyType.IsInterface:
                        result = ObjectValueType.Interface; break;
                    default:
                        break;
                }

                if (result is null)
                { return false; }
                else { return true; }

            }
        }
    }
}
