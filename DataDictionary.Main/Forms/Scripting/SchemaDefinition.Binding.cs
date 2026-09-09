using DataDictionary.BusinessLayer.AppScripting;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;
            public Action<XmlBuilderNode> OnSchemaChanged { get; init; } = (value) => { return; };

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderNode> BuilderData { get; }
            BindingView<SchemaNodeValue> schemaNodes;
            XmlBuilderData nodeValues = new XmlBuilderData();
            public DataBinding<SchemaDocumentValue> DocumentData { get; }

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding,
                BindingSource documentBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                DocumentData = new DataBinding<SchemaDocumentValue>(documentBinding, () => GetData().SchemaDocuments);
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes,w => 1==2);
                BuilderData = new DataBinding<XmlBuilderNode>(nodeBinding, () => nodeValues);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);

                BuilderData.ListChanged += BuilderData_ListChanged;

                void BuilderData_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    if (e.ListChangedType is ListChangedType.ItemChanged
                        && e.PropertyDescriptor is PropertyDescriptor property
                        && property.Name is nameof(XmlBuilderNode.SchemaNode)
                        && e.NewIndex >= 0
                        && e.NewIndex < BuilderData.Count)
                    {
                        XmlBuilderNode value = BuilderData[e.NewIndex];
                        XmlBuilderIndex key = new XmlBuilderIndex(value);
                        SchemaNodeValue? item = schemaNodes.SingleOrDefault(w => key.Equals(w));

                        if (value.SchemaNode is null && item is not null)
                        { schemaNodes.Remove(item); }
                        else if (value.SchemaNode is not null && item is null)
                        { schemaNodes.Add(value.SchemaNode); }
                    }
                }
            }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                SchemaData.LoadBinding(w => key.Equals(w));
                DocumentData.LoadBinding(w => key.Equals(w));
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes, w => key.Equals(w));

                if (SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue))
                { TemplateData.LoadBinding(w => new TemplateIndex(schemaValue).Equals(w)); }
                else
                { TemplateData.LoadBinding(w => new TemplateIndex().Equals(w)); }

                nodeValues.Load(key, GetData().SchemataNodes);
                BuilderData.LoadBinding();

                foreach (var item in schemaNodes)
                {
                    XmlBuilderIndex builderKey = new XmlBuilderIndex(item);
                    XmlBuilderNode builder = BuilderData.Single(w => builderKey.Equals(w));
                    builder.SchemaNode = item;
                }
            }

            public void LoadValue(TemplateIndex key, out SchemaDefinitionIndex schema)
            {
                SchemaDefinitionValue value = new SchemaDefinitionValue(key);
                GetData().Schemata.Add(value);
                schema = new SchemaDefinitionIndex(value);
                LoadValue(schema);
            }

            protected void RemoveValue(SchemaDefinitionIndex key)
            {
                GetData().Schemata.Remove(key);
                GetData().SchemataNodes.Remove(key);
                GetData().SchemaDocuments.Remove(key);
                throw new NotImplementedException();
            }

            public void BuildDocuments()
            {
                foreach (SchemaDocumentValue value in DocumentData.ToList())
                {
                    if (!value.IsExcluded && BusinessData.Model.TryGetBuilder(value, out Func<IEnumerable<XmlBuilder>, XElement>? builder))
                    { value.FileContent = new XDocument(new XDeclaration("1.0", "utf-8", null), builder(nodeValues)).Format(); }
                    else if(!value.KeepOrphaned)
                    { DocumentData.Remove(value); }
                }
            }

        }
    }
}
