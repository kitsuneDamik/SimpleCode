using PatientCard.DataBase;
using PatientCard.DataBaseLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientCard.Controllers
{
    public class SpecializationController : Controller
    {
        enum CollectionIndexes : byte { IdIndex = 0, SpecializationIndex }

        SpecializationTableCRUD specialization = new SpecializationTableCRUD();
        // GET: Specialization
        public ActionResult SpecializationView()
        {
            return View(specialization.GetAllStringsFromTable());
        }

        // GET: Specialization/Details/5
        public ActionResult SpecializationDetails(string id)
        {
            return View(specialization.GetModelFromTable(id));
        }

        // GET: Specialization/Create
        public ActionResult CreateSpecialization()
        {
            return View();
        }

        // POST: Specialization/Create
        [HttpPost]
        public ActionResult CreateSpecialization(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var newSpecialization = GetSpecializationInfoModel(collection);
                specialization.CreateStringInTable(newSpecialization);
                return RedirectToAction("SpecializationView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialization/Edit/5
        public ActionResult UpdateSpecialization(string id)
        {
            return View(specialization.GetModelFromTable(id));
        }

        // POST: Specialization/Edit/5
        [HttpPost]
        public ActionResult UpdateSpecialization(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var updatedSpecialization = GetSpecializationInfoModel(collection);
                specialization.UpdateStringInTable(updatedSpecialization);
                return RedirectToAction("SpecializationView");
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialization/Delete/5
        public ActionResult DeleteSpecialization(string id)
        {
            return View(specialization.GetModelFromTable(id));
        }

        // POST: Specialization/Delete/5
        [HttpPost]
        public ActionResult DeleteSpecialization(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                specialization.RemoveStringInTable(id);
                return RedirectToAction("SpecializationView");
            }
            catch
            {
                return View();
            }
        }


        private Specialization GetSpecializationInfoModel(FormCollection collection)
        {
            Dictionary<string, int> specializationModelDictionary = GetDictionaryForCollection(collection);
            CheckSpecialization(collection, specializationModelDictionary);
            return new Specialization
            {
                Id = collection[specializationModelDictionary[CollectionIndexes.IdIndex.ToString()]],
                DoctorSpecialization = collection[specializationModelDictionary[CollectionIndexes.SpecializationIndex.ToString()]],

            };
        }

        const int COLLECTION_KEYS_CREATE_COUNT = 2;

        private Dictionary<string, int> GetDictionaryForCollection(FormCollection collection)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add(CollectionIndexes.IdIndex.ToString(), (byte)CollectionIndexes.IdIndex);
            dictionary.Add(CollectionIndexes.SpecializationIndex.ToString(), (byte)CollectionIndexes.SpecializationIndex);

            if (collection.Count > COLLECTION_KEYS_CREATE_COUNT)
                ShiftDictionaryValuePerOne(ref dictionary);

            return dictionary;
        }

        private void ShiftDictionaryValuePerOne(ref Dictionary<string, int> dictionary)
        {
            const byte SHIFT_VALUE_PER_ONE = 1;
            dictionary[CollectionIndexes.IdIndex.ToString()] += SHIFT_VALUE_PER_ONE;
            dictionary[CollectionIndexes.SpecializationIndex.ToString()] += SHIFT_VALUE_PER_ONE;
        }

        private void CheckSpecialization(FormCollection collection, Dictionary<string, int> dictionary)
        {
            
            var specializationInCollection = collection[dictionary[CollectionIndexes.SpecializationIndex.ToString()]];
            var allSpecializations = specialization.GetAllStringsFromTable();
            foreach (var specialization in allSpecializations)
                if (specialization.DoctorSpecialization.Contains(specializationInCollection))
                    throw new DuplicateWaitObjectException("Такая специализация уже существует");
        }
    }
}
