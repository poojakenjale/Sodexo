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
using BusinessObjects;
using Android.Text.Method;
using Java.IO;
using System.Collections;
using System.IO;

namespace InspectionApp
{
    [Activity(Label = "AuditQuestionAnswers")]
    public class AuditQuesAnswersActivity : Activity
    {
		TextView fileQuestion1;
		TextView fileQuestion2;
		TextView fileQuestion3;
		TextView fileQuestion4;
		string image1 = string.Empty;
		string image2 = string.Empty;
		string image3 = string.Empty;
		string image4 = string.Empty;
        string auditID = string.Empty;
        string isNewAudit = string.Empty;

        private Template manageTemplate = new Template();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AuditQuestionAnswers);
            auditID = Intent.GetStringExtra("auditId") ?? "Data not available";
            isNewAudit = Intent.GetStringExtra("isNewAudit") ?? "Data not available";
            getDataForImageFile(Intent);

			TextView Question1 = FindViewById<TextView>(Resource.Id.labelQuestion1);
            TextView Question2 = FindViewById<TextView>(Resource.Id.labelQuestion2);
            TextView Question3 = FindViewById<TextView>(Resource.Id.labelQuestion3);
            TextView Question4 = FindViewById<TextView>(Resource.Id.labelQuestion4);

            EditText Answer1 = FindViewById<EditText>(Resource.Id.Answer1);
            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.rdoQuestion2);
            RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            RadioButton radioButtonYes = FindViewById<RadioButton>(Resource.Id.radio_Yes);
            RadioButton radioButtonNo = FindViewById<RadioButton>(Resource.Id.radio_No);
            var sp = FindViewById<Spinner>(Resource.Id.spinnerAnswer4);
            CheckBox checkbox = FindViewById<CheckBox>(Resource.Id.chkAnswer3);

            //Button SaveQuestion1 = FindViewById<Button>(Resource.Id.SaveQuestion1);
            //Button SaveQuestion2 = FindViewById<Button>(Resource.Id.SaveQuestion2);
            //Button SaveQuestion3 = FindViewById<Button>(Resource.Id.SaveQuestion3);
            Button SaveQuestion = FindViewById<Button>(Resource.Id.SaveQuestion);            

			//Camera code
			ImageButton Camera1 = FindViewById<ImageButton>(Resource.Id.Camera1);
			ImageButton Camera2 = FindViewById<ImageButton>(Resource.Id.Camera2);
			ImageButton Camera3 = FindViewById<ImageButton>(Resource.Id.Camera3);
			ImageButton Camera4 = FindViewById<ImageButton>(Resource.Id.Camera4);
			
			//Camera
			fileQuestion1 = FindViewById<TextView>(Resource.Id.fileQuestion1);
			fileQuestion2 = FindViewById<TextView>(Resource.Id.fileQuestion2);
			fileQuestion3 = FindViewById<TextView>(Resource.Id.fileQuestion3);
			fileQuestion4 = FindViewById<TextView>(Resource.Id.fileQuestion4);
			
			fileQuestion1.Visibility = ViewStates.Gone;
			fileQuestion2.Visibility = ViewStates.Gone;
			fileQuestion3.Visibility = ViewStates.Gone;
			fileQuestion4.Visibility = ViewStates.Gone;
			var intentQuestion = new Intent(this, typeof(CameraActivity));

			OpenCamera(Camera1, "Question1", intentQuestion);
			OpenCamera(Camera2, "Question2", intentQuestion);
			OpenCamera(Camera3, "Question3", intentQuestion);
			OpenCamera(Camera4, "Question4", intentQuestion);			

			Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerAnswer4);

            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Answers_array, Android.Resource.Layout.SimpleSpinnerItem);
            
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            manageTemplate.SetContext(this);            
            List<TemplateQuestions> Questions = new List<TemplateQuestions>();
            Questions = manageTemplate.GetTemplateQuestions();
            Question1.Text = Questions[0].QuestionDescription;
            Question2.Text = Questions[1].QuestionDescription;
            Question3.Text = Questions[2].QuestionDescription;
            Question4.Text = Questions[3].QuestionDescription;
            if (string.IsNullOrEmpty(isNewAudit))//view mode
            {
                SaveQuestion.Visibility = Android.Views.ViewStates.Gone;
				List<AuditAnswers> auditAnswers = manageTemplate.GetAuditAnswersByID(Convert.ToInt32(auditID));
				if (auditAnswers.Count > 0)
				{
					Answer1.Text = auditAnswers[0].Answer;
					if (auditAnswers[1].Answer.ToUpper() == "YES")
					{
						radioButtonYes.Checked = true;
					}
					else
					{
						radioButtonNo.Checked = true;
					}
					if (auditAnswers[2].Answer == "True")
					{
						checkbox.Checked = true;
					}
					else
					{
						checkbox.Checked = false;
					}
					ArrayAdapter answersAdapater = ArrayAdapter.CreateFromResource(
					this, Resource.Array.Answers_array, Android.Resource.Layout.SimpleSpinnerItem);
					spinner.SetSelection(answersAdapater.GetPosition(auditAnswers[3].Answer));

					//camera code
					if (auditAnswers[0].ImagePath != string.Empty)					
						GetImagePathForExisitng(auditAnswers[0].ImagePath, fileQuestion1, 1);

					if (auditAnswers[1].ImagePath != string.Empty)
						GetImagePathForExisitng(auditAnswers[1].ImagePath, fileQuestion2, 2);

					if (auditAnswers[2].ImagePath != string.Empty)
						GetImagePathForExisitng(auditAnswers[2].ImagePath, fileQuestion3, 3);

					if (auditAnswers[3].ImagePath != string.Empty)
						GetImagePathForExisitng(auditAnswers[3].ImagePath, fileQuestion4, 4);

				}
				else
				{					
					// Get images from gallery
					if (image1 != string.Empty)					
						ViewImage(fileQuestion1, image1, "Question1");
					if (image2 != string.Empty)
						ViewImage(fileQuestion2, image2, "Question2");
					if (image3 != string.Empty)
						ViewImage(fileQuestion3, image3, "Question3");
					if (image4 != string.Empty)
						ViewImage(fileQuestion4, image4, "Question4");
				}
				
			}
            else
            {
				// Get images from gallery
				if (image1 != string.Empty)
					ViewImage(fileQuestion1, image1, "Question1");
				if (image2 != string.Empty)
					ViewImage(fileQuestion2, image2, "Question2");
				if (image3 != string.Empty)
					ViewImage(fileQuestion3, image3, "Question3");
				if (image4 != string.Empty)
					ViewImage(fileQuestion4, image4, "Question4");

				SaveQuestion.Click += SaveQuestion_Click;
            }

            //Video
            //var intentVideo = new Intent(this, typeof(VideoActivity));
            //recordVideo.Click += delegate
            //{
            //    pushDataForImageFile(intentVideo);
            //    StartActivity(intentVideo);
            //};
        }

        private void SaveQuestion_Click(object sender, System.EventArgs e)
        {
            EditText Answer1 = FindViewById<EditText>(Resource.Id.Answer1);
            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.rdoQuestion2);
            RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            var sp = FindViewById<Spinner>(Resource.Id.spinnerAnswer4);
            CheckBox checkbox = FindViewById<CheckBox>(Resource.Id.chkAnswer3);
            //List<AuditDetails> auditTest = manageTemplate.GetAllAudit();
            //int id ;
            //if (auditTest.Count == 0)
            //{
            //     id = manageTemplate.SaveDefaultAudit();
            //}
            //else
            //{
            //    id = 1;
            //}
            //auditTest = manageTemplate.GetAllAudit();

            List<AuditAnswers> userAnswers = new List<AuditAnswers>();
            AuditAnswers answer1 = new AuditAnswers();
            answer1.AuditId = Convert.ToInt32(auditID);
            answer1.Answer = Answer1.Text;
            answer1.QuestionId = 1;
            answer1.ImagePath = "";// App._dir.ToString() + " " + fileQuestion1.Text;
            userAnswers.Add(answer1);

            AuditAnswers answer2 = new AuditAnswers();
            answer2.AuditId = Convert.ToInt32(auditID);
            answer2.Answer = radioButton.Text;
            answer2.QuestionId = 2;
			answer2.ImagePath = "";// App._dir.ToString() + " " + fileQuestion2.Text;
            userAnswers.Add(answer2);

            AuditAnswers answer3 = new AuditAnswers();
            answer3.AuditId = Convert.ToInt32(auditID);
            answer3.Answer = checkbox.Checked.ToString();
            answer3.QuestionId = 3;
			answer3.ImagePath = "";// App._dir.ToString() + " " + fileQuestion3.Text;
            userAnswers.Add(answer3);

            AuditAnswers answer4 = new AuditAnswers();
            answer4.AuditId = Convert.ToInt32(auditID);
            answer4.Answer = sp.SelectedItem.ToString();
            answer4.QuestionId = 4;
			answer4.ImagePath = "";// App._dir.ToString() + " " + fileQuestion4.Text;
            userAnswers.Add(answer4);

            
            manageTemplate.SaveAnswers(userAnswers);
            Toast.MakeText(this, "Answers saved successfully", ToastLength.Long);
            var intentmain = new Intent(this, typeof(MainActivity));
            StartActivity(intentmain);
        }

		private void pushDataForImageFile(Intent intent)
		{
			intent.PutExtra("Image1", image1);
			intent.PutExtra("Image2", image2);
			intent.PutExtra("Image3", image3);
			intent.PutExtra("Image4", image4);
		}

		private void getDataForImageFile(Intent intent)
		{
			image1 = intent.GetStringExtra("Image1") ?? "";
			image2 = intent.GetStringExtra("Image2") ?? "";
			image3 = intent.GetStringExtra("Image3") ?? "";
			image4 = intent.GetStringExtra("Image4") ?? "";
		}

		private void ViewImage(TextView txtView, string imageFile, string question)
		{
			txtView.Visibility = ViewStates.Visible;
			txtView.Text = imageFile;
			txtView.MovementMethod = LinkMovementMethod.Instance;
			txtView.Click += delegate {
				var intent = new Intent(this, typeof(CameraActivity));				
				pushDataForImageFile(intent);
				intent.PutExtra("viewImage", "1");
				intent.PutExtra("Question", question);
				StartActivity(intent);
			};
		}

		private void GetImagePathForExisitng(string imageFile, TextView txtView, int control)
		{
			txtView.Visibility = ViewStates.Visible;
			txtView.MovementMethod = LinkMovementMethod.Instance;
			txtView.Text = Path.GetFileName(imageFile);
			string question = string.Empty;
			switch (control)
			{
				case 1:
					image1 = txtView.Text;
					question = "Question1";
						break;
				case 2:
					image2 = txtView.Text;
					question = "Question2";
					break;
				case 3:
					image3 = txtView.Text;
					question = "Question3";
					break;
				case 4:
					image4 = txtView.Text;
					question = "Question4";
					break;
			}

			txtView.Click += delegate {
				var intent = new Intent(this, typeof(CameraActivity));
				pushDataForImageFile(intent);
				intent.PutExtra("viewImage", "1");
				intent.PutExtra("Question", question);
				StartActivity(intent);
			};
		}

		private void OpenCamera(ImageButton button, string question, Intent intentQuestion)
		{
			button.Click += delegate {
				intentQuestion.PutExtra("Question", question);
				pushDataForImageFile(intentQuestion);
				StartActivity(intentQuestion);
			};
		}
	}
}