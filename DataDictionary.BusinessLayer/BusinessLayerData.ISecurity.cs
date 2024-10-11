using DataDictionary.BusinessLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Current User Identity
        /// </summary>
        public IIdentity UserIdentity { get; private set; }

        /// <summary>
        /// Security for the current user
        /// </summary>
        public IUserSecurity UserSecurity { get { return userSecurityValue; } }
        private readonly Security userSecurityValue;
    }
}
