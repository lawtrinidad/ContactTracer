namespace ContactTracingApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            string FirstName = textBox1.Text.Replace(" ", "");
            string LastName = textBox2.Text;
            string Address = textBox3.Text.Replace(" ","");
            string PhoneNumber = textBox4.Text;
            string Email = textBox5.Text;
            string Time = comboBox1.Text.Replace(" ", "");
            string Date = (comboBox2.Text + comboBox3.Text + comboBox4.Text);
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
            } else if(radioButton4.Checked == true)
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

            if (((comboBox1.Text == "") || (comboBox2.Text == "") || (comboBox3.Text == "") || (comboBox4.Text == "")) || ((comboBox1.Text == "Time") || (comboBox2.Text == "MM") || (comboBox3.Text == "DD") || (comboBox4.Text == "YYYY")))
            {
                MessageBox.Show("Please include both the date and time of filling up this form.");
                return;
            }
            if (((radioButton1.Checked == false) && (radioButton2.Checked == false)) || ((radioButton3.Checked == false) && (radioButton4.Checked == false) && (radioButton5.Checked == false)) || ((radioButton7.Checked == false) && (radioButton8.Checked == false)))
            {
                MessageBox.Show("Please fill up all required fields.");
                return;
            }

             StreamWriter recordContact = File.AppendText(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt");
             recordContact.WriteLine(FirstName + " " + LastName + " " + Sex + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Vaccination + " " + HealthState);
             recordContact.Close();

            MessageBox.Show("Thank you for following through with our contact tracing app and protocol.\n\nHave a nice day!");

            foreach (var panel in tableLayoutPanel1.Controls)
            {
                foreach (var tools in panel1.Controls)
                {
                    if(tools is TextBox)
                    {
                        ((TextBox)tools).Text = String.Empty;
                    }
                    if (tools is ComboBox)
                    {
                        ((ComboBox)tools).SelectedIndex = 0;
                    }
                    if (tools is RadioButton)
                    {
                        ((RadioButton)tools).Checked = false;
                    }
                }
            }
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;

        }

        private void buttonToHub_Click(object sender, EventArgs e)
        {
            Form3 hub = new Form3();
            Application.Restart();
        }
    }
}