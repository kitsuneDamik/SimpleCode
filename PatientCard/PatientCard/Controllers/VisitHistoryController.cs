using PatientCard.DataBase;
using PatientCard.DataBaseLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientCard.Controllers
{
    public class VisitHistoryController : Controller
    {
        VisitHistoryTableCRUD visit = new VisitHistoryTableCRUD();
        enum CollectionIndexes : byte { IdIndex = 0, DoctorIdIndex, DiagnosisIndex, ComplaintsIndex, DateOfVisitIndex, PatientIdIndex }
       
        // GET: VisitHistory
        public ActionResult VisitsView()
        {
            return View(visit.GetAllStringsFromTable());
        }

        // GET: VisitHistory/Details/5
        public ActionResult VisitDetails(string id)
        {
            return View(visit.GetModelFromTable(id));
        }

        // GET: VisitHistory/Create
        public ActionResult AddVisit()
        {
            return View();
        }

        // POST: VisitHistory/Create
        [HttpPost]
        public ActionResult AddVisit(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var newHistory = GetVisitHistoryInfoModel(collection);
                visit.CreateStringInTable(newHistory);
                return RedirectToAction("VisitsView");
            }
            catch
            {
                return View();
            }
        }

        // GET: VisitHistory/Edit/5
        public ActionResult UpdateVisit(string id)
        {
            return View(visit.GetModelFromTable(id));
        }

        // POST: VisitHistory/Edit/5
        [HttpPost]
        public ActionResult UpdateVisit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var updatedHistory = GetVisitHistoryInfoModel(collection);
                visit.UpdateStringInTable(updatedHistory);
                return RedirectToAction("VisitsView");
            }
            catch
            {
                return View();
            }
        }

        // GET: VisitHistory/Delete/5
        public ActionResult DeleteVisit(string id)
        {
            return View(visit.GetModelFromTable(id));
        }

        // POST: VisitHistory/Delete/5
        [HttpPost]
        public ActionResult DeleteVisit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                visit.RemoveStringInTable(id);
                return RedirectToAction("VisitsView");
            }
            catch
            {
                return View();
            }
        }

        //  enum CollectionIndexes : byte { IdIndex = 0, DoctorIdIndex, DiagnosisIndex, ComplaintsIndex, DateOfVisitIndex, PatientIdIndex }
        private VisitHistory GetVisitHistoryInfoModel(FormCollection collection)
        {
            Dictionary<string, int> visitHistoryModelDictionary = GetDictionaryForCollection(collection);
            CheckData(collection, visitHistoryModelDictionary);
            return new VisitHistory
            {
                Id = collection[visitHistoryModelDictionary[CollectionIndexes.IdIndex.ToString()]],
                DoctorId = int.Parse(collection[visitHistoryModelDictionary[CollectionIndexes.DoctorIdIndex.ToString()]]),
                Diagnosis = collection[visitHistoryModelDictionary[CollectionIndexes.DiagnosisIndex.ToString()]],
                Complaints = collection[visitHistoryModelDictionary[CollectionIndexes.ComplaintsIndex.ToString()]],
                DateOfVisit = DateTime.Parse(collection[visitHistoryModelDictionary[CollectionIndexes.DateOfVisitIndex.ToString()]]),
                PatientId = int.Parse(collection[visitHistoryModelDictionary[CollectionIndexes.PatientIdIndex.ToString()]])
            };
        }

        const int COLLECTION_KEYS_CREATE_COUNT = 6;

        private Dictionary<string, int> GetDictionaryForCollection(FormCollection collection)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(CollectionIndexes.IdIndex.ToString(), (byte)CollectionIndexes.IdIndex);
            dictionary.Add(CollectionIndexes.DoctorIdIndex.ToString(), (byte)CollectionIndexes.DoctorIdIndex);
            dictionary.Add(CollectionIndexes.DiagnosisIndex.ToString(), (byte)CollectionIndexes.DiagnosisIndex);
            dictionary.Add(CollectionIndexes.ComplaintsIndex.ToString(), (byte)CollectionIndexes.ComplaintsIndex);
            dictionary.Add(CollectionIndexes.DateOfVisitIndex.ToString(), (byte)CollectionIndexes.DateOfVisitIndex);
            dictionary.Add(CollectionIndexes.PatientIdIndex.ToString(), (byte)CollectionIndexes.PatientIdIndex);

            if (collection.Count > COLLECTION_KEYS_CREATE_COUNT)
                ShiftDictionaryValuePerOne(ref dictionary);

            return dictionary;
        }

        private void ShiftDictionaryValuePerOne(ref Dictionary<string, int> dictionary)
        {
            const byte SHIFT_VALUE_PER_ONE = 1;
            dictionary[CollectionIndexes.IdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.DoctorIdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.DiagnosisIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.ComplaintsIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.DateOfVisitIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.PatientIdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
        }

        private void CheckData(FormCollection collection, Dictionary<string, int> dictionary)
        {
            CheckDoctorId(collection, dictionary);
            CheckPatientId(collection, dictionary);
        }

        const int SHIFT_PER_ONE_FOR_ENTY_TO_ARRAY = -1;
        
        private void CheckDoctorId(FormCollection collection, Dictionary<string, int> dictionary)
        {
            DoctorContext doctor = new DoctorContext();
            int doctorsCount = doctor.Doctors.Count();
            int doctorIdFromCollection = int.Parse(collection[dictionary[CollectionIndexes.DoctorIdIndex.ToString()]]);
            if ((doctorsCount > doctorIdFromCollection + SHIFT_PER_ONE_FOR_ENTY_TO_ARRAY) && (doctorIdFromCollection > 0))
                return;
            else
                throw new ArgumentOutOfRangeException("Доктора с таким номером не существует");
        }

        private void CheckPatientId(FormCollection collection, Dictionary<string, int> dictionary)
        {
            PatientContext patient = new PatientContext();
            int patientCount = patient.Patients.Count();
            int patientIdFromCollection = int.Parse(collection[dictionary[CollectionIndexes.PatientIdIndex.ToString()]]);
            if ((patientCount > patientIdFromCollection + SHIFT_PER_ONE_FOR_ENTY_TO_ARRAY) && (patientIdFromCollection > 0))
                return;
            else
                throw new ArgumentOutOfRangeException("Пациента с таким номером не существует");
        }

    }
}
