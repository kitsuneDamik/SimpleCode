using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PatientCard.DataBase
{
    public class PatientInfo
    {
        public string Id { get; set; }
        public string Iin { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class PatientContext : DbContext
    {
        public PatientContext() : base("DbConnection")
        { }

        public DbSet<PatientInfo> Patients { get; set; }
    }
}