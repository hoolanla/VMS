using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThaiNationalIDCard;
using WebCam_Capture;


namespace vms.project
{
    public partial class Form1 : Form
    {

        WebCam webcam;
        MODEL.Data _Data;

        public Form1()
        {
            InitializeComponent();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
             _Data = new MODEL.Data();
            dt1.CustomFormat = "yyyy-MM-dd";
            dt2.CustomFormat = "yyyy-MM-dd";
            dt1.Format = DateTimePickerFormat.Custom;
            dt2.Format = DateTimePickerFormat.Custom;
        }



        private void btnReadCard_Click(object sender, EventArgs e)
        {


            readCard();

        }

        public void readCard()
        {
            ThaiIDCard idcard = new ThaiIDCard();
            Personal personal = idcard.readAll(true);
            if (personal != null)
            {
                lbCard.Text = personal.Citizenid;
                lbBirth.Text = personal.Birthday.ToString("dd/MM/yyyy");
                lbPrefix.Text = personal.Th_Prefix;
                lbName.Text = personal.Th_Firstname;
                lbSurname.Text = personal.Th_Lastname;
                Console.WriteLine(personal.En_Prefix);
                Console.WriteLine(personal.En_Firstname);
                Console.WriteLine(personal.En_Lastname);
                Console.WriteLine(personal.Issue.ToString("dd/MM/yyyy")); // วันออกบัตร
                Console.WriteLine(personal.Expire.ToString("dd/MM/yyyy")); // วันหมดอายุ

                lbAddress.Text = personal.Address;
                Console.WriteLine(personal.addrHouseNo); // บ้านเลขที่
                Console.WriteLine(personal.addrVillageNo); // หมู่ที่
                Console.WriteLine(personal.addrLane); // ซอย
                Console.WriteLine(personal.addrRoad); // ถนน
                Console.WriteLine(personal.addrTambol);
                Console.WriteLine(personal.addrAmphur);
                Console.WriteLine(personal.addrProvince);

                picCard.Image = (Image)personal.PhotoBitmap;
            }
            else if (idcard.ErrorCode() > 0)
            {
                Console.WriteLine(idcard.Error());
            }
        }

        private void lbAddress_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnVDO_Click(object sender, EventArgs e)
        {
            webcam.Start();
            btnCapture.Enabled = true;

        }

        private void imgCapture_Click(object sender, EventArgs e)
        {
            imgCapture.Image = imgVideo.Image;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public static void SaveImageCapture(System.Drawing.Image image, MODEL.Data model)
        {

            try
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    string filename = Application.StartupPath + "\\Photo\\" + model.vm_id + ".Jpg";

                  //  Thread.Sleep(1000);
                    bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);

                    bitmap.Dispose();//To Do…. isprobaj
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Bitmap newBitmap = new Bitmap(image);
            //image.Dispose();
            //image = null;
            //newBitmap.Save("C://1.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //using (MemoryStream memory = new MemoryStream())
            //{
            //    using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
            //    {
            //        thumbBMP.Save(memory, ImageFormat.Jpeg);
            //        byte[] bytes = memory.ToArray();
            //        fs.Write(bytes, 0, bytes.Length);
            //    }
            //}



            //SaveFileDialog s = new SaveFileDialog();
            //s.FileName = "Image";// Default file name
            //s.DefaultExt = ".Jpg";// Default file extension
            //s.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            //// Show save file dialog box
            //// Process save file dialog box results
            //if (s.ShowDialog() == DialogResult.OK)
            //{
            //    // Save Image
            //    string filename = s.FileName;
            //    FileStream fstream = new FileStream(filename, FileMode.Create);
            //    Thread.Sleep(3);
            //    image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    fstream.Close();

            //}




            // Save Image
            //string filename = Application.StartupPath + "\\" + model.vm_id + ".Png";
            //FileStream fstream = new FileStream(filename, FileMode.Create);
            //Thread.Sleep(1000);
            //image.Save(fstream, System.Drawing.Imaging.ImageFormat.Png);


            //byte[] byt;
            //byt = imageToByteArray(image);
            //byteArrayToImage(byt);

        }

