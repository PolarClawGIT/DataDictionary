using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtention
    {
        /// <summary>
        /// Gets the Image for the Scope and Command
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Scope or Command is not in Navigation Enumeration</exception>
        public static Image GetImage(this ScopeType scope, CommandType command)
        {
            if (Enumeration.TryGetValue(scope, out Enumeration? value))
            {
                if (value.CommandImages.TryGetValue(command, out Func<Image>? image))
                { return image(); }
                else if (value.CommandImages.TryGetValue(CommandType.Default, out Func<Image>? defaultImage))
                { return defaultImage(); }
                else
                { throw new IndexOutOfRangeException(String.Format("Command Type unkown: {0}", command.ToString())); }
            }
            else
            { throw new IndexOutOfRangeException(String.Format("Scope Type unkown: {0}", scope.ToString())); }
        }

        /// <summary>
        /// Try/Get the Image for the Scope and Command.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, CommandType command, [NotNullWhen(true)] out Image? result)
        {
            if (Enumeration.TryGetValue(scope, out Enumeration? value)
                && value.CommandImages.TryGetValue(command, out Func<Image>? image))
            { result = image(); return true; }
            else { result = null; return false; }
        }
    }
}
