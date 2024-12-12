using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Globalization;

namespace program
{
    public partial class AdminPanel : Form
    {
        private Timer timer;
        public AdminPanel()
        {
            InitializeComponent();
            LoadForm();
            LoadUsers();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string deleteItemsQuery = @"
                DELETE FROM Предметы 
                WHERE коллекция_id IN (
                    SELECT коллекция_id FROM Коллекции 
                    WHERE коллекционер_id IN (
                        SELECT коллекционер_id FROM Коллекционеры 
                        WHERE права <> 'admin'
                    )
                )";

                    string deleteCollectionsQuery = @"
                DELETE FROM Коллекции 
                WHERE коллекционер_id IN (
                    SELECT коллекционер_id FROM Коллекционеры 
                    WHERE права <> 'admin'
                )";

                    string deleteUsersQuery = @"
                DELETE FROM Коллекционеры 
                WHERE права <> 'admin'";

                    using (SqlCommand command = new SqlCommand(deleteItemsQuery, connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(deleteCollectionsQuery, connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(deleteUsersQuery, connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBoxFactory.CreateMessageBox("success").Show("Все пользователи, кроме администраторов, были успешно удалены вместе с их коллекциями и предметами.", "Успех");
                    LoadForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при удалении пользователей: " + ex.Message, "Ошибка");
                }
            }
        }

        private void UpdateDateTimeLabel()
        {
            clock.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTimeLabel();
        }
        private void LoadForm()
        {
            UpdateDateTimeLabel();

            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string userCountQuery = "SELECT COUNT(*) FROM Коллекционеры";
                using (SqlCommand command = new SqlCommand(userCountQuery, connection))
                {
                    int userCount = (int)command.ExecuteScalar();
                    textBox_AllUsers.Text = userCount.ToString();
                }


                string collectionCountQuery = "SELECT COUNT(*) FROM Коллекции";
                using (SqlCommand command = new SqlCommand(collectionCountQuery, connection))
                {
                    int collectionCount = (int)command.ExecuteScalar();
                    textBox_AllCollections.Text = collectionCount.ToString();
                }


                string itemCountQuery = "SELECT COUNT(*) FROM Предметы";
                using (SqlCommand command = new SqlCommand(itemCountQuery, connection))
                {
                    int itemCount = (int)command.ExecuteScalar();
                    textBox_AllItems.Text = itemCount.ToString();
                }
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {

            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "DataBackup.zip";
                saveFileDialog.Filter = "ZIP files (*.zip)|*.zip|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить резервную копию данных";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (var zip = new System.IO.Compression.ZipArchive(File.Create(filePath), System.IO.Compression.ZipArchiveMode.Update))
                        {
                            SaveTableToCsv(zip, connectionString, "Коллекционеры");
                            SaveTableToCsv(zip, connectionString, "Коллекции");
                            SaveTableToCsv(zip, connectionString, "Предметы");
                        }
                        MessageBoxFactory.CreateMessageBox("success").Show("Данные успешно сохранены в файл: " + filePath, "Информация");

                    }
                    catch (Exception ex)
                    {
                        MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при сохранении данных: " + ex.Message, "Ошибка");
                    }
                }
            }
        }
        private void SaveTableToCsv(System.IO.Compression.ZipArchive zip, string connectionString, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string query = tableName == "Коллекционеры"
                    ? $"SELECT * FROM {tableName} WHERE права != 'admin'"
                    : $"SELECT * FROM {tableName}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var zipEntry = zip.CreateEntry($"{tableName}.csv");

                        using (var writer = new StreamWriter(zipEntry.Open()))
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader.GetName(i) + (i < reader.FieldCount - 1 ? "," : ""));
                            }
                            writer.WriteLine();


                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    writer.Write(reader[i].ToString() + (i < reader.FieldCount - 1 ? "," : ""));
                                }
                                writer.WriteLine();
                            }
                        }
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "ZIP files (*.zip)|*.zip|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл резервной копии данных";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        using (var zip = ZipFile.OpenRead(filePath))
                        {
                            foreach (var entry in zip.Entries)
                            {
                                if (entry.FullName.EndsWith(".csv"))
                                {
                                    using (var reader = new StreamReader(entry.Open()))
                                    {

                                        reader.ReadLine();

                                        while (!reader.EndOfStream)
                                        {
                                            var line = reader.ReadLine();
                                            var values = line.Split(',');


                                            InsertDataIntoTable(connectionString, entry.FullName.Replace(".csv", ""), values);
                                        }
                                    }
                                }
                            }
                        }
                        MessageBoxFactory.CreateMessageBox("success").Show("Данные успешно восстановлены.", "Информация");

                        LoadForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при восстановлении данных: " + ex.Message, "Ошибка");
                    }
                }
            }
        }
        private void InsertDataIntoTable(string connectionString, string tableName, string[] values)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";

                switch (tableName)
                {
                    case "Коллекционеры":
                        query = "INSERT INTO Коллекционеры (коллекционер_id, имя, электронная_почта, пароль, номер_телефона,дата_регистрации, права, доступ) VALUES (@id, @name, @email, @password, @phone, @registrationDate,@rights, @status)";
                        break;
                    case "Коллекции":
                        query = "INSERT INTO Коллекции (коллекция_id, коллекционер_id, название, описание, дата_создания) VALUES (@collectionId, @collectorId, @name, @description, @registrationDate)";
                        break;
                    case "Предметы":
                        query = "INSERT INTO Предметы (предмет_id, коллекция_id, название, путь_к_изображению, описание, оценочная_стоимость, дата_приобретения, состояние, количество) VALUES (@Id,@collectionId, @name, @imagePath, @description, @value, @registrationDate, @condition,@quantity)";
                        break;
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    switch (tableName)
                    {
                        case "Коллекционеры":
                            command.Parameters.AddWithValue("@id", values[0]);
                            command.Parameters.AddWithValue("@name", values[1]);
                            command.Parameters.AddWithValue("@email", values[2]);
                            command.Parameters.AddWithValue("@password", values[3]);
                            command.Parameters.AddWithValue("@phone", values[4]);
                            command.Parameters.AddWithValue("@registrationDate", Convert.ToDateTime(values[5]));
                            command.Parameters.AddWithValue("@rights", values[6]);
                            command.Parameters.AddWithValue("@status", values[7]);


                            break;
                        case "Коллекции":
                            command.Parameters.AddWithValue("@collectionId", Convert.ToInt32(values[0]));
                            command.Parameters.AddWithValue("@collectorId", Convert.ToInt32(values[1]));
                            command.Parameters.AddWithValue("@name", values[2]);
                            command.Parameters.AddWithValue("@description", values[3]);
                            command.Parameters.AddWithValue("@registrationDate", Convert.ToDateTime(values[4]));
                            break;
                        case "Предметы":
                            command.Parameters.AddWithValue("@Id", Convert.ToInt32(values[0]));
                            command.Parameters.AddWithValue("@collectionId", Convert.ToInt32(values[1]));
                            command.Parameters.AddWithValue("@name", values[2]);
                            command.Parameters.AddWithValue("@imagePath", values[3]);
                            command.Parameters.AddWithValue("@description", values[4]);
                            command.Parameters.AddWithValue("@value", Convert.ToInt32(values[5]));
                            command.Parameters.AddWithValue("@registrationDate", Convert.ToDateTime(values[6]));
                            command.Parameters.AddWithValue("@condition", values[7]);
                            command.Parameters.AddWithValue("@quantity", Convert.ToInt32(values[8]));

                            break;
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        private void LoadUsers()
        {
            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT коллекционер_id, имя FROM Коллекционеры WHERE права <> 'admin'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        comboBox_Users.Items.Clear();

                        while (reader.Read())
                        {
                            var user = new { Text = reader["имя"].ToString(), Value = reader["коллекционер_id"] };
                            comboBox_Users.Items.Add(user);
                        }
                    }
                }
            }

            comboBox_Users.DisplayMember = "Text";
            comboBox_Users.ValueMember = "Value";
        }
        private void button_blocking_Click(object sender, EventArgs e)
        {
            if (comboBox_Users.SelectedItem == null)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Пожалуйста, выберите пользователя для блокировки.", "Ошибка");
                return;
            }

            var selectedUser = (dynamic)comboBox_Users.SelectedItem;
            int collectorId = selectedUser.Value;


            ChangeUser_Access(collectorId, 0);
        }

       
        private void ChangeUser_Access(int collectorId, int accessStatus)
        {
            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Коллекционеры SET доступ = @accessStatus WHERE коллекционер_id = @collectorId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@collectorId", collectorId);
                    command.Parameters.AddWithValue("@accessStatus", accessStatus);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            string action = accessStatus == 0 ? "блокирован" : "разблокирован";
                            MessageBoxFactory.CreateMessageBox("success").Show($"Пользователь успешно {action}.", "Успех");
                        }
                        else
                        {
                            MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при изменении статуса пользователя.", "Ошибка");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxFactory.CreateMessageBox("error").Show("Ошибка: " + ex.Message, "Ошибка");
                    }
                }
            }
        }

        private void button_unlock_Click_1(object sender, EventArgs e)
        {
            if (comboBox_Users.SelectedItem == null)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Пожалуйста, выберите пользователя для разблокировки.", "Ошибка");
                return;
            }

            var selectedUser = (dynamic)comboBox_Users.SelectedItem;
            int collectorId = selectedUser.Value;


            ChangeUser_Access(collectorId, 1);
        }
    }
}
