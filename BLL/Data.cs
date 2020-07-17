using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Data
    {
        private DAL.Data _DAL = new DAL.Data();


        public string getRuningNoDoc()
        {
            return _DAL.getRuningNoDoc();
        }

        public int Insert_Data(MODEL.Data criteria)
        {
            return _DAL.Insert_Data(criteria);
        }

        public DataTable Update_Data(MODEL.Data criteria)
        {
            return _DAL.Update_Data(criteria);
        }

        public DataTable Select_Data(MODEL.Data criteria)
        {
            return _DAL.Select_Data(criteria);
        }
    }
}
