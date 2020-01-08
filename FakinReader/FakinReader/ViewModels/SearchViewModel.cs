using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace FakinReader.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        #region Constructors

        public SearchViewModel()
        {
            Title = "Search";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }
        #endregion Constructors

        #region Properties
        public ICommand OpenWebCommand { get; }
        #endregion Properties
    }
}