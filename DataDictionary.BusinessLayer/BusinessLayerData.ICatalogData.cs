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
        public IDatabaseModel DatabaseModel { get { return database; } }
        private readonly DatabaseModel database = new DatabaseModel();

    }
}
