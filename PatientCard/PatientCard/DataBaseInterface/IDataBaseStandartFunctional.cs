using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCard.DataBaseInterface
{
    public interface IDataBaseStandartFunctional<T> where T : class
    {
        List<T> GetAllStringsFromTable();

        void CreateStringInTable(T classObject);

        void RemoveStringInTable(string id);
    }
}
