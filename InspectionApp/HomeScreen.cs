using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace InspectionApp
{
    [Activity(Label = "Inspection", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory =true, Icon ="@drawable/Icon")]
    public class HomeScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Thread.Sleep(4000);
            StartActivity(typeof(MainActivity));

            // Create your application here
        }
    }
}