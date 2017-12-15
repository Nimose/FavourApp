
using Android.Webkit;
using Favourpp.Droid;
using Favourpp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebViewCustom), typeof(WebViewCustomRender))]
namespace Favourpp.Droid
{
    public class WebViewCustomRender : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            CookieManager.Instance.RemoveAllCookie();           
            this.Control.ClearCache(true);
            this.Control.ClearHistory();
            this.Control.ClearFormData();
        }
    }
}

