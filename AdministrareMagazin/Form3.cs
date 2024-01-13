using System;
using System.Windows.Forms;

namespace AdministrareMagazin
{
    public partial class Form3 : Form
    {
        private readonly AplicationDbContext dbContext;

        public Form3()
        {
            InitializeComponent();
            dbContext = new AplicationDbContext();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("butonul a fost apasat");
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("ID-ul trebuie să fie un număr întreg.");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int cantitateDeSters))
            {
                MessageBox.Show("Cantitatea trebuie să fie un număr întreg.");
                return;
            }

          
            if (cantitateDeSters < 0)
            {
                dbContext.DeleteData(id);
                MessageBox.Show("Produsul a fost șters.");
            }
            else
            {
                dbContext.DeleteQuantity(id, cantitateDeSters);
                MessageBox.Show("Cantitatea a fost actualizată.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("butonul a fost apasat");
            if (!int.TryParse(textBox1.Text, out int id))
            {
                MessageBox.Show("ID-ul trebuie să fie un număr întreg.");
                return;
            }

            if (!int.TryParse(textBox2.Text, out int cantitateDeSters))
            {
                MessageBox.Show("Cantitatea trebuie să fie un număr întreg.");
                return;
            }
            if (cantitateDeSters < 0)
            {
                dbContext.DeleteData(id);
                MessageBox.Show("Produsul a fost șters.");
            }
            else
            {
                dbContext.DeleteQuantity(id, cantitateDeSters);
                MessageBox.Show("Cantitatea a fost actualizată.");
            }
        }
    }
}
