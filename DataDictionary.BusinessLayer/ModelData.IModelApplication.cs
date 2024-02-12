using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Property;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Application data
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelApplication
    {
        /// <summary>
        /// List of Help Subjects for the Application (the help system).
        /// </summary>
        HelpCollection HelpSubjects { get; }

        /// <summary>
        /// List Properties defined for the Application.
        /// </summary>
        PropertyCollection Properties { get; }
    }
}
