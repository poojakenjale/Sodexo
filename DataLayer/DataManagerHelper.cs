using Android.Database.Sqlite;
using Android.Content;

namespace DataLayer
{
    public class DataManagerHelper : SQLiteOpenHelper
    {
        // specifies the database name
        private const string DatabaseName = "Sodexo";
        //specifies the database version (increment this number each time you make database related changes)
        private const int DatabaseVersion = 1;

        public DataManagerHelper(Context context)
            : base(context, DatabaseName, null, DatabaseVersion)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            //create database tables
            //db.ExecSQL(@"
            //            CREATE TABLE IF NOT EXISTS Customer (
            //                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
            //                FirstName       TEXT NOT NULL,
            //                LastName        TEXT NOT NULL )");

            ////create database indexes
            //db.ExecSQL(@"CREATE INDEX IF NOT EXISTS FIRSTNAME_CUSTOMER ON CUSTOMER (FIRSTNAME)");

            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS AuditTemplate (
                            Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name       TEXT NOT NULL,
                            Type        TEXT NOT NULL )");

            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS TemplateQuestions (
                            Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                            TemplateId       INTEGER NOT NULL,
                            QuestionDescription        TEXT NOT NULL )");

            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS AuditAnswers (
                            AuditId          INTEGER NOT NULL,
                            QuestionId       INTEGER NOT NULL,
                            Answer           TEXT NOT NULL,
                            ImagePath        TEXT  NULL,
                            VideoPath        TEXT NULL,
                            constraint PK_AuditAnswers primary key (AuditId, QuestionId))");
            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS AuditDetails (
                            Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserId       TEXT NOT NULL,
                            TemplateId           INTEGER NOT NULL,
                            Location        TEXT   NOT NULL,
                            GPSCoordinate        TEXT  NOT NULL)");
            db.ExecSQL(@"
                        CREATE TABLE IF NOT EXISTS Location (
                            Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name       TEXT NOT NULL)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            if (oldVersion < 2)
            {
                //perform any database upgrade tasks for versions prior to  version 2              
            }
            if (oldVersion < 3)
            {
                //perform any database upgrade tasks for versions prior to  version 3
            }
        }
    }
}