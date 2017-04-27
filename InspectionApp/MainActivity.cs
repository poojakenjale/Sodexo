using Android.App;
using Android.Widget;
using Android.OS;
using BusinessLayer;
using Android.Content;

namespace InspectionApp
{
    [Activity(Label = "InspectionApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ImageButton FillAudit = FindViewById<ImageButton>(Resource.Id.FillAudit);
            Button ViewAudit = FindViewById<Button>(Resource.Id.ViewAudit);
            FillAudit.Click += FillAudit_Click;
            ViewAudit.Click += ViewAudit_Click;

        }

        private void ViewAudit_Click(object sender, System.EventArgs e)
        {
            var activity2 = new Intent(this, typeof(AuditQuesAnswersActivity));
            activity2.PutExtra("auditId", "1");
            StartActivity(activity2);
        }

        private void FillAudit_Click(object sender, System.EventArgs e)
        {

            StartActivity(typeof(AuditQuesAnswersActivity));
        }
    }
}

