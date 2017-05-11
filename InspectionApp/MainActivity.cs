using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;
using Android.Content;
using BusinessObjects;
using System.Collections.Generic;
using System;
using Android.Gms.Maps;



namespace InspectionApp
{
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Template manageTemplate = new Template();
        private GoogleMap Gmap;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);
            ImageButton FillAudit = FindViewById<ImageButton>(Resource.Id.FillAudit);
            ListView auditListingView = FindViewById<ListView>(Resource.Id.AuditListingView);
            Button btnMap = FindViewById<Button>(Resource.Id.btnMap);
            FillAudit.Click += FillAudit_Click;
            btnMap.Click += BtnMap_Click;

            manageTemplate.SetContext(this);
            List<AuditDetails> auditTest = manageTemplate.GetAllAudit();

            var auditDetailsAdapter = new AuditDetailsAdapter(this);
            auditListingView.Adapter = auditDetailsAdapter;

        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MapViewActivity));
        }

        private void FillAudit_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AuditDetailsActivity));
        }
    }
}

