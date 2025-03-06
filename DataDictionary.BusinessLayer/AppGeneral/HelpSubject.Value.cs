using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpSubjectItem, IScopeType,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace,
        IDataValue, ITemporalValue
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpSubjectItem, IHelpSubjectValue
    {
        IDataValue dataValue; // Backing field for IDataValue
        HelpSubjectIndexPath pathValue; // Backing field for Help Subject Path/NameSpace

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ApplicationHelpPage;

        /// <inheritdoc cref="HelpSubjectItem.NameSpace"/>
        public HelpSubjectIndexPath Path { get { return pathValue; } }

        /// <inheritdoc/>
        public HelpSubjectValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            };

            pathValue = new HelpSubjectIndexPath(this);

            this.PropertyChanged += PropertyChanged;

            void PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(this.NameSpace))
                {
                    pathValue = new HelpSubjectIndexPath(this);
                    OnPropertyChanged(nameof(Path)); 
                }
            }
        }


    }
}
