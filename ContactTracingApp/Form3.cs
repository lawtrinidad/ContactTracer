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
                this.Hide();
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
            this.Hide();
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 0)
            {
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
            }
        }

        private void Form3_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    TableLayoutColumnStyleCollection styles = this.tableLayoutPanel1.ColumnStyles;
                    int column = tableLayoutPanel1.GetColumn(c);
                    foreach (ColumnStyle style in styles)
                    {
                        if (column == 0)
                        {
                            style.SizeType = SizeType.Percent;
                            style.Width = 33;
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    TableLayoutColumnStyleCollection styles = this.tableLayoutPanel1.ColumnStyles;
                    int column = tableLayoutPanel1.GetColumn(c);
                    foreach (ColumnStyle style in styles)
                    {
                        if (column == 0)
                        {
                            style.SizeType = SizeType.AutoSize;
                        }
                    }
                }
            }
        }

        private void buttonCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a contact tracing app, created to be themed to match the Polytechnic University of the Philippines.\n\nMade by John Lawrence E. Trinidad, as a BS Computer Engineering freshman at PUP.\n\n");
        }
    }
}
