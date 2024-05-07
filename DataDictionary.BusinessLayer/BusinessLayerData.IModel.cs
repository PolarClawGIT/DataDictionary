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
        public IModelData Models { get { return modelValue; } }
        private readonly ModelData modelValue;

        /// <summary>
        /// Subject Areas for the Model
        /// </summary>
        public ISubjectAreaData ModelSubjectAreas { get { return subjectAreaValues; } }
        private readonly SubjectAreaData subjectAreaValues;
    }
}
