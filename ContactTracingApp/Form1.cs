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
            string FirstName = textBox1.Text;
            string LastName = textBox2.Text;
            string Address = textBox3.Text;
            string PhoneNumber = textBox4.Text;
            string Email = textBox5.Text;
            string Time = comboBox1.Text;
            string Date = (comboBox2.Text + comboBox3.Text + comboBox4.Text);
            StreamWriter recordContact = File.AppendText(@"C:\Users\Public\Documents\Contracter\" + Date + ".txt");
            recordContact.WriteLine(FirstName + " " + LastName + " " + Address + " " + PhoneNumber + " " + Email + " " + Time + " " + Date);
            recordContact.Close();


            MessageBox.Show(FirstName + LastName + Address + PhoneNumber + Email + Time + Date);
        }
    }
}