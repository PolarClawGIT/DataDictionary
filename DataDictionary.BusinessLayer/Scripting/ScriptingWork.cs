using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Used to build a Scripting Engine WorkItem
    /// </summary>
    public class ScriptingWork
    {
        /// <inheritdoc cref="ScriptingEngine.Templates"/>
        public TemplateValue Template { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateAttributes"/>
        public IReadOnlyList<TemplateAttributeValue> Attributes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateNodes"/>
        public IReadOnlyList<TemplateNodeValue> Nodes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplatePaths"/>
        public IReadOnlyList<TemplatePathValue> Paths { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateDocuments"/>
        public BindingView<TemplateDocumentValue> Documents { get; protected set; }

        internal ScriptingWork(ITemplateIndex template, IScriptingEngine source)
        {
            TemplateIndex key = new TemplateIndex(template);

            if (source.Templates.FirstOrDefault(w => key.Equals(w)) is TemplateValue value)
            { Template = value; }
            else
            {
                Template = new TemplateValue();
                key = new TemplateIndex(Template);
            }

            Attributes = new BindingView<TemplateAttributeValue>(source.TemplateAttributes, w => key.Equals(w));
            Nodes = new BindingView<TemplateNodeValue>(source.TemplateNodes, w => key.Equals(w));
            Paths = new BindingView<TemplatePathValue>(source.TemplatePaths, w => key.Equals(w));
            Documents = new BindingView<TemplateDocumentValue>(source.TemplateDocuments, w => key.Equals(w));
        }

    }
}
