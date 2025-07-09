using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    public class XElementBuilder : IList<XElementNode>
    {
        public String RootNodeName { get; init; }

        List<XElementNode> Children { get; } = new List<XElementNode>();

        /// <inheritdoc/>
        public Int32 Count { get { return Children.Count; } }

        /// <inheritdoc/>
        public Boolean IsReadOnly { get { return false; } }

        /// <inheritdoc/>
        public XElementNode this[Int32 index]
        {
            get
            {
                if (index >= 0 && index < Children.Count)
                { return Children[index]; }
                else
                {
                    Exception ex = new IndexOutOfRangeException();
                    ex.Data.Add(nameof(index), index);
                    ex.Data.Add(nameof(Children.Count), Children.Count);
                    throw ex;
                }
            }
            set
            {
                if (index >= 0 && index < Children.Count)
                { Children[index] = value; }
                else
                {
                    Exception ex = new IndexOutOfRangeException();
                    ex.Data.Add(nameof(index), index);
                    ex.Data.Add(nameof(Children.Count), Children.Count);
                    throw ex;
                }
            }
        }

        public XElementNode this[String index]
        {
            get
            {
                if (Children.FirstOrDefault(w => index.Equals(w.PropertyName)) is XElementNode byPropertyName)
                { return byPropertyName; }
                else if (Children.FirstOrDefault(w => index.Equals(w.NodeName)) is XElementNode byNodeName)
                { return byNodeName; }
                else
                {
                    Exception ex = new IndexOutOfRangeException();
                    ex.Data.Add(nameof(index), index);
                    throw ex;
                }
            }
        }

        public XElementBuilder(ScopeType scope) : base()
        { RootNodeName = ScopeEnumeration.Cast(scope).Name; }

        public XElement Build(Object value)
        {
            XElement result = new XElement(RootNodeName);

            foreach (var item in Children)
            { result.Add(item.Build(value)); }

            return result;
        }

        /// <inheritdoc/>
        public override String ToString()
        { return RootNodeName; }

        /// <inheritdoc/>
        public Int32 IndexOf(XElementNode item)
        { return Children.IndexOf(item); }

        /// <inheritdoc/>
        public void Insert(Int32 index, XElementNode item)
        { Children.Insert(index, item); }

        /// <inheritdoc/>
        public void RemoveAt(Int32 index)
        { Children.RemoveAt(index); }

        /// <inheritdoc/>
        public void Add(XElementNode item)
        { Children.Add(item); }

        /// <inheritdoc/>
        public void Clear()
        { Children.Clear(); }

        /// <inheritdoc/>
        public Boolean Contains(XElementNode item)
        { return Children.Contains(item); }

        /// <inheritdoc/>
        public void CopyTo(XElementNode[] array, Int32 arrayIndex)
        {   Children.CopyTo(array, arrayIndex); }

        /// <inheritdoc/>
        public Boolean Remove(XElementNode item)
        { return Children.Remove(item); }

        /// <inheritdoc/>
        public IEnumerator<XElementNode> GetEnumerator()
        { return Children.GetEnumerator(); }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        { return Children.GetEnumerator(); }
    }


}
