using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using QRCodeDecoderLibrary;
using QRCodeEncoderLibrary;
using System.Runtime.InteropServices.ComTypes;

namespace ContactTracingApp
{
    public partial class Form4 : Form
    {
        Form1 form1 = new Form1();

        private bool[,] QRCodeMatrix;
        private Bitmap QRCodeImage;
        private Bitmap QRCodeInputImage;
        private QRDecoder QRCodeDecoder;
        private bool VideoCameraExists;
        private FrameSize FrameSize;
        private Camera VideoCamera;
        private IMoniker CameraMoniker;
        private Panel CameraPanel;

        public Form4()
        {
            InitializeComponent();
            VideoCameraExists = TestForVideoCamera();
            QRCodeDecoder = new QRDecoder();
            while (buttonCreateQR.BackColor == Color.White)
            {
                panelCreateQR.Visible = true;
                return;
            }
            if (QRCodeInputImage != null) QRCodeInputImage.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonShowForm1_Click(object sender, EventArgs e)
        {
            panelContent.Visible = true;
            form1.TopLevel = false;
            form1.AutoScroll = true;
            form1.Dock = DockStyle.Fill;
            panelContent.Controls.Add(form1);
            form1.Show();
            buttonShowForm1.BackColor = Color.White;
            buttonShowForm1.ForeColor = Color.Orange;
            buttonQRSection.BackColor = Color.Moccasin;
            buttonQRSection.ForeColor = Color.White;
            buttonCreateQR.BackColor = Color.Moccasin;
            buttonCreateQR.ForeColor = Color.White;
            panelGenerate.Visible = false;
            panelScan.Visible = false;
            timer1.Stop();
        }

        private void buttonQRSection_Click(object sender, EventArgs e)
        {
            form1.Visible = false;
            panelContent.Controls.Remove(form1);
            panelContent.Update();
            panelContent.Visible = true;
            panelGenerate.Visible = true;
            panelScan.Visible = true;
            buttonQRSection.BackColor = Color.White;
            buttonQRSection.ForeColor = Color.Orange;
            buttonShowForm1.BackColor = Color.Moccasin;
            buttonShowForm1.ForeColor = Color.White;
            buttonCreateQR.BackColor = Color.Moccasin;
            buttonCreateQR.ForeColor = Color.White;
            panelCreateQR.Visible = false;

            form1.TopLevel = false;
            form1.AutoScroll = true;
            form1.Dock = DockStyle.Fill;
            panelGenerate.Controls.Add(form1);
            form1.Show();
        }

        private void buttonImportQR_Click(object sender, EventArgs e)
        {
            textBoxQRText.Text = null;
            panelSubmit.Visible = false;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp"; ;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                QRCodeInputImage = new Bitmap(dialog.FileName);
                pictureBoxNewFrame.BackgroundImage = QRCodeInputImage;
                timer1.Start();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            textBoxQRText.Text = null;
            panelSubmit.Visible = false;
            QRCodeInputImage = null;
            if (pictureBoxNewFrame.BackgroundImage != null)
            {
                pictureBoxNewFrame.BackgroundImage.Dispose();
                pictureBoxNewFrame.BackgroundImage = null;
            }

            if (VideoCamera == null)
            {                
                CameraPanel = new Panel();
                Controls.Add(CameraPanel);
                CameraPanel.Name = "CameraPanel";

                VideoCamera = new Camera(CameraPanel, CameraMoniker, FrameSize);
            }

            timer1.Start();
        }
                      
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        
        private void DisplayResult
				(
				QRCodeResult[] ResultArray
				)
        {
            textBoxQRText.Text = ConvertResultToDisplayString(ResultArray);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            textBoxQRText.Text = null;
            Bitmap QRCodeImage;
            if (QRCodeInputImage == null)
            {
                QRCodeImage = VideoCamera.SnapshotSourceImage();
                pictureBoxNewFrame.BackgroundImage = QRCodeImage;
            }

                QRCodeResult[] QRCodeResultArray = QRCodeDecoder.ImageDecoder((Bitmap)pictureBoxNewFrame.BackgroundImage);

            if (QRCodeResultArray != null)
            {
                DisplayResult(QRCodeResultArray);
                panelSubmit.Visible = true;
                if (textBoxQRText.Text != null)
                {
                    timer1.Stop();
                }
            }
        }

        private bool TestForVideoCamera()
        {
            DsDevice[] CameraDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (CameraDevices == null || CameraDevices.Length == 0) return false;

            DsDevice CameraDevice = CameraDevices[0];

            CameraMoniker = CameraDevice.Moniker;

            FrameSize[] FrameSizes = Camera.GetFrameSizeList(CameraMoniker);

            if (FrameSizes == null || FrameSizes.Length == 0)
            {
                CameraMoniker = null;
                return false;
            }

            int Index;
            for (Index = 0; Index < FrameSizes.Length &&
                (FrameSizes[Index].Width != 640 || FrameSizes[Index].Height != 480); Index++) ;

            if (Index < FrameSizes.Length)
            {
                FrameSize = new FrameSize(640, 480);
            }

            else
            {
                FrameSize = FrameSizes[0];
            }

            return true;
        }

        private static string SingleQRCodeResult
                (
                string Result
                )
        {
            int Index;
            for (Index = 0; Index < Result.Length && (Result[Index] >= ' ' && Result[Index] <= '~' || Result[Index] >= 160); Index++) ;
            if (Index == Result.Length) return Result;

            StringBuilder Display = new(Result[..Index]);
            for (; Index < Result.Length; Index++)
            {
                char OneChar = Result[Index];
                if (OneChar >= ' ' && OneChar <= '~' || OneChar >= 160)
                {
                    Display.Append(OneChar);
                    continue;
                }

                if (OneChar == '\r')
                {
                    Display.Append("\r\n");
                    if (Index + 1 < Result.Length && Result[Index + 1] == '\n') Index++;
                    continue;
                }

                if (OneChar == '\n')
                {
                    Display.Append("\r\n");
                    continue;
                }

                Display.Append('¿');
            }
            return Display.ToString();
        }

        private static string ConvertResultToDisplayString
                (
                QRCodeResult[] DataByteArray
                )
        {
            // no QR code
            if (DataByteArray == null) return string.Empty;

            // image has one QR code
            if (DataByteArray.Length == 1) return SingleQRCodeResult(QRDecoder.ByteArrayToStr(DataByteArray[0].DataArray));

            // image has more than one QR code
            StringBuilder Str = new();
            for (int Index = 0; Index < DataByteArray.Length; Index++)
            {
                if (Index != 0) Str.Append("\r\n");
                Str.AppendFormat("QR Code {0}\r\n", Index + 1);
                Str.Append(SingleQRCodeResult(QRDecoder.ByteArrayToStr(DataByteArray[Index].DataArray)));
            }
            return Str.ToString();
        }

        private void buttonSubmitInfo_Click(object sender, EventArgs e)
        {
            string eachWord = textBoxQRText.Text;
            string[] wordCut = new string[] { " " };
            string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
            MessageBox.Show(word.Count().ToString());

            if ((word.Count() != 8) || (word.Count() >= 9))
            {
                MessageBox.Show("Invalid data.\n\nPlease check if you're using the correct QR code.\nWe encourage only using QR codes made with the app.");
                return;
            }

            string FirstName = word[0];
            string LastName = word[1];
            string Sex = word[2];
            string Address = word[3];
            string PhoneNumber = word[4];
            string Email = word[5];
            string Vax = word[6];
            string HealthState = word[7];

            form1.textBox1.Text = FirstName;
            form1.textBox2.Text = LastName;
            if (Sex == "Male") form1.radioButton1.Checked = true;
            else form1.radioButton2.Checked = true;
            form1.textBox3.Text = Address;
            form1.textBox4.Text = PhoneNumber;
            form1.textBox5.Text = Email;
            if (Vax == "1stDose") form1.radioButton3.Checked = true;
            else if (Vax == "FullyVaccinated") form1.radioButton4.Checked = true;
            else form1.radioButton5.Checked = true;
            if (HealthState == "Risk") form1.radioButton8.Checked = true;
            else form1.radioButton7.Checked = true;

            MessageBox.Show("Data has been autofilled.\n\nDate and Time has not been entered.");
            textBoxQRText.Text = null;
            pictureBoxNewFrame.BackgroundImage = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                return;
            }
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void buttonCreateQR_Click(object sender, EventArgs e)
        {
            panelScan.Visible = false;
            form1.Visible = false;
            panelGenerate.Controls.Remove(form1);
            panelGenerate.Update();
            panelContent.Visible = true;
            panelCreateQR.Visible = true;
            buttonCreateQR.BackColor = Color.White;
            buttonCreateQR.ForeColor = Color.Orange;
            buttonQRSection.BackColor = Color.Moccasin;
            buttonQRSection.ForeColor = Color.White;
            buttonQRSection.BackColor = Color.Moccasin;
            buttonQRSection.ForeColor = Color.White;
            timer1.Stop();
        }

        private void panelCreateQR_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            string FirstName = textBox1.Text.Replace(" ", "");
            string LastName = textBox2.Text;
            string Address = textBox3.Text.Replace(" ", "");
            string PhoneNumber = textBox4.Text;
            string Email = textBox5.Text;
            string Sex;
            string Vaccination;
            string HealthState;

            if (radioButton1.Checked == true)
            {
                Sex = "Male";
            }
            else
            {
                Sex = "Female";
            }

            if (radioButton3.Checked == true)
            {
                Vaccination = "1stDose";
            }
            else if (radioButton4.Checked == true)
            {
                Vaccination = "FullyVaccinated";
            }
            else
            {
                Vaccination = "None";
            }

            if (radioButton8.Checked == true)
            {
                HealthState = "Risk";
            }
            else
            {
                HealthState = "Safe";
            }

            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == ""))
            {
                MessageBox.Show("Please fill up all required fields.");
                return;
            }

