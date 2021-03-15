using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCard.DataBase
{
    public class VisitHistory
    {
        public string Id { get; set; }
        //[ForeignKey("DoctorInfo")] - no working
        public int DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public string Complaints { get; set; }
        public DateTime DateOfVisit { get; set; }
        //[ForeignKey("PatientInfo")] - no working
        public int PatientId { get; set; }
    }

    public class VisitHistoryContext : DbContext
    {
        public VisitHistoryContext() : base("DbConnection")
        { }

        public DbSet<VisitHistory> Visits { get; set; }
    }
}