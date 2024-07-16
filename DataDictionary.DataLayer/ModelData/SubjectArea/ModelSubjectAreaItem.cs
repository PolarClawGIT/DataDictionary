using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ModelData.SubjectArea
{
    /// <summary>
    /// Interface for Model Subject Area Item
    /// </summary>
    public interface IModelSubjectAreaItem : IModelSubjectAreaKey, IModelSubjectAreaUniqueKey, IScopeType
    {
        /// <summary>
        /// Description of the Subject Area
        /// </summary>
        String? SubjectAreaDescription { get; }

        /// <summary>
        /// NameSpace used for the Subject Area
        /// </summary>
        String? SubjectName { get; }
    }

    /// <summary>
    /// Implementation for Model Subject Area Item
    /// </summary>
    [Serializable]
    public class ModelSubjectAreaItem : BindingTableRow, IModelSubjectAreaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get { return GetValue<Guid>(nameof(SubjectAreaId)); } protected set { SetValue(nameof(SubjectAreaId), value); } }

        /// <inheritdoc/>
        public String? SubjectAreaTitle { get { return GetValue(nameof(SubjectAreaTitle)); } set { SetValue(nameof(SubjectAreaTitle), value); } }

        /// <inheritdoc/>
        public String? SubjectAreaDescription { get { return GetValue(nameof(SubjectAreaDescription)); } set { SetValue(nameof(SubjectAreaDescription), value); } }

        /// <inheritdoc/>
        public String? SubjectName { get { return GetValue(nameof(SubjectName)); } set { SetValue(nameof(SubjectName), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelSubjectArea;

        /// <summary>
        /// Constructor for Model Subject Area Item
        /// </summary>
        public ModelSubjectAreaItem() : base()
        {
            if (SubjectAreaId is null) { SubjectAreaId = Guid.NewGuid(); }
            if (string.IsNullOrWhiteSpace(SubjectAreaTitle)) { SubjectAreaTitle = "(new Subject Area)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(SubjectName), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ModelSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (SubjectAreaTitle is not null) { return SubjectAreaTitle; } else { return string.Empty; } }
    }
}
