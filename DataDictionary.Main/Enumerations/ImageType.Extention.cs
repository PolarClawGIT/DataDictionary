using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    partial class ScopeIcons
    {
        public static Icon GetIcon(this ScopeType scope)
        {
            if (scope.TryGetIcon(out Icon? result))
            { return result; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(scope), scope);
                throw ex;
            }
        }

        public static Boolean TryGetIcon(this ScopeType scope, [NotNullWhen(true)] out Icon? value)
        {
            if (scopeIconMap.ContainsKey(scope))
            { value = scopeIconMap[scope]; return true; }
            else { value = null; return false; }
        }

        public static Boolean TryGetImage(this ScopeType scope, [NotNullWhen(true)] out Image? value)
        {
            if (scope.TryGetIcon(out Icon? result))
            { value = result.GetSmallImage(); return true; }
            else { value = null; return false; }
        }

        public static void AddImages(this ImageList target, params IEnumerable<ScopeType> scopes)
        {
            foreach (var item in scopes)
            {
                if (!target.Images.ContainsKey(item.GetName())
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(item.GetName(), value); }
            }
        }

        public static void AddImages(this ImageList target)
        { target.AddImages(Enum.GetValues<ScopeType>().ToList()); }
    }
}
