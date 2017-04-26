using System;

using System;
namespace DataLayer
{
    public class AuditTemplate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public String Name { get; set; }
        public String Type { get; set; }
    }
}