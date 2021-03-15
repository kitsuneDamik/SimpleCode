using PatientCard.DataBase;
using PatientCard.DataBaseInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PatientCard.DataBaseLogic
{
    public class VisitHistoryTableCRUD : ITableCRUD<VisitHistory>
    {
        public void CreateStringInTable(VisitHistory classObject)
        {
            using (var visit = new VisitHistoryContext())
            {
                visit.Visits.Add(classObject);
                visit.SaveChanges();
            }
        }

        public List<VisitHistory> GetAllStringsFromTable()
        {
            var visit = new VisitHistoryContext();
            return visit.Visits.ToList();
        }

        public VisitHistory GetModelFromTable(string id)
        {
            var visit = new VisitHistoryContext();
            return visit.Visits.FirstOrDefault(v => v.Id == id);
        }

        public void RemoveStringInTable(string id)
        {
            using (var visit = new VisitHistoryContext())
            {
                var model = GetModelFromTable(id);
                visit.Visits.Attach(model);
                visit.Visits.Remove(model);
                visit.SaveChanges();
            }
        }

        public void UpdateStringInTable(VisitHistory classObject)
        {
            using (var visit = new VisitHistoryContext())
            {
                visit.Visits.AddOrUpdate(classObject);
                visit.SaveChanges();
            }
        }
    }
}