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
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Template manageTemplate = new Template();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ImageButton FillAudit = FindViewById<ImageButton>(Resource.Id.FillAudit);
            Button ViewAudit = FindViewById<Button>(Resource.Id.ViewAudit);
            ListView listView = FindViewById<ListView>(Resource.Id.AuditListing);
            FillAudit.Click += FillAudit_Click;
            ViewAudit.Click += ViewAudit_Click;
            manageTemplate.SetContext(this);
            List<AuditDetails> auditList = new List<AuditDetails>();
            auditList = manageTemplate.GetAllAudit();
            string[] auditNames= new string[auditList.Count];
            for(int i=0; i<auditList.Count;i++)
            {
                auditNames[i] = auditList[i].Location + "-" + auditList[i].UserId;
            }

           
            ArrayAdapter test= new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, auditNames);
            
            listView.Adapter = test;
        }

        private void ViewAudit_Click(object sender, System.EventArgs e)
        {
            var activity2 = new Intent(this, typeof(AuditQuesAnswersActivity));
            activity2.PutExtra("auditId", "1");
            StartActivity(activity2);
        }

        private void FillAudit_Click(object sender, System.EventArgs e)
        {

            StartActivity(typeof(AuditDetails));
        }
    }
}

