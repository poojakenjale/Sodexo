using System;
using DataLayer;

namespace DataLayer
{
    public class TemplateQuestions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int TemplateId { get; set; }
        public String QuestionDescription { get; set; }
    }
}