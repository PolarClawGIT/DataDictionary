using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Security
{
    class ObjectManager<TValue> : ObjectManager
        where TValue : class, IObjectSecurityValue
    {
        ScopeType formScope;

        public ObjectManager(ScopeType scope) : base ()
        {
            formScope = scope;

            SetIcon(scope);
        }
    }
}
