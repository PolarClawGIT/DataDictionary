using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Wrapper Interface for the Document Types.
    /// </summary>
    [Obsolete("POC code")]
    public interface IDocumentValue : IDocumentItem,
        IBindingRowState, IBindingPropertyChanged,
        IDocumentIndex, ITemplateIndex,
        IKeyEquality<IDocumentIndex>
    {
        /// <summary>
        /// Is the Document Type a Schema Document.
        /// </summary>
        Boolean IsSchemaDocument { get; }

        /// <summary>
        /// Is the Document Type a Transform Document.
        /// </summary>
        Boolean IsTransformDocument { get; }
    }

    /// <summary>
    /// Wrapper Class for the Document Types.
    /// </summary>
    [Obsolete("POC code")]
    public class DocumentValue : IDocumentValue
    {
        SchemaDocumentItem? schemaItem;
        TransformDocumentItem? transformItem;

        /// <inheritdoc/>
        public String? FileName
        {
            get
            {
                if (schemaItem is not null) { return schemaItem.FileName; }
                else if (transformItem is not null) { return transformItem.FileName; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Guid? DocumentId
        {
            get
            {
                if (schemaItem is not null) { return schemaItem.DocumentId; }
                else if (transformItem is not null) { return transformItem.DocumentId; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get
            {
                if (schemaItem is not null) { return schemaItem.TemplateId; }
                else if (transformItem is not null) { return transformItem.TemplateId; }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSchemaDocument
        {
            get
            {
                if (schemaItem is not null) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean IsTransformDocument
        {
            get
            {
                if (transformItem is not null) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean HasValue { get { return schemaItem is not null || transformItem is not null; } }

        /// <inheritdoc/>
        public event EventHandler<RowStateEventArgs>? RowStateChanged;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        private DocumentValue() : base() { }

        /// <summary>
        /// Create a DocumentItem from a SchemaDocumentItem
        /// </summary>
        /// <param name="value"></param>
        public DocumentValue(SchemaDocumentItem value) : this()
        {
            schemaItem = value;

            value.PropertyChanged += Value_PropertyChanged;
            value.RowStateChanged += Value_RowStateChanged;
        }

        /// <summary>
        /// Create a DocumentItem from a TransformDocumentItem
        /// </summary>
        /// <param name="value"></param>
        public DocumentValue(TransformDocumentItem value) : this()
        {
            transformItem = value;

            value.PropertyChanged += Value_PropertyChanged;
            value.RowStateChanged += Value_RowStateChanged;
        }

        private void Value_RowStateChanged(Object? sender, RowStateEventArgs e)
        {
            if (RowStateChanged is EventHandler<RowStateEventArgs> handler)
            { handler(this, new RowStateEventArgs(e.RowState)); }
        }

        private void Value_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler
                && (e.PropertyName is nameof(FileName) or nameof(DocumentId) or nameof(TemplateId)))
            { handler(this, new PropertyChangedEventArgs(e.PropertyName)); }
        }

        /// <inheritdoc/>
        public DataRowState RowState()
        {
            if (schemaItem is not null) { return schemaItem.RowState(); }
            else if (transformItem is not null) { return transformItem.RowState(); }
            else { return DataRowState.Detached; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IDocumentIndex? other)
        {
            if (schemaItem is IDocumentIndex schemaValue)
            { return new DocumentIndex(schemaValue).Equals(other); }
            else if (transformItem is IDocumentIndex transformValue)
            { return new DocumentIndex(transformValue).Equals(other); }
            else { return false; }
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDocumentIndex key && Equals(new DocumentIndex(key)); }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentValue left, IDocumentIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentValue left, IDocumentIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator ==(DocumentValue left, DocumentValue right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DocumentValue left, DocumentValue right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (DocumentId is Guid) { return DocumentId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }

    /// <summary>
    /// Used to Compare two Document Values.
    /// </summary>
    /// <remarks>Used with Linq Union</remarks>
    [Obsolete("POC code")]
    public class DocumentCompare : IEqualityComparer<DocumentValue>
    {
        /// <inheritdoc/>
        public Boolean Equals(DocumentValue? x, DocumentValue? y)
        { return x is DocumentValue left && y is DocumentValue right && left.Equals(right); }

        /// <inheritdoc/>
        public Int32 GetHashCode([DisallowNull] DocumentValue obj)
        { return obj.GetHashCode(); }
    }
}
