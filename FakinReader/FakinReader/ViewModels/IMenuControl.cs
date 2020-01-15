using FakinReader.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FakinReader.ViewModels
{
    public interface IMenuControl<T>
    {
        #region Properties
        IAccountManager AccountManager { get; }
        int ExpandedHeight { get; }
        string ExpanderButtonText { get; }
        ICommand ExpandHideData { get; }
        #endregion Properties

        #region Methods
        Task<List<T>> GetMenuItems();
        void ResetMenuItems();
        #endregion Methods
    }
}