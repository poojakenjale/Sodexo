using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessObjects;
using Android.Content;
using Android.Database;

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
                    foodAudit.Name = "Appearance Care";
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

                    AuditTemplate foodAudit4 = new AuditTemplate();
                    foodAudit4.Name = "Pest Inspection";
                    foodAudit4.Type = "Food";
                     dbUpdates.AddTemplate(foodAudit4);

                    AuditTemplate foodAudit5 = new AuditTemplate();
                    foodAudit5.Name = "RestRoom Inspection";
                    foodAudit5.Type = "Food";
                     dbUpdates.AddTemplate(foodAudit5);

                    AuditTemplate foodAudit1 = new AuditTemplate();
                    foodAudit1.Name = "QA Audio Visual Inspection";
                    foodAudit1.Type = "Food";
                     dbUpdates.AddTemplate(foodAudit1);

                    AuditTemplate foodAudit2 = new AuditTemplate();
                    foodAudit2.Name = "Chevron Janitorial Services Survey Form v8";
                    foodAudit2.Type = "Food";
                     dbUpdates.AddTemplate(foodAudit2);

                    AuditTemplate foodAudit3 = new AuditTemplate();
                    foodAudit3.Name = "Chevron Site Services - Service Quality Inspection";
                    foodAudit3.Type = "Food";
                     dbUpdates.AddTemplate(foodAudit3);


                    AuditTemplate foodAudit6 = new AuditTemplate();
                    foodAudit6.Name = "Meeting Room Inspection ";
                    foodAudit6.Type = "Food";
                    dbUpdates.AddTemplate(foodAudit6);
                }
               

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<AuditTemplate> GetTemplates()
        {
            return dbUpdates.GetAuditTemplates();
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

        public int SaveAudit(AuditDetails audit)
        { 
            return dbUpdates.SaveAuditDetail(audit);
        }
        public int SaveDefaultAudit()
        {
            AuditDetails audit1 = new AuditDetails();
            //audit.Id = 1;
            audit1.TemplateId = 1;
            audit1.Location = "CGI Mumbai";
            audit1.UserId = "CGI Admin";
            audit1.GPSCoordinate = "20,20";
            int audit1Id = dbUpdates.SaveAuditDetail(audit1);
            //AuditDetails audit = new AuditDetails();
            ////audit.Id = 1;
            //audit.TemplateId = 1;
            //audit.Location = "Seepz Mumbai";
            //audit.UserId = "Seepz Admin";
            //audit.GPSCoordinate = "20,20";
            //int idtest= dbUpdates.SaveAuditDetail(audit);
            return audit1Id;
        }

        public List<AuditDetails> GetAllAudit()
        {
            return dbUpdates.GetAllAudits();
        }
    }
}
