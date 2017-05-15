using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;
using Android.Content;
using BusinessObjects;
using System.Collections.Generic;
using System;
using Android;

namespace InspectionApp
{
    [Activity(Label = "INSPECTION", Theme = "@style/MyCustomTheme", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Template manageTemplate = new Template();

        private Boolean canAccessLocation()
        {
            return (hasPermission(Manifest.Permission.AccessFineLocation));
        }

        private Boolean canAccessCamera()
        {
            return (hasPermission(Manifest.Permission.Camera));
        }

        private Boolean hasPermission(String perm)
        {
            return (CheckSelfPermission(perm) == Android.Content.PM.Permission.Granted);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);
            if (!canAccessLocation() || !canAccessCamera())
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.Camera }, 1);
            }



            ImageButton FillAudit = FindViewById<ImageButton>(Resource.Id.FillAudit);
            ListView auditListingView = FindViewById<ListView>(Resource.Id.AuditListingView);
            ImageButton btnMap = FindViewById<ImageButton>(Resource.Id.btnMap);
            FillAudit.Click += FillAudit_Click;
            btnMap.Click += BtnMap_Click;

            manageTemplate.SetContext(this);
            List<AuditDetails> auditTest = manageTemplate.GetAllAudit();

            var auditDetailsAdapter = new AuditDetailsAdapter(this);    
            auditListingView.Adapter = auditDetailsAdapter;
            //List<AuditDetails> _auditDetailList = manageTemplate.GetAllAudit();
            //if(_auditDetailList.Count ==0)
            //{
            //    auditListingView.setVisibility(View.INVISIBLE);
            //}
            auditListingView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                long id = auditDetailsAdapter.GetItemId(e.Position);
                var activity2 = new Intent(this, typeof(AuditQuesAnswersActivity));
                activity2.PutExtra("auditId", id.ToString());
                activity2.PutExtra("isNewAudit", false.ToString());
                this.StartActivity(activity2);
            };            
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

