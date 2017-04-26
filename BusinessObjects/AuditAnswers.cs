using System;


namespace DataLayer
{
    public class AuditAnswers
    {
        public int AuditId { get; set; }
        public int QuestionId { get; set; }
        public String Answer { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
    }
}