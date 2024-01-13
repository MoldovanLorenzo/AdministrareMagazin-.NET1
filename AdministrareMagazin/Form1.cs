using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministrareMagazin
{
    public partial class Form1 : Form
    {
        private readonly AplicationDbContext dbContext;
        public Form1()
        {
            InitializeComponent();
            dbContext = new AplicationDbContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form3 form3= new Form3();
            form3.Show();
        }

        private void adaugareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void vanzareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = dbContext.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afișarea datelor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cautareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string denumireCautata = textBox1.Text;
            DataTable rezultateCautare = await Task.Run(() => dbContext.SearchDataByDenumire(denumireCautata));
            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.DataSource = rezultateCautare;
            });
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
