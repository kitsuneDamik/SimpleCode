using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCard.DataBase
{
    public class Specialization
    {
        public string Id { get; set; }
        //[Index(IsUnique = true)] - no working
        public string DoctorSpecialization { get; set; }
    }

    public class SpecializationContext : DbContext
    {
        public SpecializationContext() : base("DbConnection")
        {} 
        
        public DbSet<Specialization> Specializations { get; set; }
    }
}