using PatientCard.DataBase;
using PatientCard.DataBaseLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientCard.Controllers
{
    public class PatientController : Controller
    {
        enum CollectionIndexes : byte { IdIndex = 0, IinIndex, FullNameIndex, AddressIndex, PhoneNumberIndex}
        static PatientTableCRUD patient = new PatientTableCRUD();
        // GET: Patient
        public ActionResult PatientView()
        {
            return View(patient.GetAllStringsFromTable());
        }
        [HttpPost]
        public ActionResult PatientView(string searchString)
        {
            var patients = patient.GetAllStringsFromTable();

            if (!String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("PatientDetails", new { id = patients.FirstOrDefault(p => p.Iin == searchString).Id });
            }
            else
                return View();
        }
        
        // GET: Patient/Details/5
        public ActionResult PatientDetails(string id)
        {
            return View(patient.GetModelFromTable(id));
        }

        // GET: Patient/Create
        public ActionResult CreatePatientPages()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult CreatePatientPages(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var newPatient = GetPatientInfoModel(collection);
                patient.CreateStringInTable(newPatient);
                return RedirectToAction("PatientView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult UpdatePatientInfo(string id)
        {
            return View(patient.GetModelFromTable(id));
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult UpdatePatientInfo(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var newPatient = GetPatientInfoModel(collection);
                patient.UpdateStringInTable(newPatient);
                return RedirectToAction("PatientView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult DeletePatient(string id)
        {
            return View(patient.GetModelFromTable(id));
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult DeletePatient(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                patient.RemoveStringInTable(id);
                return RedirectToAction("PatientView");
            }
            catch
            {
                return View();
            }
        }

        private PatientInfo GetPatientInfoModel(FormCollection collection)
        {
            Dictionary<string, int> patientModelDictionary = GetDictionaryForCollection(collection);
            CheckData(collection, patientModelDictionary);
            return new PatientInfo
            {
                Id = collection[patientModelDictionary[CollectionIndexes.IdIndex.ToString()]],
                Iin = collection[patientModelDictionary[CollectionIndexes.IinIndex.ToString()]],
                FullName = collection[patientModelDictionary[CollectionIndexes.FullNameIndex.ToString()]],
                Address = collection[patientModelDictionary[CollectionIndexes.AddressIndex.ToString()]],
                PhoneNumber = collection[patientModelDictionary[CollectionIndexes.PhoneNumberIndex.ToString()]]
            };
        }

        const int COLLECTION_KEYS_CREATE_COUNT = 5;

        private Dictionary<string, int> GetDictionaryForCollection(FormCollection collection)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(CollectionIndexes.IdIndex.ToString(), (byte)CollectionIndexes.IdIndex);
            dictionary.Add(CollectionIndexes.IinIndex.ToString(), (byte)CollectionIndexes.IinIndex);
            dictionary.Add(CollectionIndexes.FullNameIndex.ToString(), (byte)CollectionIndexes.FullNameIndex);
            dictionary.Add(CollectionIndexes.AddressIndex.ToString(), (byte)CollectionIndexes.AddressIndex);
            dictionary.Add(CollectionIndexes.PhoneNumberIndex.ToString(), (byte)CollectionIndexes.PhoneNumberIndex);

            if (collection.Count > COLLECTION_KEYS_CREATE_COUNT)
                ShiftDictionaryValuePerOne(ref dictionary);

            return dictionary;
        }

        private void ShiftDictionaryValuePerOne(ref Dictionary<string, int> dictionary)
        {
            const byte SHIFT_VALUE_PER_ONE = 1;
            dictionary[CollectionIndexes.IdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.IinIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.FullNameIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.AddressIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.PhoneNumberIndex.ToString()] += SHIFT_VALUE_PER_ONE;
        }

        private void CheckData(FormCollection collection, Dictionary<string, int> dictionary)
        {
            CheckIinOrPhoneNumber(collection, dictionary, collection[dictionary[CollectionIndexes.IinIndex.ToString()]]);
            CheckIinOrPhoneNumber(collection, dictionary, collection[dictionary[CollectionIndexes.PhoneNumberIndex.ToString()]]);
        }

        private void CheckIinOrPhoneNumber(FormCollection collection, Dictionary<string, int> dictionary, string stringFromCollection)
        {
            const int LENGTH = 11;
            foreach (var iinSymbol in stringFromCollection)
            {
                if (!(int.TryParse(iinSymbol.ToString(), out int result)) || (stringFromCollection.Length != LENGTH))
                    throw new FormatException("Неверный формат");
            }
        }
    }
}
