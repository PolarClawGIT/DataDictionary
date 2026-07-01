using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DataDictionary.Main.Enumerations
{
    partial class NodeRenderAsIcon
    {
        /// <summary>
        /// Try/Get the Icon for the Scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetIcon(this NodeRenderAsType scope, [NotNullWhen(true)] out Icon? value)
        {
            if (nodeRenderIconMap.ContainsKey(scope))
            { value = nodeRenderIconMap[scope]; return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Try/Get the Icon converted to a Image (16x16) for the Scope.
        /// </summary>
        /// <param name="renderAs"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this NodeRenderAsType renderAs, [NotNullWhen(true)] out Image? value)
        {
            if (renderAs.TryGetIcon(out Icon? result))
            { value = result.GetSmallImage(); return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Adds a list of Images to an Image List for the Scopes listed.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="renderTypes"></param>
        public static void AddImages(this ImageList target, params IEnumerable<NodeRenderAsType> renderTypes)
        {
            foreach (var item in renderTypes)
            {
                if (Enum.GetName(item) is String name
                    && !target.Images.ContainsKey(name)
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(name, value); }
            }
        }
    }
}
