using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ContactTracingApp
{
    public partial class Form2 : Form
    {
        

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            comboBoxMonth.SelectedIndex = 0;
            comboBoxDay.SelectedIndex = 0;
            comboBoxYear.SelectedIndex = 0;
            comboBox1.SelectedIndex = -1;
            comboBox1.Enabled = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            listBox9.Items.Clear();
            listBox10.Items.Clear();
            listBoxDefault.Items.Clear();
            
            panelInfo.Visible = false;

            string Date = (comboBoxMonth.Text + comboBoxDay.Text + comboBoxYear.Text);

            if ((comboBoxMonth.Text == "MM") || (comboBoxDay.Text == "DD") || (comboBoxYear.Text == "YYYY"))
            {
                MessageBox.Show("Invalid date. Input a proper date and try again.");
                return;
            }
            comboBox1.Enabled = true;
            labelInstruction1.Visible = false;
            labelInstruction2.Visible = true;

            if (File.Exists(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt"))
            {
                StreamReader loadContact = File.OpenText(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt");
                string records = loadContact.ReadToEnd();
                loadContact.Close();
                string eachLine = records;
                string[] lineCut = new string[] { "\n" };
                string[] line = eachLine.Split(lineCut, StringSplitOptions.None);
                foreach (string entry in line)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[0];
                    if (word[0] == "")
                    {
                        return;
                    }
                    string LastName = word[1];
                    string Sex = word[2];
                    string Address = word[3];
                    string PhoneNumber = word[4];
                    string Email = word[5];
                    string Time = word[6];
                    string Vax = word[7];
                    string HealthState = word[8];
                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                    listBoxDefault.Items.Add(FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                    listBox6.Items.Add(FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                    listBox7.Items.Add(LastName + " " + FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                    listBox8.Items.Add(Address + " " + FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                    listBox9.Items.Add(Time + " " + FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                    listBox10.Items.Add(HealthState + " " + FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vax + " " + HealthState);
                }
            }
            else
            {
                MessageBox.Show("No records can be found from that date.");
                return;
            }
            comboBox1.Enabled = true;
            return;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            panelInfo.Visible = true;

            if ((comboBox1.SelectedIndex == 0) || (comboBox1.SelectedIndex == -1))
            {
                listBoxDefault.SelectedIndex = i;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                string eachWord = (string)listBoxDefault.SelectedItem;
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[0];
                string LastName = word[1];
                string Sex = word[2];
                string Address = word[3];
                string PhoneNumber = word[4];
                string Email = word[5];
                string Time = word[6];
                string Vax = word[7];
                string HealthState = word[8];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;

            }
            if (comboBox1.SelectedIndex == 1)
            {
                listBox6.SelectedIndex = i;
                string eachWord = (string)listBox6.SelectedItem;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[0];
                string LastName = word[1];
                string Sex = word[2];
                string Address = word[3];
                string PhoneNumber = word[4];
                string Email = word[5];
                string Time = word[6];
                string Vax = word[7];
                string HealthState = word[8];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                listBox7.SelectedIndex = i;
                string eachWord = (string)listBox7.SelectedItem;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                MessageBox.Show((string)listBox7.SelectedItem);
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[1];
                string LastName = word[2];
                string Sex = word[3];
                string Address = word[4];
                string PhoneNumber = word[5];
                string Email = word[6];
                string Time = word[7];
                string Vax = word[8];
                string HealthState = word[9];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                listBox8.SelectedIndex = i;
                string eachWord = (string)listBox8.SelectedItem;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[1];
                string LastName = word[2];
                string Sex = word[3];
                string Address = word[4];
                string PhoneNumber = word[5];
                string Email = word[6];
                string Time = word[7];
                string Vax = word[8];
                string HealthState = word[9];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                listBox9.SelectedIndex = i;
                string eachWord = (string)listBox9.SelectedItem;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[1];
                string LastName = word[2];
                string Sex = word[3];
                string Address = word[4];
                string PhoneNumber = word[5];
                string Email = word[6];
                string Time = word[7];
                string Vax = word[8];
                string HealthState = word[9];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                listBox10.SelectedIndex = i;
                string eachWord = (string)listBox10.SelectedItem;
                if (((string)listBox1.SelectedItem == null) || ((string)listBox1.SelectedItem == " ") || ((string)listBox1.SelectedItem == "\n"))
                {
                    return;
                }
                string[] wordCut = new string[] { " " };
                string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                string FirstName = word[1];
                string LastName = word[2];
                string Sex = word[3];
                string Address = word[4];
                string PhoneNumber = word[5];
                string Email = word[6];
                string Time = word[7];
                string Vax = word[8];
                string HealthState = word[9];

                labelFNameField.Text = FirstName;
                labelLNameField.Text = LastName;
                labelSexField.Text = Sex;
                labelAddressField.Text = Address;
                labelEmailField.Text = Email;
                labelPhoneNumberField.Text = PhoneNumber;
                labelTimeField.Text = Time;
                labelVaxField.Text = Vax;
                labelHealthField.Text = HealthState;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelInfo.Visible = false;

            if (comboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBoxDefault.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[0];
                    string LastName = word[1];
                    string Sex = word[2];
                    string Address = word[3];
                    string Time = word[6];
                    string HealthState = word[8];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
                return;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBox6.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[0];
                    string LastName = word[1];
                    string Sex = word[2];
                    string Address = word[3];
                    string Time = word[6];
                    string HealthState = word[8];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBox7.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[1];
                    string LastName = word[2];
                    string Sex = word[3];
                    string Address = word[4];
                    string Time = word[7];
                    string HealthState = word[9];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBox8.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[1];
                    string LastName = word[2];
                    string Sex = word[3];
                    string Address = word[4];
                    string Time = word[7];
                    string HealthState = word[9];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
            }
            if (comboBox1.SelectedIndex == 4)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBox9.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[1];
                    string LastName = word[2];
                    string Sex = word[3];
                    string Address = word[4];
                    string Time = word[7];
                    string HealthState = word[9];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
            }
            if (comboBox1.SelectedIndex == 5)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();

                foreach (string entry in listBox10.Items)
                {
                    string eachWord = entry;
                    string[] wordCut = new string[] { " " };
                    string[] word = eachWord.Split(wordCut, StringSplitOptions.None);
                    string FirstName = word[1];
                    string LastName = word[2];
                    string Sex = word[3];
                    string Address = word[4]; ;
                    string Time = word[7];
                    string HealthState = word[9];

                    listBox1.Items.Add(FirstName + " " + LastName);
                    listBox2.Items.Add(Sex);
                    listBox3.Items.Add(Address);
                    listBox4.Items.Add(Time);
                    listBox5.Items.Add(HealthState);
                }
            }
        }

        private void buttonClosepanelInfo_Click(object sender, EventArgs e)
        {
            panelInfo.Visible = false;
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
    }
}
