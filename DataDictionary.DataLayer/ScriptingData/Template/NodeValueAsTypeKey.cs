using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for Scripting NodeValueAs Key.
    /// </summary>
    public interface INodeValueAsTypeKey : IKey
    {
        /// <summary>
        /// How the Value of the Node is to be rendered.
        /// </summary>
        TemplateNodeValueAsType NodeValueAs { get; }
    }

    /// <summary>
    /// Implementation for Node Value As Key.
    /// </summary>
    public class NodeValueAsTypeKey : INodeValueAsTypeKey, IKeyComparable<INodeValueAsTypeKey>, IParsable<NodeValueAsTypeKey>
    {
        /// <inheritdoc/>
        public TemplateNodeValueAsType NodeValueAs { get; init; } = TemplateNodeValueAsType.none;

        /// <summary>
        /// Basic Constructor for the Script As Key.
        /// </summary>
        protected NodeValueAsTypeKey() : base() { }

        /// <summary>
        /// Constructor for the Script As Key.
        /// </summary>
        /// <param name="source"></param>
        public NodeValueAsTypeKey(TemplateNodeValueAsType source) : this()
        { NodeValueAs = source; }

        /// <summary>
        /// Constructor for the Script As Key.
        /// </summary>
        /// <param name="source"></param>
        public NodeValueAsTypeKey(INodeValueAsTypeKey source) : this()
        { if (source is INodeValueAsTypeKey) { NodeValueAs = source.NodeValueAs; } }

        /// <summary>
        /// Converts a NodeValueAsType into a NodeValueAsTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator NodeValueAsTypeKey(TemplateNodeValueAsType source) { return new NodeValueAsTypeKey(source); }

        /// <summary>
        /// Converts a NodeValueAsTypeKey into a NodeValueAsType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator TemplateNodeValueAsType(NodeValueAsTypeKey source) { return source.NodeValueAs; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(INodeValueAsTypeKey? other)
        {
            return other is INodeValueAsTypeKey
                && this.NodeValueAs != TemplateNodeValueAsType.none
                && other.NodeValueAs != TemplateNodeValueAsType.none
                && this.NodeValueAs.Equals(other.NodeValueAs);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(INodeValueAsTypeKey? other)
        {
            if (other is INodeValueAsTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is INodeValueAsTypeKey value) { return CompareTo(new NodeValueAsTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INodeValueAsTypeKey value && this.Equals(new NodeValueAsTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NodeValueAsTypeKey left, NodeValueAsTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return NodeValueAs.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the NodeValueAsType Enum to what the Database uses as TableType Name.
        /// </summary>
        static Dictionary<TemplateNodeValueAsType, String> parseName = new Dictionary<TemplateNodeValueAsType, String>()
        {
            { TemplateNodeValueAsType.ElementText,    "Element.Text"},
            { TemplateNodeValueAsType.ElementCData,   "Element.CData"},
            { TemplateNodeValueAsType.ElementXML,     "Element.XML"},
            { TemplateNodeValueAsType.Attribute,      "Attribute"},
        };

        /// <inheritdoc/>
        public static NodeValueAsTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (NodeValueAsTypeKey.TryParse(source, provider, out NodeValueAsTypeKey? result))
            { return result; }
            else
            { return TemplateNodeValueAsType.none; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out NodeValueAsTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out NodeValueAsTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<TemplateNodeValueAsType, String> dbItem
                && dbItem.Key != TemplateNodeValueAsType.none)
            { result = new NodeValueAsTypeKey() { NodeValueAs = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of NodeValueAsTypeKey for each NodeValueAsType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<NodeValueAsTypeKey> Items()
        { return Enum.GetValues(typeof(TemplateNodeValueAsType)).Cast<TemplateNodeValueAsType>().Select(s => new NodeValueAsTypeKey() { NodeValueAs = s }); }

        /// <summary>
        /// Returns the TableType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(NodeValueAs)) { return parseName[NodeValueAs]; }
            else { return NodeValueAs.ToString(); }
        }
    }

}
