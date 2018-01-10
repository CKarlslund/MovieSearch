using SearchMovies.Data.CustomRenderer;
using SearchMovies.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AllCapsLabel), typeof(AllCapsLabelRenderer))]
namespace SearchMovies.Droid
{
    class AllCapsLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Black);
                Control.SetAllCaps(true);
                Control.SetPaddingRelative(20,5,50,10);
                Control.SetTextColor(global::Android.Graphics.Color.White);
            }
        }
    }
}