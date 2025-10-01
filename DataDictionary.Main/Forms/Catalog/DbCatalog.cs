using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbCatalog : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingSource.Current is ICatalogValue current && ReferenceEquals(current, item); }

        protected DbCatalog() : base()
        {
            InitializeComponent();

            SetRowState(bindingSource);
            SetTitle(bindingSource);
            SetCommand(ScopeType.Database, Enumerations.CommandType.Export);
        }

        public DbCatalog(ICatalogValue catalogItem) : this()
        {
            CatalogIndex key = new CatalogIndex(catalogItem);

            IBindingList data = new BindingView<CatalogValue>(BusinessData.CatalogModel.DbCatalogs, w => key.Equals(w));
            data.ListChanged += ListChanged;

            bindingSource.DataSource = data;
            bindingSource.Position = 0;

            CommandButtons[Enumerations.CommandType.Export].Text = "to Model";
            CommandButtons[Enumerations.CommandType.Export].DropDown = exportOptions;
            exportAll.Image = ScopeType.Model.GetImage(Enumerations.CommandType.Add);
            exportAttributes.Image = ScopeType.ModelAttribute.GetImage(Enumerations.CommandType.Add);
            exportEntites.Image = ScopeType.ModelEntity.GetImage(Enumerations.CommandType.Add);

            exportProcesses.Visible = false; // Disabled until processes are supported

            void ListChanged(Object? sender, ListChangedEventArgs e)
            {
                // This addresses an invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                { bindingSource.RaiseListChangedEvents = false; }
            }
        }

        private void DbCatalog_Load(object sender, EventArgs e)
        {
            catalogTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ICatalogValue.CatalogTitle)));
            catalogDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ICatalogValue.CatalogDescription)));
            sourceServerNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ICatalogValue.ServerName)));
            sourceDatabaseNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ICatalogValue.DatabaseName)));
            sourceDateData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSource, nameof(ICatalogValue.SourceDate)));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSource.Current is not ICatalogValue);
        }

        private void ExportOptionAll_Click(object sender, EventArgs e)
        {
            if (bindingSource.Current is ICatalogValue current)
            {
                throw new NotImplementedException(); // TODO needs to be fixed.
                //var newModel = new ModelImport();
                //List<WorkItem> work = new List<WorkItem>();
                //work.AddRange(newModel.Load(BusinessData.CatalogModel, current));
                //work.AddRange(newModel.Build(BusinessData.Model));

                //DoWork(work, onCompleting);
            }

            //void onCompleting(RunWorkerCompletedEventArgs args)
            //{ SendMessage(new RefreshNavigation()); }
        }


        private void ExportEntites_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.Model.Entities.Import(BusinessData.Catalog, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportAttributes_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingSource.Current is ICatalogValue current)
            //{
            //    BusinessData.Model.Attributes.Import(BusinessData.Catalog, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportProcesses_Click(object sender, EventArgs e)
        {
            //TODO: Add processes
            throw new NotImplementedException();
        }
    }
}
