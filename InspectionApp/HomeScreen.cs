using Android.App;
using Android.OS;
using System.Threading;

namespace InspectionApp
{
	[Activity(Label = "INSPECTION", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory =true, Icon ="@drawable/Icon")]
    public class HomeScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
 
			Thread.Sleep(1500);
			StartActivity(typeof(MainActivity));
        }
	}
}