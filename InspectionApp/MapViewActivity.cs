using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using BusinessLayer;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InspectionApp
{
	[Activity(Label = "INSPECTION", Theme = "@style/MyCustomTheme", Icon = "@drawable/Icon")]
	public class MapViewActivity : Activity, IOnMapReadyCallback
	{
		List<AuditDetails> _auditDetailList;
		private Template manageTemplate = new Template();
		private GoogleMap Gmap;
		List<AuditTemplate> _auditTemplateList;
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
			LatLng latlng = null;
			this.Gmap = googleMap;
			Gmap.UiSettings.ZoomControlsEnabled = true;
			Gmap.UiSettings.ZoomGesturesEnabled = true;

			LatLngBounds.Builder builder = new LatLngBounds.Builder();

			_auditDetailList = manageTemplate.GetAllAudit();
			_auditTemplateList = manageTemplate.GetTemplates();
			foreach (AuditDetails auditDetail in _auditDetailList)
			{
				string[] gpsCoordinates = auditDetail.GPSCoordinate.Split(',');
				if (gpsCoordinates.Count() > 0)
				{
					latlng = new LatLng(Convert.ToDouble(gpsCoordinates[0]), Convert.ToDouble(gpsCoordinates[1]));
					MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetAlpha(auditDetail.Id);
					var templatedetails = _auditTemplateList.Find(t => t.Id == auditDetail.TemplateId);
					options.SetTitle(templatedetails.Name);
					options.SetSnippet(auditDetail.Location + " - " + DateTime.Today.ToShortDateString());

					Gmap.AddMarker(options);
					builder.Include(latlng);
				}
			}

			LatLngBounds bounds = builder.Build();
			CameraUpdate cu = CameraUpdateFactory.NewLatLngBounds(bounds, Resources.DisplayMetrics.WidthPixels, Resources.DisplayMetrics.HeightPixels, 120);
			googleMap.MoveCamera(cu);

			Gmap.InfoWindowClick += Gmap_InfoWindowClick;
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
		//private void MapOnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs markerClickEventArgs)
		//{
		//    markerClickEventArgs.Handled = true;
		//    Marker marker = markerClickEventArgs.Marker;

		//    var questionAnswer = new Intent(this, typeof(AuditQuesAnswersActivity));
		//    questionAnswer.PutExtra("auditId", marker.Alpha.ToString());
		//    questionAnswer.PutExtra("isNewAudit", false.ToString());
		//    StartActivity(questionAnswer);           
		//}
	}
}