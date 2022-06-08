namespace ContactTracingApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            //Declaring the variables

            string FirstName = textBox1.Text;
            string LastName = textBox2.Text;
            string Address = textBox3.Text;
            string PhoneNumber = textBox4.Text;
            string Email = textBox5.Text;
            string Time = comboBox1.Text;
            string Date = (comboBox2.Text + comboBox3.Text + comboBox4.Text);

            //To make sure all information is gathered:

            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == ""))
            {
                MessageBox.Show("Please fill up all required fields.");
                return;
            }

            if ((comboBox1.Text == "") || (comboBox2.Text == "") || (comboBox3.Text == "") || (comboBox4.Text == ""))
            {
                MessageBox.Show("Please include both the date and time of filling up this form.");
                return;
            }
            
            //Recording the data

             StreamWriter recordContact = File.AppendText(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt");
             recordContact.WriteLine(FirstName + " " + LastName + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " ");
             recordContact.Close();

            //Closing parts

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
                        ((ComboBox)tools).SelectedIndex = -1;
                    }
                }
            }


        }
    }
}