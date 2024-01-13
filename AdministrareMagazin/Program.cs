using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministrareMagazin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AplicationDbContext aplicationDbContext = new AplicationDbContext();
            aplicationDbContext.CreateDatabase();
            aplicationDbContext.CreateUsersTable();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form4 form4 = new Form4();
            form4.FormClosed += (sender, e) => ShowForm1();
            Application.Run(form4);
        }

        private static void ShowForm1()
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}
