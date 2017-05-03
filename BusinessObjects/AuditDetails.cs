//using DataLayer;
using System;

namespace BusinessObjects
 {
    public class AuditDetails
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TemplateId { get; set; }
        public string Location { get; set; }
        public string GPSCoordinate { get; set; }
    }
}