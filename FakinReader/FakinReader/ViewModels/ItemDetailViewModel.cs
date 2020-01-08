using FakinReader.Models;

namespace FakinReader.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Constructors

        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
        #endregion Constructors

        #region Properties
        public Item Item { get; set; }
        #endregion Properties
    }
}