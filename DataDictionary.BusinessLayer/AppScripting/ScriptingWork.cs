using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Used to build a Scripting Engine WorkItem
    /// </summary>
    public class ScriptingWork
    {
        /// <inheritdoc cref="ScriptingEngine.Templates"/>
        public ScriptingTemplateValue Template { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateAttributes"/>
        public IReadOnlyList<ScriptingAttributeValue> Attributes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateNodes"/>
        public IReadOnlyList<ScriptingNodeValue> Nodes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplatePaths"/>
        public IReadOnlyList<ScriptingPathValue> Paths { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateDocuments"/>
        public BindingView<XDocumentValue> Documents { get; protected set; }

        internal ScriptingWork(IScriptingTemplateIndex template, IScriptingEngine source)
        {
            ScriptingTemplateIndex key = new ScriptingTemplateIndex(template);

            if (source.Templates.FirstOrDefault(w => key.Equals(w)) is ScriptingTemplateValue value)
            { Template = value; }
            else
            {
                Template = new ScriptingTemplateValue();
                key = new ScriptingTemplateIndex(Template);
            }

            Attributes = new BindingView<ScriptingAttributeValue>(source.TemplateAttributes, w => key.Equals(w));
            Nodes = new BindingView<ScriptingNodeValue>(source.TemplateNodes, w => key.Equals(w));
            Paths = new BindingView<ScriptingPathValue>(source.TemplatePaths, w => key.Equals(w));
            Documents = new BindingView<XDocumentValue>(source.TemplateDocuments, w => key.Equals(w));
        }

    }
}
