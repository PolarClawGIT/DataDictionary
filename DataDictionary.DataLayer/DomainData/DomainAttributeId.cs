﻿using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeId
    {
        Nullable<Guid> AttributeId { get; }
    }
}
