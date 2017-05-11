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
    [Activity(Label = "MapViewActivity")]
    public class MapViewActivity : Activity, IOnMapReadyCallback
    {
        List<AuditDetails> _auditDetailList;
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
            foreach (AuditDetails auditDetail in _auditDetailList)
            {
                string [] gpsCoordinates = auditDetail.GPSCoordinate.Split(',');
                latlng = new LatLng(Convert.ToDouble(gpsCoordinates[0]), Convert.ToDouble(gpsCoordinates[1]));
                MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle(auditDetail.Location);
                Gmap.AddMarker(options);
            }
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
            Gmap.MoveCamera(camera);
        }
        private void SetupMap()
        {
            if (Gmap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }
    }
}