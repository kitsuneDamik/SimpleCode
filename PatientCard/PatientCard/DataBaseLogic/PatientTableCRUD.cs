using PatientCard.DataBase;
using PatientCard.DataBaseInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PatientCard.DataBaseLogic
{
    public class PatientTableCRUD : ITableCRUD<PatientInfo>
    {
        public void CreateStringInTable(PatientInfo classObject)
        {
            using (var patient = new PatientContext())
            {
                patient.Patients.Add(classObject);
                patient.SaveChanges();
            }
        }

        public List<PatientInfo> GetAllStringsFromTable()
        {
            PatientContext patient = new PatientContext();
            return patient.Patients.ToList();
        }

        public PatientInfo GetModelFromTable(string id)
        {
            PatientContext context = new PatientContext();
            return context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveStringInTable(string id)
        {
            using (var patient = new PatientContext())
            {
                var model = GetModelFromTable(id);
                patient.Patients.Attach(model);
                patient.Patients.Remove(model);
                patient.SaveChanges();
            }
        }

        public void UpdateStringInTable(PatientInfo classObject)
        {
            using (var patient = new PatientContext())
            {
                patient.Patients.AddOrUpdate(classObject);
                patient.SaveChanges();
            }
        }
    }
}