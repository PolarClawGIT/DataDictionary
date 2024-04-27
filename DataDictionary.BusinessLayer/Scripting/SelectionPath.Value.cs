<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using DataDictionary.DataLayer.ScriptingData.Selection;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ISelectionPathValue : DataLayer.ScriptingData.Selection.ISelectionPathItem
    { }

    /// <inheritdoc/>
    public class SelectionPathValue : DataLayer.ScriptingData.Selection.SelectionPathItem, ISelectionPathValue
=======
    public interface ISelectionPathValue : ISelectionPathItem
    { }

    /// <inheritdoc/>
    public class SelectionPathValue : SelectionPathItem, ISelectionPathValue
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public SelectionPathValue() : base() { }
    }
}
