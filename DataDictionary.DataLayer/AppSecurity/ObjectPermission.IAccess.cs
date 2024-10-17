using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for common Security Object Access Permission
    /// </summary>
    public interface IObjectAccess
    {
        /// <summary>
        /// Grant permission for the object
        /// </summary>
        Boolean IsGrant { get; set; }

        /// <summary>
        /// Deny all permission for the object
        /// </summary>
        Boolean IsDeny { get; set; }
    }
}
