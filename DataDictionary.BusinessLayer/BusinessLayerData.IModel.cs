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
        public IModelData Models { get { return models; } }
        private readonly Model.ModelData models = new Model.ModelData();

        /// <summary>
        /// Subject Areas for the Model
        /// </summary>
        public ISubjectAreaData ModelSubjectAreas { get { return subjectAreas; } }
        private readonly SubjectAreaData subjectAreas = new SubjectAreaData();
    }
}
