using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        /// <summary>
        /// Gets a list of Routine Parameters given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public BindingView<DbRoutineParameterItem> GetRoutineParameters(IDbRoutineKey source)
        {
            DbRoutineKey key = new DbRoutineKey(source);
            return new BindingView<DbRoutineParameterItem>(DbRoutineParameters, w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Routine Parameters given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public BindingView<DbRoutineDependencyItem> GetRoutineDependencies(IDbRoutineKey source)
        {
            DbRoutineKey key = new DbRoutineKey(source);
            return new BindingView<DbRoutineDependencyItem>(DbRoutineDependencies, w => key.Equals(w));
        }
    }
}