        public static void SaveImageBarcode(System.Drawing.Image image, MODEL.Data model)
        {

            try
            {
                using (Bitmap bitmap = new Bitmap(image))
                {
                    string filename = Application.StartupPath + "\\Barcode\\" + model.vm_id + ".Jpg";

                    //  Thread.Sleep(1000);
                    bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);

                    bitmap.Dispose();//To Do…. isprobaj
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

  

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            Report m = new Report();
            m.model = _Data;
            m.Show();



        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            if (picCard.Image == null && imgCapture.Image == null)
            {
                MessageBox.Show("ต้องบันทึกภาพก่อน");
                return; 

            }





                if (cbNocard.Checked)
                {
                    _Data.name = txtName.Text;
                    _Data.lastname = txtSurname.Text;
                    _Data.picture = imgCapture.Image;

                    _Data.license_plate = txtLicense.Text;
                    _Data.company = txtCompany.Text;
                    _Data.contact_person = txtContactPerson.Text;
                    _Data.dept = txtDept.Text;
                }
                else
                {
                    _Data.name = lbName.Text;
                    _Data.lastname = lbSurname.Text;
                    _Data.id_card = lbCard.Text;
                    _Data.address = lbAddress.Text;
                    _Data.birth = lbBirth.Text;
                    _Data.picture = picCard.Image;

                    _Data.license_plate = txtLicense.Text;
                    _Data.company = txtCompany.Text;
                    _Data.contact_person = txtContactPerson.Text;
                    _Data.dept = txtDept.Text;
                }






                BLL.Data _BLL = new BLL.Data();
                _Data.vm_id = _BLL.getRuningNoDoc();


            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            picBarcode.Image = barcode.Draw(_Data.vm_id, 50);


            // var barcodeImage = barcode.Draw(_Data.vm_id, 50);
            // var resultImage = new Bitmap(barcodeImage.Width + 100, barcodeImage.Height + 20); // 20 is bottom padding, adjust to your text

            // using (var graphics = Graphics.FromImage(resultImage))
            //using (var font = new Font("Consolas", 7))
            // using (var brush = new SolidBrush(Color.Black))
            // using (var format = new StringFormat()
            // {
            //     Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
            //     LineAlignment = StringAlignment.Far
            // })
            // {
            //     graphics.Clear(Color.White);
            //     graphics.DrawImage(barcodeImage, 0, 0);
            //     graphics.DrawString(_Data.vm_id,font, brush, resultImage.Width / 2, resultImage.Height, format);
            // }

            // picBarcode.Image = resultImage;


            SaveImageBarcode(picBarcode.Image, _Data);
            SaveImageCapture(_Data.picture, _Data);
                _BLL.Insert_Data(_Data);

            btnPrint.Enabled = true;

            }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbPrefix.Text = "";
            lbName.Text = "";
            lbSurname.Text = "";
            lbBirth.Text = "";
            lbAddress.Text = "";
            lbCard.Text = "";
            picCard.Image = null;

            txtName.Text = "";
            txtSurname.Text = "";
            txtCompany.Text = "";
            txtContactPerson.Text = "";
            txtLicense.Text = "";
            txtDept.Text = "";
            webcam.Stop();
            imgCapture.Image = null;
            imgVideo.Image = null;




        }

        private void cbNocard_CheckedChanged(object sender, EventArgs e)
        {

            if(cbNocard.Checked)
            {
                btnVDO.Enabled = true;
            }
            else
            {
                btnVDO.Enabled = false;
                lbPrefix.Text = "";
                lbName.Text = "";
                lbSurname.Text = "";
                lbBirth.Text = "";
                lbAddress.Text = "";
                lbCard.Text = "";
                picCard.Image = null;

                txtName.Text = "";
                txtSurname.Text = "";
                txtCompany.Text = "";
                txtContactPerson.Text = "";
                txtLicense.Text = "";
                txtDept.Text = "";
                webcam.Stop();
                imgCapture.Image = null;
                imgVideo.Image = null;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if (  tabControl1.SelectedIndex == 1)
            {
                txtBarcode.Focus();
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void lbAddress2_Click(object sender, EventArgs e)
        {

        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
          
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MODEL.Data model = new MODEL.Data();
                model.vm_id = txtBarcode.Text;
                BLL.Data _BLL = new BLL.Data();
                DataTable dt;
                dt = _BLL.Update_Data(model);
                if (dt.Rows.Count > 0)
                {

                    lbName2.Text = dt.Rows[0]["name"].ToString();
                    lbSurname2.Text = dt.Rows[0]["lastname"].ToString();
                    lbCard2.Text = dt.Rows[0]["id_card"].ToString();

                    lbCompany2.Text = dt.Rows[0]["company"].ToString();
                    lbContact_person2.Text = dt.Rows[0]["contact_person"].ToString();
                    lbDept2.Text = dt.Rows[0]["dept"].ToString();
                    lbLicense2.Text = dt.Rows[0]["license_plate"].ToString();
                    lb_in_time.Text = dt.Rows[0]["in_time"].ToString();
                    lb_out_time.Text = dt.Rows[0]["out_time"].ToString();

                    Bitmap bm = new Bitmap(Application.StartupPath + "//Photo//" + model.vm_id + ".Jpg");
                    picShow2.Image = bm;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MODEL.Data _model = new MODEL.Data();

            string strDT1, strDT2;
            strDT1 = dt1.Value.ToString("yyyy-MM-dd") + " 08:00:00";
            strDT2 = dt2.Value.ToString("yyyy-MM-dd") + " 08:00:00";

            _model.in_time = strDT1;
            _model.out_time = strDT2;

            BLL.Data _BLL = new BLL.Data();
            DataTable dt;
            dt = _BLL.Select_Data(_model);
            DG.DataSource = dt;
        }
    }
    }

