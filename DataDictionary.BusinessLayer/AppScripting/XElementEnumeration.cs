using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Used to map a ScopeType to the PropertyInfo definition for use in scripting.
    /// </summary>
    public class XElementEnumeration : Enumeration<ScopeType, XElementEnumeration>
    {
        Type? propertySource;
        List<PropertyInfo> properties = new List<PropertyInfo>();

        XElementEnumeration(ScopeType scope) : base()
        {
            ScopeEnumeration source = ScopeEnumeration.Cast(scope);
        }

        XElementEnumeration(ScopeType scope, Type source) : this(scope)
        {
            propertySource = source;
            properties = source.GetProperties().ToList();
        }

        static XElementEnumeration()
        {
            List<XElementEnumeration> data = new List<XElementEnumeration>()
            {
                //new PropertyEnumeration(ScopeType.Null),
                //new PropertyEnumeration(ScopeType.Application),
                //new PropertyEnumeration(ScopeType.ApplicationHelp),
                //new PropertyEnumeration(ScopeType.ApplicationHelpPage),
                //new PropertyEnumeration(ScopeType.ApplicationHelpForm),
                //new PropertyEnumeration(ScopeType.ApplicationHelpGroup),
                //new PropertyEnumeration(ScopeType.ApplicationOption),
                //new PropertyEnumeration(ScopeType.Model),
                new XElementEnumeration(ScopeType.ModelSubjectArea, typeof(AppModel.SubjectAreaValue)),
                new XElementEnumeration(ScopeType.ModelAttribute, typeof(AppModel.AttributeValue)),
                new XElementEnumeration(ScopeType.ModelAttributeAlias, typeof(AppModel.AttributeAliasValue)),
                new XElementEnumeration(ScopeType.ModelAttributeProperty, typeof(AppModel.AttributePropertyValue)),
                new XElementEnumeration(ScopeType.ModelAttributeDefinition, typeof(AppModel.AttributeDefinitionValue)),
                new XElementEnumeration(ScopeType.ModelAttributeSubjectArea, typeof(AppModel.AttributeSubjectAreaValue)),
                new XElementEnumeration(ScopeType.ModelEntity, typeof(AppModel.EntityValue)),
                new XElementEnumeration(ScopeType.ModelEntityAlias, typeof(AppModel.EntityAliasValue)),
                new XElementEnumeration(ScopeType.ModelEntityProperty, typeof(AppModel.EntityPropertyValue)),
                new XElementEnumeration(ScopeType.ModelEntityDefinition, typeof(AppModel.EntityDefinitionValue)),
                new XElementEnumeration(ScopeType.ModelEntitySubjectArea, typeof(AppModel.EntitySubjectAreaValue)),
                new XElementEnumeration(ScopeType.ModelEntityAttribute, typeof(AppModel.EntityAttributeValue)),
                new XElementEnumeration(ScopeType.ModelProcess, typeof(AppModel.ProcessValue)),
                new XElementEnumeration(ScopeType.ModelProcessAlias, typeof(AppModel.ProcessAliasValue)),
                new XElementEnumeration(ScopeType.ModelProcessProperty, typeof(AppModel.ProcessPropertyValue)),
                new XElementEnumeration(ScopeType.ModelProcessDefinition, typeof(AppModel.ProcessDefinitionValue)),
                new XElementEnumeration(ScopeType.ModelProcessSubjectArea, typeof(AppModel.ProcessSubjectAreaValue)),
                new XElementEnumeration(ScopeType.ModelProcessArgument, typeof(AppModel.ProcessArgumentValue)),
                //new PropertyEnumeration(ScopeType.ModelNameSpace),
                new XElementEnumeration(ScopeType.ModelProperty, typeof(AppModel.PropertyValue)),
                new XElementEnumeration(ScopeType.ModelDefinition, typeof(AppModel.DefinitionValue)),
                //new PropertyEnumeration(ScopeType.ModelAlias),
                //new PropertyEnumeration(ScopeType.Library),
                //new PropertyEnumeration(ScopeType.LibraryNameSpace),
                //new PropertyEnumeration(ScopeType.LibraryType),
                //new PropertyEnumeration(ScopeType.LibraryTypeEvent),
                //new PropertyEnumeration(ScopeType.LibraryTypeField),
                //new PropertyEnumeration(ScopeType.LibraryTypeMethod),
                //new PropertyEnumeration(ScopeType.LibraryTypeProperty),
                //new PropertyEnumeration(ScopeType.LibraryTypeParameter),
                //new PropertyEnumeration(ScopeType.Database),
                new XElementEnumeration(ScopeType.DatabaseSchema, typeof(AppCatalog.SchemaValue)),
                new XElementEnumeration(ScopeType.DatabaseTable, typeof(AppCatalog.TableValue)),
                new XElementEnumeration(ScopeType.DatabaseFunction, typeof(AppCatalog.RoutineValue)),
                new XElementEnumeration(ScopeType.DatabaseProcedure, typeof(AppCatalog.RoutineValue)),
                new XElementEnumeration(ScopeType.DatabaseDomain, typeof(AppCatalog.DomainValue)),
                new XElementEnumeration(ScopeType.DatabaseView, typeof(AppCatalog.TableValue)),
                new XElementEnumeration(ScopeType.DatabaseViewColumn, typeof(AppCatalog.TableColumnValue)),
                new XElementEnumeration(ScopeType.DatabaseTableColumn, typeof(AppCatalog.TableColumnValue)),
                new XElementEnumeration(ScopeType.DatabaseConstraint, typeof(AppCatalog.ConstraintValue)),
                new XElementEnumeration(ScopeType.DatabaseConstraintColumn, typeof(AppCatalog.ConstraintColumnValue)),
                new XElementEnumeration(ScopeType.DatabaseProcedureParameter, typeof(AppCatalog.RoutineParameterValue)),
                new XElementEnumeration(ScopeType.DatabaseFunctionParameter, typeof(AppCatalog.RoutineParameterValue)),
                new XElementEnumeration(ScopeType.DatabaseFunctionColumn, typeof(AppCatalog.RoutineColumnValue)),
                new XElementEnumeration(ScopeType.DatabaseReference, typeof(AppCatalog.ReferenceValue)),
                new XElementEnumeration(ScopeType.DatabaseProperty, typeof(AppCatalog.PropertyValue)),
                //new PropertyEnumeration(ScopeType.Security),
                //new PropertyEnumeration(ScopeType.SecurityPrincipal),
                //new PropertyEnumeration(ScopeType.SecurityRole),
                //new PropertyEnumeration(ScopeType.SecuritySecurable),
                //new PropertyEnumeration(ScopeType.Scripting),
                //new PropertyEnumeration(ScopeType.ScriptingTemplate),
                //new PropertyEnumeration(ScopeType.ScriptingTemplatePath),
                //new PropertyEnumeration(ScopeType.ScriptingTemplateNode),
                //new PropertyEnumeration(ScopeType.ScriptingTemplateAttribute),
                //new PropertyEnumeration(ScopeType.ScriptingTemplateDocument),
            };

            BuildDictionary(data);
        }

        public static IEnumerable<PropertyInfo> GetProperties(ScopeType scope)
        {
            if (Members.ContainsKey(scope))
            { return Members[scope].properties; }
            else { return new List<PropertyInfo>(); }
        }

        public static ScopeType GetScope(Type type)
        {
            if (Members.FirstOrDefault(w => w.Value.propertySource is not null && w.Value.propertySource.Equals(type)).Key is ScopeType result)
            { return result; }
            else { return ScopeType.Null; }
        }

    }
}
