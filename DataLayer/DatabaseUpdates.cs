using Android.Content;
using BusinessObjects;
using SQLite;
using System.Collections.Generic;
using System.Linq;

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
				return db.Insert(addtemp);
			}
		}

		public long AddAuditQuestion(TemplateQuestions addtemp)
		{
			using (var db = new SQLiteConnection(_helper.WritableDatabase.Path))
			{
				return db.Insert(addtemp);
			}
		}

		public List<AuditAnswers> GetAuditAnswersByID(int id)
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<AuditAnswers>().Where(a => a.AuditId == id).ToList();
			}
		}

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
				return database.Insert(auditdetail);
			}
		}

		public int SaveAnswer(AuditAnswers answer)
		{
			using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
			{
				return database.Insert(answer);
			}
		}

		//retrieve a specific user by querying against their first name
		public List<AuditTemplate> GetAuditTemplates()
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<AuditTemplate>().ToList();
			}
		}

		//retrieve a specific user by querying against their first name
		public AuditTemplate GetAuditTemplate(string name)
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<AuditTemplate>().FirstOrDefault(u => u.Name == name);
			}
		}

		//retrieve a list of all customers
		public int GetTemplateCount()
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<AuditTemplate>().ToList().Count();
			}
		}

		public List<TemplateQuestions> GetTemplateQuestions()
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<TemplateQuestions>().ToList();
			}
		}

		public List<AuditDetails> GetAllAudits()
		{
			using (var database = new SQLiteConnection(_helper.ReadableDatabase.Path))
			{
				return database.Table<AuditDetails>().ToList();
			}
		}
	}
}