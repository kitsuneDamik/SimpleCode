using PatientCard.DataBase;
using PatientCard.DataBaseLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientCard.Controllers
{
    public class DoctorController : Controller
    {
        enum CollectionIndexes : byte { IdIndex = 0, FullNameIndex, SpecializationIdIndex }
        DoctorTableCRUD doctor = new DoctorTableCRUD();

        // GET: Doctor
        public ActionResult DoctorView()
        {
            return View(doctor.GetAllStringsFromTable());
        }

        // GET: Doctor/Details/5
        public ActionResult DoctorDetails(string id)
        {
            return View(doctor.GetModelFromTable(id));
        }

        // GET: Doctor/Create
        public ActionResult CreateDoctor()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult CreateDoctor(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var newDoctor = GetDoctorInfoModel(collection);
                doctor.CreateStringInTable(newDoctor);
                return RedirectToAction("DoctorView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult UpdateDoctor(string id)
        {
            return View(doctor.GetModelFromTable(id));
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult UpdateDoctor(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var updatedDoctor = GetDoctorInfoModel(collection);
                doctor.UpdateStringInTable(updatedDoctor);
                return RedirectToAction("DoctorView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Delete/5
        public ActionResult DeleteDoctor(string id)
        {
            return View(doctor.GetModelFromTable(id));
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult DeleteDoctor(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                doctor.RemoveStringInTable(id);
                return RedirectToAction("DoctorView");
            }
            catch
            {
                return View();
            }
        }

        private DoctorInfo GetDoctorInfoModel(FormCollection collection)
        {
            Dictionary<string, int> doctorModelDictionary = GetDictionaryForCollection(collection);
            CheckData(doctorModelDictionary, collection);
            return new DoctorInfo
            {
                Id = collection[doctorModelDictionary[CollectionIndexes.IdIndex.ToString()]],
                FullName = collection[doctorModelDictionary[CollectionIndexes.FullNameIndex.ToString()]],
                SpecializationId = int.Parse(collection[doctorModelDictionary[CollectionIndexes.SpecializationIdIndex.ToString()]]),
            };
        }

        const int COLLECTION_KEYS_CREATE_COUNT = 3;

        private Dictionary<string, int> GetDictionaryForCollection(FormCollection collection)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(CollectionIndexes.IdIndex.ToString(), (byte)CollectionIndexes.IdIndex);
            dictionary.Add(CollectionIndexes.FullNameIndex.ToString(), (byte)CollectionIndexes.FullNameIndex);
            dictionary.Add(CollectionIndexes.SpecializationIdIndex.ToString(), (byte)CollectionIndexes.SpecializationIdIndex);

            if (collection.Count > COLLECTION_KEYS_CREATE_COUNT)
                ShiftDictionaryValuePerOne(ref dictionary);

            return dictionary;
        }

        private void ShiftDictionaryValuePerOne(ref Dictionary<string, int> dictionary)
        {
            const byte SHIFT_VALUE_PER_ONE = 1;
            dictionary[CollectionIndexes.IdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.FullNameIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.SpecializationIdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
        }

        private void CheckData(Dictionary<string, int> dictionaryWithModel, FormCollection collection)
        {
            CheckSpecializationId(dictionaryWithModel, collection);
            CheckFullName(dictionaryWithModel, collection);
        }

        private void CheckSpecializationId(Dictionary<string, int> dictionary, FormCollection collection)
        {
            const int SHIFT_PER_ONE_FOR_ENTY_TO_ARRAY = -1;
            SpecializationContext specialization = new SpecializationContext();
            int specializationCount = specialization.Specializations.Count();
            int collectionIndex = int.Parse(collection[dictionary[CollectionIndexes.SpecializationIdIndex.ToString()]]);
            if ((specializationCount > collectionIndex + SHIFT_PER_ONE_FOR_ENTY_TO_ARRAY) && (collectionIndex > 0))
            {
                return;
            }
            else
                throw new ArgumentOutOfRangeException("Недопустимый ID специализации");
        }

        private void CheckFullName(Dictionary<string, int> dictionary, FormCollection collection)
        {
            char[] forbiddenSymbols = new char[] { '/', '\\', '\'', '\n', '\t', '\a', '"', ':', ';', '{', '}', '[', ']', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char symbol in collection[dictionary[CollectionIndexes.FullNameIndex.ToString()]])
            {
                if (forbiddenSymbols.Contains(symbol))
                    throw new FormatException("Формат ФИО был нарушен");
            }
        }
    }
}
