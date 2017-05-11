using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;
using Android.Content;
using BusinessObjects;
using System.Collections.Generic;
using System;



namespace InspectionApp
{
    [Activity(Label = "Inspection", Theme = "@android:style/Theme.Material.Light", Icon = "@drawable/icon")]
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

