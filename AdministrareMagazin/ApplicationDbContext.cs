using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

public class AplicationDbContext
{
    private const string ConnectionString = "Data Source=MyDatabase.db;Version=3;";

    public void CreateDatabase()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string createTableQuery = "CREATE TABLE IF NOT EXISTS MyTable (ID INTEGER PRIMARY KEY AUTOINCREMENT, Denumire TEXT, Descriere TEXT, DataIntrareMagazin DATETIME, TermenValabilitate DATETIME, Cantitate INTEGER);";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        CreateUsersTable();
    }

    public void CreateUsersTable()
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string createUserTableQuery = "CREATE TABLE IF NOT EXISTS Users (ID INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT, Password TEXT);";
            using (SQLiteCommand command = new SQLiteCommand(createUserTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddUser(string username, string password)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string insertUserQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password);";
            using (SQLiteCommand command = new SQLiteCommand(insertUserQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteUser(int userId)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string deleteUserQuery = "DELETE FROM Users WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(deleteUserQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", userId);
                command.ExecuteNonQuery();
            }
        }
    }

    public bool AuthenticateUser(string username, string password)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string authenticateUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password;";
            using (SQLiteCommand command = new SQLiteCommand(authenticateUserQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                int userCount = Convert.ToInt32(command.ExecuteScalar());

                return userCount > 0;
            }
        }
    }

    public void InsertData(string denumire, string descriere, DateTime dataIntrare, DateTime dataIesire, int cantitate)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string insertDataQuery = "INSERT INTO MyTable (Denumire, Descriere, DataIntrareMagazin, TermenValabilitate, Cantitate) VALUES (@Denumire, @Descriere, @DataIntrareMagazin, @TermenValabilitate, @Cantitate);";
            using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
            {
                command.Parameters.AddWithValue("@Denumire", denumire);
                command.Parameters.AddWithValue("@Descriere", descriere);
                command.Parameters.AddWithValue("@DataIntrareMagazin", dataIntrare);
                command.Parameters.AddWithValue("@TermenValabilitate", dataIesire);
                command.Parameters.AddWithValue("Cantitate", cantitate);
                command.ExecuteNonQuery();
            }
        }
    }

    public DataTable GetData()
    {
        DataTable dataTable = new DataTable();

        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string getDataQuery = "SELECT * FROM MyTable;";
            using (SQLiteCommand command = new SQLiteCommand(getDataQuery, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }

        return dataTable;
    }

    public void DeleteData(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string deleteDataQuery = "DELETE FROM MyTable WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(deleteDataQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateCantitate(int id, int cantitate)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string updateDataQuery = "UPDATE MyTable SET Cantitate = @Cantitate WHERE ID = @ID;";
            using (SQLiteCommand command = new SQLiteCommand(updateDataQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Cantitate", cantitate);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteQuantity(int id, int quantityToRemove)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            var existingProduct = GetData().AsEnumerable().FirstOrDefault(row => Convert.ToInt32(row["ID"]) == id);

            if (existingProduct != null)
            {
                object cantitateObject = existingProduct["Cantitate"];

                if (cantitateObject != DBNull.Value)
                {
                    int cantitateInt;
                    if (int.TryParse(cantitateObject.ToString(), out cantitateInt))
                    {
                        int newQuantity = cantitateInt - quantityToRemove;

                        if (newQuantity <= 0)
                        {
                            string deleteDataQuery = "DELETE FROM MyTable WHERE ID = @ID;";
                            using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteDataQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@ID", id);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            UpdateQuantity(id, newQuantity);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Eroare: Imposibil de convertit valoarea 'Cantitate' la tipul 'int'.");
                    }
                }
                else
                {
                    MessageBox.Show($"Eroare: Valoarea 'Cantitate' este DBNull.Value.");
                }
            }
            else
            {
                MessageBox.Show($"Produsul cu ID-ul {id} nu există în baza de date.");
            }
        }
    }

    public void UpdateQuantity(int id, int newQuantity)
    {
        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string updateQuantityQuery = "UPDATE MyTable SET Cantitate = @Cantitate WHERE ID = @ID;";
            using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuantityQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@ID", id);
                updateCommand.Parameters.AddWithValue("@Cantitate", newQuantity);
                updateCommand.ExecuteNonQuery();
            }
        }
    }

    public DataTable SearchDataByDenumire(string denumireCautata)
    {
        DataTable dataTable = new DataTable();

        using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string searchDataQuery = "SELECT * FROM MyTable WHERE Denumire LIKE @Denumire;";
            using (SQLiteCommand command = new SQLiteCommand(searchDataQuery, connection))
            {
                command.Parameters.AddWithValue("@Denumire", "%" + denumireCautata + "%");

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
        }

        return dataTable;
    }


}

