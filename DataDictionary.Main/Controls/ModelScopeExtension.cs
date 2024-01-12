using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class ModelScopeExtension
    {
        /// <summary>
        /// This associates a Image to a Model Scope. ScopeType must be defined. ScopeKey defines the text.
        /// </summary>
        /// <see cref="DataDictionary.DataLayer.ApplicationData.Scope.ScopeType"/>
        /// <see cref="DataDictionary.DataLayer.ApplicationData.Scope.ScopeKey"/>
        static Dictionary<ScopeType, Image> images = new Dictionary<ScopeType, Image>()
        {
            {ScopeType.Null,                             Resources.UnknownMember },
            {ScopeType.Library,                          Resources.Library },
            {ScopeType.LibraryEvent,                     Resources.Event },
            {ScopeType.LibraryField,                     Resources.Field },
            {ScopeType.LibraryMethod,                    Resources.Method },
            {ScopeType.LibraryNameSpace,                 Resources.Namespace },
            {ScopeType.LibraryProperty,                  Resources.Property },
            {ScopeType.LibraryParameter,                 Resources.Parameter },
            {ScopeType.LibraryType,                      Resources.Class },

            {ScopeType.Database,                         Resources.Database },
            {ScopeType.DatabaseSchema,                   Resources.Schema },
            {ScopeType.DatabaseFunction,                 Resources.ScalarFunction },
            {ScopeType.DatabaseProcedure,                Resources.Procedure },
            {ScopeType.DatabaseTable,                    Resources.Table },
            {ScopeType.DatabaseDomain,                   Resources.Type },
            {ScopeType.DatabaseView,                     Resources.View },
            {ScopeType.DatabaseViewColumn,               Resources.Column },
            {ScopeType.DatabaseTableColumn,              Resources.Column },
            {ScopeType.DatabaseTableConstraint,          Resources.Key},
            {ScopeType.DatabaseProcedureParameter,       Resources.Parameter },
            {ScopeType.DatabaseFunctionParameter,        Resources.Parameter },

            {ScopeType.Model,                            Resources.SoftwareDefinitionModel },
            {ScopeType.ModelSubjectArea,                 Resources.Diagram },
            {ScopeType.ModelAttribute,                   Resources.Attribute },
            {ScopeType.ModelEntity,                      Resources.Entity },

        };

        public static Image ToImage(this ScopeType scope)
        {
            if (images.ContainsKey(scope))
            { return images[scope]; }
            else
            { return images[ScopeType.Null]; }
        }

        public static ImageList ToImageList()
        {
            ImageList result = new ImageList();

            foreach (KeyValuePair<ScopeType, Image> item in images)
            { result.Images.Add(item.Key.ToScopeName(), item.Value); }

            return result;
        }
    }
}
