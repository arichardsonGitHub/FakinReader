using System.Windows.Input;

namespace FakinReader.ViewModels
{
    public class HelpAndSupportViewModel : BaseViewModel
    {
        #region Constructors
        public HelpAndSupportViewModel()
        {
            Title = "Help and support";
        }
        #endregion Constructors

        #region Properties
        public ICommand OpenWebCommand { get; }
        #endregion Properties
    }
}