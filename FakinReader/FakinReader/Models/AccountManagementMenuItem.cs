using FakinReader.Models.Enums;
using Xamarin.Forms;

namespace FakinReader.Models
{
    public class AccountManagementMenuItem
    {
        #region Properties
        public string Title { get; set; }
        public ImageSource IconSource { get; set; }
        public MenuItemType MenuItemType { get; set; }
        #endregion Properties
    }
}