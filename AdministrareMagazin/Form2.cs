using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace AdministrareMagazin
{
    public partial class Form2 : Form
    {
        private readonly AplicationDbContext dbContext;
        public Form2()
        {
            InitializeComponent();
            dbContext = new AplicationDbContext();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string denumire = textBox1.Text;
            string descriere = textBox2.Text;

            if (DateTime.TryParse(textBox3.Text, out DateTime intrare) &&
                DateTime.TryParse(textBox4.Text, out DateTime valabilitate) &&
                int.TryParse(textBox5.Text, out int cantitate))
            {
                dbContext.InsertData(denumire, descriere, intrare, valabilitate, cantitate);
                MessageBox.Show("Datele au fost adăugate cu succes!");
            }
            else
            {
                MessageBox.Show("Datele introduse nu sunt în formatul corect!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(textBox6.Text, out id))
            {
                MessageBox.Show("ID-ul trebuie să fie un număr întreg.");
                return;
            }

            int cantitateNoua;
            if (!int.TryParse(textBox7.Text, out cantitateNoua))
            {
                MessageBox.Show("Cantitatea trebuie să fie un număr întreg.");
                return;
            }

            var produs = dbContext.GetData().AsEnumerable().FirstOrDefault(row =>
     Convert.ToInt32(row["ID"]) == id &&
     row["Cantitate"] != DBNull.Value);

            if (produs == null)
            {
                MessageBox.Show($"Produsul cu ID-ul {id} nu există în baza de date sau Cantitatea este NULL.");
                return;
            }

            int cantitateCurenta;
            if (!int.TryParse(produs["Cantitate"].ToString(), out cantitateCurenta))
            {
                MessageBox.Show("Nu s-a putut converti Cantitatea la un număr întreg.");
                return;
            }

            int cantitateFinala = cantitateCurenta + cantitateNoua;

            dbContext.UpdateCantitate(id, cantitateFinala);

            MessageBox.Show($"Cantitatea pentru produsul cu ID-ul {id} a fost actualizată cu succes.");


        }



        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
