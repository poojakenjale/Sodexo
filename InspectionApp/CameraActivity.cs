using Android.App;
using Android.Widget;
using Android.OS;
using Java.IO;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Net;
using Android.Graphics;
using System;
using Android.Text.Method;
using BusinessObjects;

namespace InspectionApp
{
    [Activity(Label = "INSPECTION", Theme = "@android:style/Theme.Material.Light", Icon = "@drawable/icon")]
    public class CameraActivity : Activity
	{
		ImageView _imageCamera;
		Button back;
		string viewImage = string.Empty;
		string image1 = string.Empty;
		string image2 = string.Empty;
		string image3 = string.Empty;
		string image4 = string.Empty;
		string Question = string.Empty;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Camera);
			Button takePicture = FindViewById<Button>(Resource.Id.takePicture);
			back = FindViewById<Button>(Resource.Id.back);
			takePicture.Visibility = Android.Views.ViewStates.Visible;
			_imageCamera = FindViewById<ImageView>(Resource.Id.imageCamera);
			viewImage = Intent.GetStringExtra("viewImage") ?? "";
			image1 = Intent.GetStringExtra("Image1") ?? "";
			image2 = Intent.GetStringExtra("Image2") ?? "";
			image3 = Intent.GetStringExtra("Image3") ?? "";
			image4 = Intent.GetStringExtra("Image4") ?? "";
			Question = Intent.GetStringExtra("Question") ?? "";

			if (viewImage != "1")
			{
				if (IsThereAnAppToTakePictures())
				{
					CreateDirectoryForPictures();
					takePicture.Click += TakeAPicture;
				}
			}
			else
			{
				string fileName = (Question == "Question1") ? image1 : (Question == "Question2") ? image2 : (Question == "Question3") ? image3 : image4;
				App._file = new File(App._dir, fileName);
				_imageCamera.SetImageURI(Android.Net.Uri.FromFile(App._file));
				takePicture.Visibility = Android.Views.ViewStates.Gone;

				back.Click += delegate {
					var second = new Intent(this, typeof(AuditQuesAnswersActivity));
					pushDataForImageFile(second);
					StartActivity(second);
				};
			}
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Android.OS.Environment.GetExternalStoragePublicDirectory(
					Android.OS.Environment.DirectoryPictures), "InspectionApp");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			App._file = new File(App._dir, String.Format("myPhoto_{0}_{1}.jpg", Guid.NewGuid(), Question));
			string imageFileName = Android.Net.Uri.FromFile(App._file).LastPathSegment;
			switch (Question)
			{
				case "Question1":
					image1 = imageFileName;
					break;
				case "Question2":
					image2 = imageFileName;
					break;
				case "Question3":
					image3 = imageFileName;
					break;
				case "Question4":
					image4 = imageFileName;
					break;
			}
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
			pushDataForImageFile(intent);
			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// Make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// Display in ImageView. We will resize the bitmap to fit the display.
			// Loading the full sized image will consume to much memory
			// and cause the application to crash.

			int height = Resources.DisplayMetrics.HeightPixels;
			int width = _imageCamera.Height;
			App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
			if (App.bitmap != null)
			{
				_imageCamera.SetImageBitmap(App.bitmap);
				App.bitmap = null;
			}

			back.Click += delegate {
				var second = new Intent(this, typeof(AuditQuesAnswersActivity));
				pushDataForImageFile(second);
				StartActivity(second);
			};

			// Dispose of the Java side bitmap.
			GC.Collect();
		}

		private void pushDataForImageFile(Intent intent)
		{
			intent.PutExtra("Image1", image1);
			intent.PutExtra("Image2", image2);
			intent.PutExtra("Image3", image3);
			intent.PutExtra("Image4", image4);
		}
	}
}