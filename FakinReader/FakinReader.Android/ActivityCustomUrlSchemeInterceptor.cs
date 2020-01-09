using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FakinReader.Helpers;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FakinReader.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "red",
    DataHost = "FakinReader.companyname.com",
    DataPath = "/oauth2redirect")]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        #region Methods

        #endregion Methods
    }
}