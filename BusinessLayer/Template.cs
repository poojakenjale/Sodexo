using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessObjects;
using Android.Content;

namespace BusinessLayer
{
    public class Template
    {
        private DatabaseUpdates dbUpdates = new DatabaseUpdates();


        public void SetDefaultTemplate()
        {
            AuditTemplate foodAudit = new AuditTemplate();

            List<TemplateQuestions> Questions = new List<TemplateQuestions>();
            Questions = dbUpdates.GetTemplateQuestions();
            AuditTemplate temp1 = dbUpdates.GetAuditTemplate("Food");
            try
            {
                if (dbUpdates.GetTemplateCount() == 0)
                {
                    foodAudit.Name = "Food Audit";
                    foodAudit.Type = "Food";
                    int id = dbUpdates.AddTemplate(foodAudit);

                    TemplateQuestions question1 = new TemplateQuestions();
                    question1.TemplateId = id;
                    question1.QuestionDescription = "Is there sufficient storage space? ";
                    dbUpdates.AddAuditQuestion(question1);
                    Questions = dbUpdates.GetTemplateQuestions();

                    TemplateQuestions question2 = new TemplateQuestions();
                    question2.TemplateId = id;
                    question2.QuestionDescription = "Are chemicals and cleaning products stored away from food storage areas?";
                    dbUpdates.AddAuditQuestion(question2);
                    Questions = dbUpdates.GetTemplateQuestions();

                    TemplateQuestions question3 = new TemplateQuestions();
                    question3.TemplateId = id;
                    question3.QuestionDescription = "Is all packaging in good condition?";
                    dbUpdates.AddAuditQuestion(question3);

                    TemplateQuestions question4 = new TemplateQuestions();
                    question4.TemplateId = id;
                    question4.QuestionDescription = "Have appropriate corrective actions been taken and recorded?";
                    dbUpdates.AddAuditQuestion(question4);
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void SetContext(Context context)
        {
            dbUpdates.SetContext(context);

        }

        public List<TemplateQuestions> GetTemplateQuestions()
        {
            return dbUpdates.GetTemplateQuestions();
        }
        public List<AuditAnswers> GetAuditAnswersByID(int id)
        {
            return dbUpdates.GetAuditAnswersByID(id);
        }
        public void SaveAnswers(List<AuditAnswers> questionAnswers)
        {
            foreach (AuditAnswers ans in questionAnswers)
            {
                dbUpdates.SaveAnswer(ans);
            }
        }

        public int SaveDefaultAudit()
        {
            AuditDetails audit = new AuditDetails();
            //audit.Id = 1;
            audit.TemplateId = 1;
            audit.Location = "CGI Mumbai";
            audit.UserId = "CGI Admin";
            audit.GPSCoordinate = "10,20";
            return dbUpdates.SaveAuditDetail(audit);
        }
    }
}
