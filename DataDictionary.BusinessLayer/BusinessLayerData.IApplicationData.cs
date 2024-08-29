using DataDictionary.BusinessLayer.Application;
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
		/// Wrapper for Application Data
		/// </summary>
		public IApplicationData ApplicationData { get { return applicationValues; } }
        private readonly ApplicationData applicationValues;
    }
}
