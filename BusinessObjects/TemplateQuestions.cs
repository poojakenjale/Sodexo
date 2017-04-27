using SQLite;
using System;


namespace BusinessObjects
{
    public class TemplateQuestions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public String QuestionDescription { get; set; }
    }
}