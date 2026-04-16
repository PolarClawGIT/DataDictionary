using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the unique Name of a Process.
    /// </summary>
    public interface IProcessKeyName : IKey
    {
        /// <summary>
        /// Title of the Domain Process (aka Name of the Process)
        /// </summary>
        String? ProcessTitle { get; }
    }

    /// <summary>
    /// Implementation for the unique Name of a Process.
    /// </summary>
    public class ProcessKeyName : IProcessKeyName,
        IKeyComparable<IProcessKeyName>, IKeyComparable<ProcessKeyName>
    {
        /// <inheritdoc/>
        public String ProcessTitle { get; init; } = string.Empty;

        /// <inheritdoc/>
        public virtual Boolean HasValue { get { return !String.IsNullOrEmpty(ProcessTitle); } }

        /// <summary>
        /// Constructor for the Process Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ProcessKeyName(IProcessKeyName source) : base()
        {
            if (source.ProcessTitle is string) { ProcessTitle = source.ProcessTitle; }
            else { ProcessTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Process Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ProcessKeyName(ITableKeyName source) : base()
        {
            if (source.TableName is string) { ProcessTitle = source.TableName; }
            else { ProcessTitle = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Process Unique Key.
        /// </summary>
        /// <param name="source"></param>
        public ProcessKeyName(IRoutineKeyName source) : base()
        {
            if (source.RoutineName is string) { ProcessTitle = source.RoutineName; }
            else { ProcessTitle = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ProcessKeyName? other)
        {
            return
                other is ProcessKeyName &&
                !string.IsNullOrEmpty(ProcessTitle) &&
                !string.IsNullOrEmpty(other.ProcessTitle) &&
                ProcessTitle.Equals(other.ProcessTitle, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IProcessKeyName? other)
        { return other is IProcessKeyName value && Equals(new ProcessKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IProcessKeyName value && Equals(new ProcessKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ProcessKeyName? other)
        {
            if (other is ProcessKeyName value)
            { return string.Compare(ProcessTitle, value.ProcessTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IProcessKeyName? other)
        { if (other is IProcessKeyName value) { return CompareTo(new ProcessKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IProcessKeyName value) { return CompareTo(new ProcessKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ProcessKeyName left, ProcessKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ProcessKeyName left, ProcessKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ProcessKeyName left, ProcessKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ProcessKeyName left, ProcessKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ProcessKeyName left, ProcessKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ProcessKeyName left, ProcessKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return ProcessTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (ProcessTitle is string) { return ProcessTitle; }
            else { return string.Empty; }
        }
    }
}
