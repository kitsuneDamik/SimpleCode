using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCard.DataBase
{
    public class DoctorInfo
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        //[ForeignKey("Specialization")] - no working
        public int SpecializationId { get; set; }
    }

    public class DoctorContext : DbContext
    {
        public DoctorContext() : base("DbConnection")
        { }

        public DbSet<DoctorInfo> Doctors { get; set; }
    }

}