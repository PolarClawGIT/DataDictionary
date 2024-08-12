using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    public enum DbConstraintType
    {
        Null,
        Check,
        Unique,
        PrimaryKey,
        ForeignKey,
        /*
      * CHECK

UNIQUE

PRIMARY KEY

FOREIGN KEY
        */
    }
}
