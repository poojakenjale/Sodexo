using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace InspectionApp
{
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var btnStartAudit = FindViewById<Button>(Resource.Id.btnStartAudit);
            btnStartAudit.Click += btnStartAudit_Click;
                
        }

        private void btnStartAudit_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AuditDetails));
        }
    }
}

