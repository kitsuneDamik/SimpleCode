using PatientCard.DataBase;
using PatientCard.DataBaseInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PatientCard.DataBaseLogic
{
    public class DoctorTableCRUD : ITableCRUD<DoctorInfo>
    {
        public void CreateStringInTable(DoctorInfo classObject)
        {
            using (var doctor = new DoctorContext())
            {
                doctor.Doctors.Add(classObject);
                doctor.SaveChanges();
            }
        }

        public List<DoctorInfo> GetAllStringsFromTable()
        {
            var doctors = new DoctorContext();
            return doctors.Doctors.ToList();
        }

        public DoctorInfo GetModelFromTable(string id)
        {
            var doctors = new DoctorContext();
            return doctors.Doctors.FirstOrDefault(d => d.Id == id);
        }

        public void RemoveStringInTable(string id)
        {
            using (var doctor = new DoctorContext())
            {
                var model = GetModelFromTable(id);
                doctor.Doctors.Attach(model);
                doctor.Doctors.Remove(GetModelFromTable(id));
                doctor.SaveChanges();
            }
        }

        public void UpdateStringInTable(DoctorInfo classObject)
        {
            using (var doctor = new DoctorContext())
            {
                doctor.Doctors.AddOrUpdate(classObject);
                doctor.SaveChanges();
            }
        }
    }
}