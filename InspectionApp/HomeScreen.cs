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
using Android;
using Android.Content;

namespace InspectionApp
{
    [Activity(Label = "INSPECTION", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory =true, Icon ="@drawable/Icon")]
    public class HomeScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
 
			if (!canAccessLocation() || !canAccessCamera() || !canWriteExternalStorage())
			{
				RequestPermissions(new string[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage }, 1);
			}

			Thread.Sleep(1500);
			StartActivity(typeof(MainActivity));
        }

		private Boolean canAccessLocation()
		{
			return (hasPermission(Manifest.Permission.AccessFineLocation));
		}

		private Boolean canAccessCamera()
		{
			return (hasPermission(Manifest.Permission.Camera));
		}

		private Boolean canWriteExternalStorage()
		{
			return (hasPermission(Manifest.Permission.WriteExternalStorage));
		}

		private Boolean hasPermission(String perm)
		{
			return (CheckSelfPermission(perm) == Android.Content.PM.Permission.Granted);
		}
	}
}