using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessAliasValue : IProcessAliasItem,
        IProcessIndex, IAliasIndex, IAliasSubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public class ProcessAliasValue : ProcessAliasItem, IProcessAliasValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcessAlias; } }

        /// <inheritdoc/>
        public new PathIndex AliasPath
        {
            get
            {
                // Changing the property in the base class is not always caught by the OnPropertyChanged.
                // Extra code is needed to check if the data has changed and update the backing field.  
                if (!aliasPathValue.MemberFullPath.Equals(base.AliasPath))
                { aliasPathValue = new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }

                return aliasPathValue;
            }
            set
            {
                base.AliasPath = value.MemberFullPath;
                aliasPathValue.Set(value);
                OnPropertyChanged(nameof(base.AliasPath));
            }
        }
        PathIndex aliasPathValue = new PathIndex();

        /// <inheritdoc/>
        public ProcessAliasValue() : base() { }

        /// <inheritdoc cref="ProcessAliasItem(IProcessKey)"/>
        public ProcessAliasValue(IProcessIndex key) : base(key) { }

        /// <summary>
        /// Create an Process Alias form Process and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public ProcessAliasValue(IProcessIndex key, AliasIndex alias) : base(key)
        {
            base.AliasPath = alias.AliasPath;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal ProcessAliasValue(IProcessKey key) : base(key) { }

        /// <inheritdoc cref="ProcessAliasItem.AliasPath"/>
        public PathIndex AliasName
        {
            get { return new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }
            set { base.AliasPath = value.MemberFullPath; }
        }


    }
}
