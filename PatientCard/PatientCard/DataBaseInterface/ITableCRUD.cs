using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCard.DataBaseInterface
{
    public interface ITableCRUD<T> : IDataBaseStandartFunctional<T> where T : class 
    {
        T GetModelFromTable(string id);

        void UpdateStringInTable(T classObject);
    }
}
