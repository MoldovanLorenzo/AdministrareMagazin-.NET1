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
    public partial class Form4 : Form
    {
        private readonly AplicationDbContext dbContext;
        public Form4()
        {
            InitializeComponent();
            dbContext = new AplicationDbContext();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string username = textBox1.Text;
                string password = textBox2.Text;

                AplicationDbContext aplicationDbContext = new AplicationDbContext();
                bool isAuthenticated = aplicationDbContext.AuthenticateUser(username, password);

                if (isAuthenticated)
                {
                    MessageBox.Show("Autentificare reușită!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Autentificare eșuată. Verificați username-ul și parola.");
                }
            }
            else
            {
                MessageBox.Show("Completați username-ul și parola înainte de a efectua autentificarea.");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string password = textBox3.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Completați toate câmpurile pentru înregistrare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dbContext.AddUser(username, password);

            MessageBox.Show("Utilizator înregistrat cu succes.", "Înregistrare reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBox4.Text = string.Empty;
            textBox3.Text = string.Empty;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                
                if (int.TryParse(textBox5.Text, out int selectedProductId))
                {
                    
                    AplicationDbContext aplicationDbContext = new AplicationDbContext();
                    aplicationDbContext.DeleteUser(selectedProductId);

                   
                }
                else
                {
                    MessageBox.Show("Introduceți un ID valid (număr întreg).");
                }
            }
            else
            {
                MessageBox.Show("Completați ID-ul înainte de a efectua ștergerea.");
            }
        }

    }
}
