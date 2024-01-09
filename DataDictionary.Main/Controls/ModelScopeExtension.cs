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
            {ScopeType.DatabaseSchemaFunction,           Resources.ScalarFunction },
            {ScopeType.DatabaseSchemaProcedure,          Resources.Procedure },
            {ScopeType.DatabaseSchemaTable,              Resources.Table },
            {ScopeType.DatabaseSchemaType,               Resources.Type },
            {ScopeType.DatabaseSchemaView,               Resources.View },
            {ScopeType.DatabaseSchemaViewColumn,         Resources.Column },
            {ScopeType.DatabaseSchemaTableColumn,        Resources.Column },
            {ScopeType.DatabaseSchemaTableConstraint,    Resources.Key},
            {ScopeType.DatabaseSchemaProcedureParameter, Resources.Parameter },
            {ScopeType.DatabaseSchemaFunctionParameter,  Resources.Parameter },

            {ScopeType.Model,                            Resources.SaveTable },
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
