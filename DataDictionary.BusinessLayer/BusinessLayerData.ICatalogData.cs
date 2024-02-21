using DataDictionary.BusinessLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Catalog (database) Data
        /// </summary>
        public IDatabaseData DatabaseData { get { return database; } }
        private readonly DatabaseData database = new DatabaseData();

    }
}
