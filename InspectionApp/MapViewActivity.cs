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
using BusinessLayer;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using BusinessObjects;


namespace InspectionApp
{
    [Activity(Label = "INSPECTION", Theme = "@style/MyCustomTheme", Icon = "@drawable/icon")]
    public class MapViewActivity : Activity, IOnMapReadyCallback
    {
        List<AuditDetails> _auditDetailList;
        List<AuditTemplate> _auditTemplates;
        private Template manageTemplate = new Template();
        private GoogleMap Gmap;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MapView);
            Button btnBack = FindViewById<Button>(Resource.Id.btnBack);
            manageTemplate.SetContext(this);
            btnBack.Click += BtnBack_Click;
            SetupMap();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.Gmap = googleMap;
            LatLng latlng = null;
            _auditDetailList = manageTemplate.GetAllAudit();
            _auditTemplates = manageTemplate.GetTemplates();
            foreach (AuditDetails auditDetail in _auditDetailList)
            {
                string [] gpsCoordinates = auditDetail.GPSCoordinate.Split(',');
                if (gpsCoordinates.Count() > 0)
                {
                    latlng = new LatLng(Convert.ToDouble(gpsCoordinates[0]), Convert.ToDouble(gpsCoordinates[1]));
                    var templatedetails = _auditTemplates.Find(t => t.Id == auditDetail.TemplateId);
                    string title = string.Concat(templatedetails.Name, "-", auditDetail.Location);
                    MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle(title).SetAlpha(auditDetail.Id);
                    Gmap.AddMarker(options);
                }            
            }
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 8);
            Gmap.MoveCamera(camera);
            Gmap.UiSettings.ZoomControlsEnabled = true;
            Gmap.UiSettings.ZoomGesturesEnabled = true;
            
            Gmap.InfoWindowClick += Gmap_InfoWindowClick; ;
        }

        private void Gmap_InfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            
            Marker marker = e.Marker;

            var questionAnswer = new Intent(this, typeof(AuditQuesAnswersActivity));
            questionAnswer.PutExtra("auditId", marker.Alpha.ToString());
            questionAnswer.PutExtra("isNewAudit", false.ToString());
            StartActivity(questionAnswer);
        }

        private void SetupMap()
        {
            if (Gmap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }
        private void MapOnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs markerClickEventArgs)
        {
            markerClickEventArgs.Handled = true;
            Marker marker = markerClickEventArgs.Marker;

            var questionAnswer = new Intent(this, typeof(AuditQuesAnswersActivity));
            questionAnswer.PutExtra("auditId", marker.Alpha.ToString());
            questionAnswer.PutExtra("isNewAudit", false.ToString());
            StartActivity(questionAnswer);           
        }
    }
}