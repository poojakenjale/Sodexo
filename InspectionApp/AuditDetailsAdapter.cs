using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using BusinessObjects;
using BusinessLayer;
using System.Linq;


namespace InspectionApp
{
    public class AuditDetailsAdapter : BaseAdapter
    {
        List<AuditDetails> _auditDetailList;
        List<AuditTemplate> _auditTemplateList;
        Activity _activity;

        private List<AuditDetails> _originalData;
        private Template manageTemplate = new Template();

        public AuditDetailsAdapter(Activity activity)
        {
            _activity = activity;
            manageTemplate.SetContext(_activity);
            FillAuditList();            
        }

        void FillAuditList()
        {
            _auditDetailList = manageTemplate.GetAllAudit();
            _auditTemplateList = manageTemplate.GetTemplates();
        }

        public override int Count
        {
            get { return _auditDetailList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            // could wrap a Contact in a Java.Lang.Object
            // to return it here if needed
            return null;
        }

        public override long GetItemId(int position)
        {
            return _auditDetailList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.AuditDetailItem, parent, false);
            var auditDescription = view.FindViewById<TextView>(Resource.Id.AuditDescription);
            var auditCreatedBy = view.FindViewById<TextView>(Resource.Id.CreatedBy);
            var auditCreatedOn = view.FindViewById<TextView>(Resource.Id.CreatedOn);
            //var btnViewAudit = view.FindViewById<Button>(Resource.Id.btnView);
            auditDescription.Text = string.Concat(_auditDetailList[position].Location, "-", _auditDetailList[position].UserId);


            if (_auditDetailList[position].Id == 0)
            {
                auditDescription = view.FindViewById<TextView>(Resource.Id.AuditDescription);
                //btnViewAudit = view.FindViewById<Button>(Resource.Id.btnView);
                auditDescription.Text = "No audits are created yet";
                //btnViewAudit.Visibility = ViewStates.Gone;
            }
            else
            {
                //auditDescription.Text = string.Concat(_auditDetailList[position].Location, "-", _auditDetailList[position].UserId);
               var templatedetails= _auditTemplateList.Find(t => t.Id == _auditDetailList[position].TemplateId);
                auditDescription.Text = string.Concat(templatedetails.Name, "-", _auditDetailList[position].Location);
                auditCreatedBy.Text =_activity.GetString(Resource.String.LoggedInUserName);
                auditCreatedOn.Text = DateTime.Today.ToShortDateString();
                //btnViewAudit.Tag = _auditDetailList[position].Id;
                //btnViewAudit.Click += delegate {
                //    btnOneClick((int)btnViewAudit.Tag);
                //};
            }
            return view;
        }

        void btnOneClick(int id)
        {
            var activity2 = new Intent(_activity, typeof(AuditQuesAnswersActivity));
            activity2.PutExtra("auditId", id.ToString());
            activity2.PutExtra("isNewAudit", false.ToString());
            _activity.StartActivity(activity2);
        }
        //public Filter Filter { get; private set; }
        //public override AuditDetails this[int position]
        //{
        //    get { return _auditDetailList[position]; }
        //}
        public override void NotifyDataSetChanged()
        {
            // If you are using cool stuff like sections
            // remember to update the indices here!
            base.NotifyDataSetChanged();
        }

        public void filter(string filter)
        {



            _originalData = manageTemplate.GetAllAudit();

            _auditDetailList = new List<AuditDetails>();
            if (_originalData != null && _originalData.Any())
            {
                // Compare constraint to all names lowercased. 
                // It they are contained they are added to results.

                string searchText = filter.ToLower();

                foreach (AuditDetails audit in _originalData)
                {
                    var templatedetails = _auditTemplateList.Find(t => t.Id == audit.TemplateId);
                    string filterOn = string.Concat(templatedetails.Name, " ", audit.Location," " ,_activity.GetString(Resource.String.LoggedInUserName));
                    if (templatedetails.Name.ToLower().Contains(searchText) || audit.Location.ToLower().Contains(searchText) || _activity.GetString(Resource.String.LoggedInUserName).ToLower().Contains(searchText))
                        _auditDetailList.Add(audit);
                }
            }
            
            NotifyDataSetChanged();         
        }


    }
}