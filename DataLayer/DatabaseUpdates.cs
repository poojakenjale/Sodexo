using Android.Content;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace DataLayer
{
    public class DatabaseUpdates
    {
        private DataManagerHelper _helper;

        public void SetContext(Context context)
        {
            if (context != null)
            {
                _helper = new DataManagerHelper(context);
            }

        }

       
        public int AddTemplate(AuditTemplate addtemp)
        {
            using (var db = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                try
                {
                    return db.Insert(addtemp);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public long AddAuditQuestion(TemplateQuestions addtemp)
        {
            using (var db = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                try
                {
                    return db.Insert(addtemp);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<AuditAnswers> GetAuditAnswersByID(int id)
        {
            using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
            {
                try
                {
                    return database.Table<AuditAnswers>().Where(a => a.AuditId == id).ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //public int SaveDefaultAudit()
        //{
        //    AuditDetails audit = new AuditDetails();
        //    //audit.Id = 1;
        //    audit.TemplateId = 1;
        //    audit.Location = "CGI Mumbai";
        //    audit.UserId = "CGI Admin";
        //    audit.GPSCoordinate = "10,20";
        //    return SaveAuditDetail(audit);
        //}
        public void SaveAudit(string[] answers)
        {
            AuditDetails audit = new AuditDetails();
            //audit.Id = 1;
            audit.TemplateId = 1;
            audit.Location = "CGI Mumbai";
            audit.UserId = "CGI Admin";
            audit.GPSCoordinate = "10,20";
            int auditid = SaveAuditDetail(audit);

            for (int i = 0; i < answers.Length; i++)
            {

                AuditAnswers answer = new AuditAnswers();
                answer.AuditId = auditid;
                answer.Answer = answers[i];
                answer.QuestionId = i + 1;
                SaveAnswer(answer);
            }
        }



        //Get the total number of orders for a specific customer
        public int SaveAuditDetail(AuditDetails auditdetail)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                try
                {
                    return database.InsertOrReplace(auditdetail);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int SaveAnswer(AuditAnswers answer)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                try
                {
                    return database.InsertOrReplace(answer);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //retrieve a specific user by querying against their first name
        public AuditTemplate GetAuditTemplate(string name)
        {
            using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
            {
                try
                {
                    return database.Table<AuditTemplate>().FirstOrDefault(u => u.Name == name);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }




        //retrieve a list of all customers
        public int GetTemplateCount()
        {
            using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
            {
                try
                {
                    return database.Table<AuditTemplate>().ToList().Count();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<TemplateQuestions> GetTemplateQuestions()
        {
            using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
            {
                try
                {
                    return database.Table<TemplateQuestions>().ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
    
}