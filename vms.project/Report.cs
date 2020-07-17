using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vms.project
{
    public partial class Report : Form
    {

        public MODEL.Data model;

        public Report()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {


            DataTable dtMap = new DataTable("myMember");  //*** DataTable Map DataSet.xsd ***//
            



DataRow dr = null;
            dtMap.Columns.Add(new DataColumn("vm_id", typeof(string)));
            dtMap.Columns.Add(new DataColumn("name", typeof(string)));
            dtMap.Columns.Add(new DataColumn("lastname", typeof(string)));
            dtMap.Columns.Add(new DataColumn("company", typeof(string)));
            dtMap.Columns.Add(new DataColumn("license_plate", typeof(string)));
            dtMap.Columns.Add(new DataColumn("contact_person", typeof(string)));
            dtMap.Columns.Add(new DataColumn("in_time", typeof(string)));
            dtMap.Columns.Add(new DataColumn("dept", typeof(string)));
            dtMap.Columns.Add(new DataColumn("barcode", typeof(System.Byte[])));


            FileStream fiStream = new FileStream(Application.StartupPath + "//Barcode//"  + model.vm_id + ".Jpg", FileMode.Open);
            
            BinaryReader binReader = new BinaryReader(fiStream);
           
            byte[] pic = { };
           
            pic = binReader.ReadBytes((int)fiStream.Length);
            



dr = dtMap.NewRow();

            dr["vm_id"] = model.vm_id;
            dr["name"] = model.name;
            dr["lastname"] = model.lastname;
            dr["company"] = model.company;
            dr["license_plate"] = model.license_plate;
            dr["contact_person"] = model.contact_person;
            dr["in_time"] = model.in_time;
            dr["dept"] = model.dept;
            dr["barcode"] = pic;
            dtMap.Rows.Add(dr);

            fiStream.Close();
            binReader.Close();

            ReportDocument rpt = new ReportDocument();
            
            rpt.Load("visitor.rpt");
            
            rpt.SetDataSource(dtMap);
            
this.crystalReportViewer1.ReportSource = rpt;

        }
    }
}
