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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void buttonOpenRecords_Click(object sender, EventArgs e)
        {
            textBoxUsername.Visible = true;
            textBoxPassword.Visible = true;
            buttonSurveyForm.Visible = false;
            buttonOpenRecords.Visible = false;
            splitContainerLogInDialog.Visible = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            textBoxUsername.Visible = false;
            textBoxPassword.Visible = false;
            buttonSurveyForm.Visible = true;
            buttonOpenRecords.Visible = true;
            splitContainerLogInDialog.Visible = false;
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            if ((textBoxUsername.Text == "admin") && (textBoxPassword.Text == "admin"))
            {
                Form2 records = new Form2();
                records.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("You have entered an incorrect username or password.\n\nFor security purposes, this feature can only be accessed by those who have clearance.\n\nMake sure you have the correct login and try again.");
                textBoxPassword.Text = "";
                textBoxUsername.Text = "";
            }
        }

        private void buttonSurveyForm_Click(object sender, EventArgs e)
        {
            Form1 surveyform = new Form1();
            surveyform.Show();
            this.Visible = false;
        }
    }
}
