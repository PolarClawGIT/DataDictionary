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
        public IModelData Models { get { return modelValues; } }
        private readonly ModelData modelValues;

        /// <summary>
        /// Subject Areas for the Model
        /// </summary>
        public ISubjectAreaData SubjectAreas { get { return subjectAreaValues; } }
        private readonly SubjectAreaData subjectAreaValues;
    }
}
