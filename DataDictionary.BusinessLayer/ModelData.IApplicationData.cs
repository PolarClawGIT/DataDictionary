using DataDictionary.BusinessLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
	partial class ModelData : IApplicationData
	{
		/// <inheritdoc/>
		public IHelpSubjectData HelpSubjects { get { return ApplicationData.HelpSubjects; } }

		/// <inheritdoc/>
		public IPropertyData Properties { get { return ApplicationData.Properties; } }

		/// <summary>
		/// Wrapper for Application Data
		/// </summary>
		public ApplicationData.ApplicationData ApplicationData { get; } = new ApplicationData.ApplicationData();
	}
}
