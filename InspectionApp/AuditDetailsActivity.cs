using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer;
using BusinessObjects;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace InspectionApp
{
    [Activity(Label = "AuditDetails")]
    public class AuditDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Template template = new Template();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AuditDetails);
            // Create your application here
            template.SetContext(this);
            List<AuditTemplate> templateList = template.GetTemplates();

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, templateList);
            adapter.SetDropDownViewResource
                (Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner spnTemplate = FindViewById<Spinner>(Resource.Id.spnTemplates);
            spnTemplate.Adapter = adapter;            
        }
    }
}