            if (((radioButton1.Checked == false) && (radioButton2.Checked == false)) || ((radioButton3.Checked == false) && (radioButton4.Checked == false) && (radioButton5.Checked == false)) || ((radioButton7.Checked == false) && (radioButton8.Checked == false)))
            {
                MessageBox.Show("Please fill up all required fields.");
                return;
            }

            string QRData = FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Vaccination + " " + HealthState;

            QREncoder encoder = new QREncoder();
            QRCodeMatrix = encoder.Encode(QRData);
            QRSaveBitmapImage bitmapImage = new(QRCodeMatrix);
            QRCodeImage = bitmapImage.CreateQRCodeBitmap();

            pictureBoxQR.BackgroundImage = QRCodeImage;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (pictureBoxQR.BackgroundImage == null)
            {
                return;
            }
            if ((textBoxFileName.Text == null) || (textBoxFileName.Text == ""))
            {
                MessageBox.Show("No file name specified.");
                return;
            }
            string fileName = textBoxFileName.Text;
            
            pictureBoxQR.BackgroundImage.Save(@"C:\Users\Public\Pictures\"+fileName+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            MessageBox.Show("Your QR code has been saved at: C:\\Users\\Public\\Pictures.");
        }

        private void buttonClearQR_Click(object sender, EventArgs e)
        {
            pictureBoxQR.BackgroundImage = null;
            foreach (var tools in panelCreateQR.Controls)
            {
                if (tools is TextBox)
                {
                    ((TextBox)tools).Text = null;
                }
                if (tools is RadioButton)
                {
                    ((RadioButton)tools).Checked = false;
                }
            }
        }
    }
}
