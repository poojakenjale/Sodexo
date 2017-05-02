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

namespace InspectionApp
{
    [Activity(Label = "AuditQuestionAnswers")]
    public class AuditQuesAnswersActivity : Activity
    {
        private Template manageTemplate = new Template();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AuditQuestionAnswers);
            string auditID = Intent.GetStringExtra("auditId") ?? "Data not available";


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
            Button Clear = FindViewById<Button>(Resource.Id.Clear);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerAnswer4);

            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Answers_array, Android.Resource.Layout.SimpleSpinnerItem);
            
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            manageTemplate.SetContext(this);
            manageTemplate.SetDefaultTemplate();
            List<TemplateQuestions> Questions = new List<TemplateQuestions>();
            Questions = manageTemplate.GetTemplateQuestions();
            Question1.Text = Questions[0].QuestionDescription;
            Question2.Text = Questions[1].QuestionDescription;
            Question3.Text = Questions[2].QuestionDescription;
            Question4.Text = Questions[3].QuestionDescription;
            if (auditID == "1")
            {
                SaveQuestion.Visibility = Clear.Visibility = Android.Views.ViewStates.Gone;
                List<AuditAnswers> auditAnswers = manageTemplate.GetAuditAnswersByID(1);
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
                }
            }
            else
            {
                SaveQuestion.Click += SaveQuestion_Click;
            }

        }

        private void SaveQuestion_Click(object sender, System.EventArgs e)
        {
            EditText Answer1 = FindViewById<EditText>(Resource.Id.Answer1);
            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.rdoQuestion2);
            RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
            var sp = FindViewById<Spinner>(Resource.Id.spinnerAnswer4);
            CheckBox checkbox = FindViewById<CheckBox>(Resource.Id.chkAnswer3);

            int id = manageTemplate.SaveDefaultAudit();

            List<AuditAnswers> userAnswers = new List<AuditAnswers>();
            AuditAnswers answer1 = new AuditAnswers();
            answer1.AuditId = id;
            answer1.Answer = Answer1.Text;
            answer1.QuestionId = 1;
            userAnswers.Add(answer1);

            AuditAnswers answer2 = new AuditAnswers();
            answer2.AuditId = id;
            answer2.Answer = radioButton.Text;
            answer2.QuestionId = 2;
            userAnswers.Add(answer2);

            AuditAnswers answer3 = new AuditAnswers();
            answer3.AuditId = id;
            answer3.Answer = checkbox.Checked.ToString();
            answer3.QuestionId = 3;
            userAnswers.Add(answer3);

            AuditAnswers answer4 = new AuditAnswers();
            answer4.AuditId = id;
            answer4.Answer = sp.SelectedItem.ToString();
            answer4.QuestionId = 4;
            userAnswers.Add(answer4);

            
            manageTemplate.SaveAnswers(userAnswers);
        }
    }
}