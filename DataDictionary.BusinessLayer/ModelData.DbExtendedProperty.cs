using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbExtendedPropertyKeyName source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableColumnKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbTableKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public BindingView<DbExtendedPropertyItem> GetExtendedProperty(IDbRoutineKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return new BindingView<DbExtendedPropertyItem>(DbExtendedProperties, w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbRoutineParameterKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbConstraintKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets a list of Extended Properties given a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DbExtendedPropertyItem> GetExtendedProperty(IDbSchemaKey source)
        {
            DbExtendedPropertyKeyName key = new DbExtendedPropertyKeyName(source);
            return DbExtendedProperties.Where(w => key.Equals(w));
        }
    }
}
