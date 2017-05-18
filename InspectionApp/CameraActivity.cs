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
    [Activity(Label = "INSPECTION", Theme = "@style/MyCustomTheme", Icon = "@drawable/icon")]
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
		string auditID = string.Empty;
		string isNewAudit = string.Empty;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Camera);
			//Button takePicture = FindViewById<Button>(Resource.Id.takePicture);
			back = FindViewById<Button>(Resource.Id.back);
			//takePicture.Visibility = Android.Views.ViewStates.Visible;
			_imageCamera = FindViewById<ImageView>(Resource.Id.imageCamera);
			viewImage = Intent.GetStringExtra("viewImage") ?? "";
			image1 = Intent.GetStringExtra("Image1") ?? "";
			image2 = Intent.GetStringExtra("Image2") ?? "";
			image3 = Intent.GetStringExtra("Image3") ?? "";
			image4 = Intent.GetStringExtra("Image4") ?? "";
			Question = Intent.GetStringExtra("Question") ?? "";
			auditID = Intent.GetStringExtra("auditId");
			isNewAudit = Intent.GetStringExtra("isNewAudit");

			if (viewImage != "1")
			{
				back.Visibility = Android.Views.ViewStates.Gone;
				if (IsThereAnAppToTakePictures())
				{
					CreateDirectoryForPictures();
					TakeAPicture();
				}
			}
			else
			{
				string fileName = (Question == "Question1") ? image1 : (Question == "Question2") ? image2 : (Question == "Question3") ? image3 : image4;
				AppFile._file = new File(AppFile._dir, fileName);
				_imageCamera.SetImageURI(Android.Net.Uri.FromFile(AppFile._file));
				//takePicture.Visibility = Android.Views.ViewStates.Gone;

				back.Click += delegate {
					var second = new Intent(this, typeof(AuditQuesAnswersActivity));
					pushDataForImageFile(second,Result.Ok, Question);
					StartActivity(second);
				};
			}
		}

		private void CreateDirectoryForPictures()
		{
			AppFile._dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "Inspection");
			if (!AppFile._dir.Exists())
			{
				AppFile._dir.Mkdirs();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			AppFile._file = new File(AppFile._dir, String.Format("myPhoto_{0}_{1}.jpg", Guid.NewGuid(), Question));
			string imageFileName = Android.Net.Uri.FromFile(AppFile._file).LastPathSegment;
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
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(AppFile._file));
			//pushDataForImageFile(intent);
			StartActivityForResult(intent, 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// Make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(AppFile._file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// Display in ImageView. We will resize the bitmap to fit the display.
			// Loading the full sized image will consume to much memory
			// and cause the application to crash.

			int height = Resources.DisplayMetrics.HeightPixels;
			int width = _imageCamera.Height;
			AppFile.bitmap = AppFile._file.Path.LoadAndResizeBitmap(width, height);
			if (AppFile.bitmap != null)
			{
				_imageCamera.SetImageBitmap(AppFile.bitmap);
				AppFile.bitmap = null;
			}

			var second = new Intent(this, typeof(AuditQuesAnswersActivity));
			pushDataForImageFile(second, resultCode, Question);
			StartActivity(second);

			//back.Click += delegate {
			//	var second = new Intent(this, typeof(AuditQuesAnswersActivity));
			//	pushDataForImageFile(second, resultCode, Question);
			//	StartActivity(second);
			//};

			// Dispose of the Java side bitmap.
			GC.Collect();
		}

		private void pushDataForImageFile(Intent intent, Result resultCode, string question)
		{
			if (resultCode == Result.Canceled)
			{
				switch (question)
				{
					case "Question1":
						image1 = string.Empty;
						break;
					case "Question2":
						image2 = string.Empty;
						break;
					case "Question3":
						image3 = string.Empty;
						break;
					case "Question4":
						image4 = string.Empty;
						break;

				}
			}
			
			intent.PutExtra("Image1", image1);
			intent.PutExtra("Image2", image2);
			intent.PutExtra("Image3", image3);
			intent.PutExtra("Image4", image4);
			intent.PutExtra("auditId", auditID);
			intent.PutExtra("isNewAudit", isNewAudit);

		}
	}
}