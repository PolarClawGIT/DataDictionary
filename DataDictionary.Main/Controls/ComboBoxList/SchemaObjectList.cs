using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Main.Forms.Scripting;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    
    record SchemaObjectList
    {
        // TODO: POC, Tried to get two CombBoxes to work together. It is not working.
        // Currently not needed.

        public XmlBuilderIndex? ValueMember { get; } = null;
        public BindingList<SchemaObjectList> Children { get; }

        //public static XmlBuilderIndex NullValue { get; } = ObjectValueType.Null;
        public static String NullValue { get; } = String.Empty;

        /// <inheritdoc/>
        public String ObjectScope
        {
            get
            {
                if (ValueMember is null) { return NullValue; }
                else
                { return ValueMember.ObjectScope.GetName(); }
            }
        }

        /// <inheritdoc/>
        public String? ObjectProperty
        {
            get
            {
                if (ValueMember is null) { return NullValue; }
                else
                { return ValueMember.ObjectProperty; }
            }
        }

        SchemaObjectList()
        {
            Children = new BindingList<SchemaObjectList>();
        }

        SchemaObjectList(XmlBuilder value)
        {
            ValueMember = new XmlBuilderIndex(value);
            Children = new BindingList<SchemaObjectList>();
        }

        SchemaObjectList(XmlBuilder value, IEnumerable<XmlBuilder> children)
        {
            ValueMember = new XmlBuilderIndex(value);
            Children = new BindingList<SchemaObjectList>(children.Select(s => new SchemaObjectList(s)).ToList());
        }

        public static void Load(ComboBoxData objectControl, ComboBoxData propertyControl, IEnumerable<XmlBuilder> source)
        {
            BindingList<SchemaObjectList> objectValues = new BindingList<SchemaObjectList>(
                source.
                    Where(w => String.IsNullOrEmpty(w.ObjectProperty)).
                    Select(s => new SchemaObjectList(s, source.Where(w => s.ObjectScope == w.ObjectScope &&!String.IsNullOrEmpty(w.ObjectProperty)))).
                    //Prepend(new SchemaObjectList()).
                    ToList());

            BindingSource parents = new BindingSource();
            BindingSource children = new BindingSource();
            parents.DataSource = objectValues;
            children.DataSource = parents;
            children.DataMember = nameof(Children);

            objectControl.DataSource = objectValues;
            objectControl.ValueMember = nameof(ObjectScope);
            objectControl.DisplayMember = nameof(ObjectScope);

            //objectControl.SelectedIndexChanged += ObjectControl_SelectedIndexChanged;

            propertyControl.DataSource = children;
            propertyControl.ValueMember = nameof(ObjectProperty);
            propertyControl.DisplayMember = nameof(ObjectProperty);

            void ObjectControl_SelectedIndexChanged(Object? sender, EventArgs e)
            {
                if(objectControl.SelectedIndex >= 0)
                {
                    propertyControl.DataSource = objectValues[objectControl.SelectedIndex].Children;
                }
                else { propertyControl.DataSource = null; }
            }
        }


    }
}
