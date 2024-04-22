using DataDictionary.DataLayer.DatabaseData.Constraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintKey : IDbConstraintKey { }

    /// <inheritdoc/>
    public class ConstraintKey : DbConstraintKey
    {
        /// <inheritdoc cref="DbConstraintKey(IDbConstraintKey)"/>
        public ConstraintKey(IConstraintKey source) : base(source) { }
    }

    /// <inheritdoc/>
    public interface IConstraintKeyName : IDbConstraintKeyName
    { }

    /// <inheritdoc/>
    public class ConstraintKeyName : DbConstraintKeyName, IConstraintKeyName
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintKeyName(IConstraintKeyName source) : base(source) { }
    }
}
