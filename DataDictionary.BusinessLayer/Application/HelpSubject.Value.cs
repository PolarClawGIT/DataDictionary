﻿using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpItem, IHelpSubjectIndex, IHelpSubjectIndexNameSpace
    { }
}
