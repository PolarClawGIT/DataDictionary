﻿using DataDictionary.BusinessLayer.Model;
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
        /// Models for the application (expect 0 or 1)
        /// </summary>
<<<<<<< HEAD
        public IModelData<ModelValue> Models { get { return modelValues; } }
        internal readonly Model.ModelData<ModelValue> modelValues;
=======
        public IModelData Models { get { return modelValues; } }
        private readonly ModelData modelValues;
>>>>>>> RenameIndexValue

        /// <summary>
        /// Subject Areas for the Model
        /// </summary>
        [Obsolete("May not be needed/supported")]
        public ISubjectAreaData ModelSubjectAreas { get { return subjectAreaValues; } }

        [Obsolete("May not be needed/supported")]
        private readonly SubjectAreaData subjectAreaValues;
    }
}
