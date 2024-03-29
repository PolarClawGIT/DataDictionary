﻿using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Model
{
    partial class ModelManager
    {
        class ModelManagerItem : IModelItem
        {
            private bool inModel = false;
            private bool inDatabase = false;
            IModelItem data;

            public Boolean InModel { get { return inModel; } set { inModel = value; OnPropertyChanged(nameof(InModel)); } }
            public Boolean InDatabase { get { return inDatabase; } set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); } }

            public Guid? ModelId { get { return data.ModelId; } }
            public string? ModelTitle { get { return data.ModelTitle; } set { data.ModelTitle = value; } }
            public string? ModelDescription { get { return data.ModelDescription; } set { data.ModelDescription = value; } }


            public ModelManagerItem(IModelItem source) : base()
            {
                data = source;
                data.PropertyChanged += Data_PropertyChanged;
                data.RowStateChanged += Data_RowStateChanged;
            }

            private void Data_RowStateChanged(object? sender, EventArgs e)
            {
                if (RowStateChanged is EventHandler handler)
                { handler(sender, e); }
            }

            private void Data_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(sender, e); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public event EventHandler? RowStateChanged;

            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }

            public DataRowState RowState()
            { return data.RowState(); }
        }

        class ModelManagerCollection : BindingList<ModelManagerItem>
        {
            public void Build(IModelItem modelItem, IEnumerable<ModelItem> dbItems)
            {
                this.Clear();

                // List of keys all keys
                var modelItems = new List<IModelItem>() { modelItem };

                List<ModelKey> modelKeys = modelItems.Select(s => new ModelKey(s))
                    .Union(dbItems.Select(s => new ModelKey(s)))
                    .ToList();

                foreach (ModelKey modelKey in modelKeys)
                {
                    ModelManagerItem? item = this.FirstOrDefault(w => modelKey.Equals(w));
                    ModelItem? dbItem = dbItems.FirstOrDefault(w => modelKey.Equals(w));

                    if (item is null && dbItem is ModelItem)
                    {
                        item = new ModelManagerItem(dbItem);
                        this.Add(item);
                    }

                    if (item is null && modelItem is ModelItem)
                    {
                        item = new ModelManagerItem(modelItem);
                        this.Add(item);
                    }

                    if (item is ModelManagerItem)
                    {// Should always have a item at this point.
                        item.InModel = (modelItem is ModelItem);
                        item.InDatabase = (dbItem is ModelItem);
                    }
                }
            }

            public Boolean Remove(ModelItem modelItem)
            {
                ModelKey key = new ModelKey(modelItem);
                if (this.FirstOrDefault(w => key.Equals(w)) is ModelManagerItem toRemove)
                { return base.Remove(toRemove); }
                else { return false; }
            }
        }
    }
}
