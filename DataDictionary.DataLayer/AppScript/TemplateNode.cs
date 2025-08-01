using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Template Node Interface. Represents an XML Attribute or XML Element.
    /// </summary>
    public interface ITemplateNode
    {
        /// <summary>
        /// Name of the Node (Attribute or Element)
        /// </summary>
        String? NodeName { get; }

        //String? NodePath { get; } TODO: To be Resolved at Business Layer?
        
        /// <summary>
        /// Order of the Node to be rendered.
        /// </summary>
        Int32? RenderOrder { get; }

        /// <summary>
        /// How the node is to be Rendered
        /// </summary>
        TemplateNodeValueAsType RenderValueAs { get; }

        /// <summary>
        /// Fixed value for the Node Value.
        /// </summary>
        String? FixedValue { get; }

        /// <summary>
        /// Scope of the object whos Property value is to be rendered.
        /// </summary>
        ScopeType ObjectScope { get; }

        /// <summary>
        /// Property of the Object whos value is to be rendered.
        /// </summary>
        String? ObjectProperty { get; }

        /// <summary>
        /// The ID of the Model Property whos value is to be rendered.
        /// </summary>
        Guid? ModelPropertyId { get; }
    }
}
