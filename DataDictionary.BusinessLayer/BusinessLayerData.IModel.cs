using DataDictionary.BusinessLayer.Model;
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
        public IModelData<ModelValue> Models { get { return modelValues; } }
        private readonly Model.ModelData<ModelValue> modelValues;

        /// <summary>
        /// Subject Areas for the Model
        /// </summary>
        public ISubjectAreaData ModelSubjectAreas { get { return subjectAreaValues; } }
        private readonly SubjectAreaData subjectAreaValues;
    }
}
