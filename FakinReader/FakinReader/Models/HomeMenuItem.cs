﻿using FakinReader.Models.Enums;
using Xamarin.Forms;

namespace FakinReader.Models
{
    public class HomeMenuItem
    {
        #region Properties
        public ImageSource IconSource { get; set; }
        public MenuItemType MenuItemType { get; set; }
        public string Title { get; set; }
        #endregion Properties
    }
}