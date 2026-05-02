using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode
    {
        class TreeDataNode : TreeNode
        {
            public SchemaNodeValue DataValue;

            public TreeDataNode(SchemaNodeValue nodeValue) : base()
            {
                DataValue = nodeValue;
                // Set Text
                // Set Icon
            }
        }
    }
}
