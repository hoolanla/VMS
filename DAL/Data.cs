using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Data
    {

        public string getRuningNoDoc()
        {
            //Format yyyyMMdd-01   2018011501
           Class.clsDB db = new Class.clsDB();
            string sql = null;
            string curDate;
            curDate = DateTime.Now.ToString("yyyyMMdd");
            sql = "Select vm_id From vm_visitor where vm_id like '" + curDate + "%'";
            object MyScalar = null;
            DataTable dt;
            dt = db.ExecuteDataTable(sql);
            db.Close();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    sql = "Select Max(vm_id) + 1 From vm_visitor";
                    MyScalar = db.ExecuteScalar(sql);
                    return MyScalar.ToString();
                }
                else
                {

                    return curDate.ToString() + "01";
                }
            }

            return curDate;
        }


        public int Insert_Data(MODEL.Data criteria)
        {
            try
            {

                criteria.create_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                criteria.in_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Class.clsDB db = new Class.clsDB();
                string sql;
                sql = "Insert into vm_visitor ( ";
                sql += "vm_id,";
                sql += "name,";
                sql += "lastname,";
                sql += "birthday,";
                sql += "address ,";
                sql += "company ,";
                sql += "contact_person ,";
                sql += "dept ,";
                sql += "id_card ,";
                sql += "license_plate ,";
                sql += "create_by ,";
                sql += "in_time ,";
                sql += "create_date) VALUES(";
                sql += "'" + criteria.vm_id + "',";
                sql += "'" + criteria.name + "',";
                sql += "'" + criteria.lastname + "',";
                sql += "'" + criteria.birth + "',";
                sql += "'" + criteria.address + "',";
                sql += "'" + criteria.company + "',";
                sql += "'" + criteria.contact_person + "',";
                sql += "'" + criteria.dept + "',";
                sql += "'" + criteria.id_card + "',";
                sql += "'" + criteria.license_plate + "',";
                sql += "'" + criteria.create_by + "',";
                sql += "'" + criteria.in_time + "',";
                sql += "'" + criteria.create_date + "')";

                int ret;
                ret = db.ExecuteNonQuery(sql);
                db.Close();

                return ret;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public DataTable Update_Data(MODEL.Data criteria)
        {
            try
            {

                criteria.update_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                criteria.out_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Class.clsDB db = new Class.clsDB();
                string sql;
                sql = "Update vm_visitor SET flag_out='1',  ";
                sql += "out_time='" + criteria.out_time + "',";
                sql += "update_date='" + criteria.update_date + "' Where vm_id='" + criteria.vm_id + "'";

                int ret;
                ret = db.ExecuteNonQuery(sql);
                db.Close();
            if(ret == 1)
                {
                    DataTable dt;
                    sql = "Select * From vm_visitor Where vm_id = '" + criteria.vm_id + "'";
                  dt =  db.ExecuteDataTable(sql);
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable Select_Data(MODEL.Data criteria)
        {
            try
            {

                Class.clsDB db = new Class.clsDB();
                string sql;
             

           
                    DataTable dt;
                    sql = "Select * From vm_visitor Where in_time >= '" + criteria.in_time + "' and out_time <= '" + criteria.out_time + "'" ;
                    dt = db.ExecuteDataTable(sql);
                    return dt;
        
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
