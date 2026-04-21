using DataDictionary.DataLayer.AppScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Wrapper class around a merged list of SchemaDocumentData and TransformDocumentData
    /// </summary>
    [Obsolete("POC code")]
    public class DocumentData : BindingList<DocumentValue>
    {
        // TODO: Not even sure if this is usable or useful.
        // The problem is if an item is added, changed or removed from the two source lists.
        // the Linq.Union will not pick that up as it is a static list.
        // I want the list to update when either of the source lists update.
        // The sources are also Binding Views that have complex logic to handle changes.

        /// <summary>
        /// Constructor for the DocumentData
        /// </summary>
        /// <param name="schemaValues"></param>
        /// <param name="transformsValues"></param>
        public DocumentData(ISchemaDocumentData schemaValues, ITransformDocumentData transformsValues)
        {
            this.AddRange(schemaValues.Select(s => new DocumentValue(s)));
            this.AddRange(transformsValues.Select(s => new DocumentValue(s)));

            schemaValues.ListChanged += Values_ListChanged;
            transformsValues.ListChanged += Values_ListChanged;
        }

        private void Values_ListChanged(Object? sender, ListChangedEventArgs e)
        {
            Int32 newIndex = -1;
            DocumentValue document;
            IEnumerable<IDocumentItem> documents;
            DocumentCompare compare = new DocumentCompare();

            if (sender is SchemaDocumentData schemaData)
            {
                documents = schemaData.OfType<IDocumentItem>();

                if (e.NewIndex >= 0 && e.NewIndex < schemaData.Count)
                { document = new DocumentValue(schemaData[e.NewIndex]); }
                else { throw GetException(); }
            }
            else if (sender is TransformDocumentData transformData)
            {
                documents = transformData.OfType<IDocumentItem>();

                if (e.NewIndex >= 0 && e.NewIndex < transformData.Count)
                { document = new DocumentValue(transformData[e.NewIndex]); }
                else { throw GetException(); }
            }
            else { throw GetException(); }

            

            





            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    OnListChanged(new ListChangedEventArgs(e.ListChangedType, -1));
                    break;
                case ListChangedType.ItemAdded:
                    break;
                case ListChangedType.ItemDeleted:
                    break;
                case ListChangedType.ItemMoved:
                    break;
                case ListChangedType.ItemChanged:
                    break;
                case ListChangedType.PropertyDescriptorAdded:
                    break;
                case ListChangedType.PropertyDescriptorDeleted:
                    break;
                case ListChangedType.PropertyDescriptorChanged:
                    break;
                default:
                    break;
            }

            Exception GetException()
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(e.ListChangedType), e.ListChangedType);
                ex.Data.Add(nameof(e.NewIndex), e.NewIndex);
                if (sender is ICollection senderValue)
                {
                    ex.Data.Add(nameof(senderValue), senderValue.GetType().Name);
                    ex.Data.Add(nameof(senderValue.Count), senderValue.Count);
                }
                else if (sender is Object) { ex.Data.Add(nameof(sender), sender.GetType().Name); }
                else { ex.Data.Add(nameof(sender), "<Null>"); }

                return ex;
            }
        }


    }

}
