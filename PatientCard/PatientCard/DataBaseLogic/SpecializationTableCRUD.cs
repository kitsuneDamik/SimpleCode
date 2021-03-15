using PatientCard.DataBase;
using PatientCard.DataBaseInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PatientCard.DataBaseLogic
{
    public class SpecializationTableCRUD : ITableCRUD<Specialization>
    {
        public void CreateStringInTable(Specialization classObject)
        {
            using (var specialization = new SpecializationContext())
            {
                specialization.Specializations.Add(classObject);
                specialization.SaveChanges();
            }
        }

        public List<Specialization> GetAllStringsFromTable()
        {
            SpecializationContext specialization = new SpecializationContext();
            return specialization.Specializations.ToList();
        }

        public Specialization GetModelFromTable(string id)
        {
            SpecializationContext specialization = new SpecializationContext();
            return specialization.Specializations.FirstOrDefault(s => s.Id == id);
        }

        public void RemoveStringInTable(string id)
        {
            using (var specialization = new SpecializationContext())
            {
                var model = GetModelFromTable(id);
                specialization.Specializations.Attach(model);
                specialization.Specializations.Remove(model);
                specialization.SaveChanges();
            }
        }

        public void UpdateStringInTable(Specialization classObject)
        {
            using (var specialization = new SpecializationContext())
            {
                specialization.Specializations.AddOrUpdate(classObject);
                specialization.SaveChanges();
            }
        }
    }
}