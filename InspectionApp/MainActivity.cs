using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;
using Android.Content;
using BusinessObjects;
using System.Collections.Generic;
using Android.Database;

namespace InspectionApp
{
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Template manageTemplate = new Template();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);
            ImageButton FillAudit = FindViewById<ImageButton>(Resource.Id.FillAudit);       
            ListView auditListingView = FindViewById<ListView>(Resource.Id.AuditListingView);
            FillAudit.Click += FillAudit_Click;

            manageTemplate.SetContext(this);            
           var auditDetailsAdapter = new AuditDetailsAdapter(this);
           auditListingView.Adapter = auditDetailsAdapter;

        }

        private void FillAudit_Click(object sender, System.EventArgs e)
        {

            StartActivity(typeof(AuditQuesAnswersActivity));
        }
    }
}

