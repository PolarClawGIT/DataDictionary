using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Used to build a Scripting Engine WorkItem
    /// </summary>
    [Obsolete("Not Used", true)]
    public class ScriptingWork
    {
        /// <inheritdoc/>
        public ScriptingTemplateValue Template { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyList<ScriptingAttributeValue> Attributes { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyList<ScriptingNodeValue> Nodes { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyList<ScriptingPathValue> Paths { get; protected set; }

        /// <inheritdoc/>
        public BindingView<XDocumentValue> Documents { get; protected set; }

        internal ScriptingWork(IScriptingTemplateIndex template, IScriptingEngine source)
        {
            throw new InvalidOperationException();

            //ScriptingTemplateIndex key = new ScriptingTemplateIndex(template);

            //if (source.Templates.FirstOrDefault(w => key.Equals(w)) is ScriptingTemplateValue value)
            //{ Template = value; }
            //else
            //{
            //    Template = new ScriptingTemplateValue();
            //    key = new ScriptingTemplateIndex(Template);
            //}

            //Attributes = new BindingView<ScriptingAttributeValue>(source.TemplateAttributes, w => key.Equals(w));
            //Nodes = new BindingView<ScriptingNodeValue>(source.TemplateNodes, w => key.Equals(w));
            //Paths = new BindingView<ScriptingPathValue>(source.TemplatePaths, w => key.Equals(w));
            //Documents = new BindingView<XDocumentValue>(source.TemplateDocuments, w => key.Equals(w));
        }

    }
}
