using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;

namespace InspectionApp
{
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Template manageTemplate= new Template();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            manageTemplate.SetContext(this);
            manageTemplate.SetDefaultTemplate();
        }
    }
}

