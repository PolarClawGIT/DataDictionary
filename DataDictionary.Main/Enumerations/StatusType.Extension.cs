using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    partial class StatusImage
    {
        /// <summary>
        /// Gets the Image (16x16) for the Scope and Status
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Image GetImage(this ScopeType scope, StatusType status)
        {
            if (scope.TryGetImage(status, out Image? result))
            { return result; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(scope), scope);
                ex.Data.Add(nameof(status), status);
                throw ex;
            }
        }

        /// <summary>
        /// Try/Get the Image (16x16) for the Scope and Status.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="status"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, StatusType status, [NotNullWhen(true)] out Image? value)
        {
            if (scope.TryGetImage(out Image? scopeImage)
                && statusOverlay.ContainsKey(status))
            { value = scopeImage.MergeImage(statusOverlay[status]); return true; }
            else { value = null; return false; }
        }
    }
}
