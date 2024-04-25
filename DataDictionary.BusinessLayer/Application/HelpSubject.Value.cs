﻿using DataDictionary.DataLayer.ApplicationData.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem, IHelpSubjectIndex
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpSubjectValue
    {
        /// <inheritdoc/>
        public HelpSubjectValue() : base() { }
    }
}
