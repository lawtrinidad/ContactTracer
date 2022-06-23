using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            string Date = (comboBoxMonth.Text + comboBoxDay.Text + comboBoxYear.Text);

            if ((comboBoxMonth.Text == "MM") || (comboBoxDay.Text == "DD") || (comboBoxYear.Text == "YYYY"))
            {
                MessageBox.Show("Invalid date. Input a proper date and try again.");
                return;
            }

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
                }
            }
            else
            {
                MessageBox.Show("No records can be found from that date.");
                return;
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;

            panelInfo.Visible = true;

            string Date = (comboBoxMonth.Text + comboBoxDay.Text + comboBoxYear.Text);

            if (File.Exists(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt"))
            {
                StreamReader loadContact = File.OpenText(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt");
                string records = loadContact.ReadToEnd();
                loadContact.Close();
                string eachLine = records;
                string[] lineCut = new string[] { "\n" };
                string[] line = eachLine.Split(lineCut, StringSplitOptions.None);
                string eachWord = line[i];
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
        }
    }
}
