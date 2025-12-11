using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    partial class CommandImage
    {
        /// <summary>
        /// Gets the Image (16x16) for the Scope and Command
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Image GetImage(this ScopeType scope, CommandType command)
        {
            if (scope.TryGetImage(command, out Image? result))
            { return result; }
            else {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(scope), scope);
                ex.Data.Add(nameof(command), command);
                throw ex;
            }
        }

        /// <summary>
        /// Try/Get the Image (16x16) for the Scope and Command.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, CommandType command, [NotNullWhen(true)] out Image? value)
        {
            if (scope.TryGetImage(out Image? scopeImage)
                && commandOverlay.ContainsKey(command))
            { value = scopeImage.MergeImage(commandOverlay[command]); return true; }
            else { value = null; return false; }
        }
    }
}
