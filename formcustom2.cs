using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace program
{
    public partial class formcustom2 : Form
    {
        private int collectorId;
        private int? collectionId;
        private TextBox textBox_newName;
        private TextBox textBox_newInformation;
        private Button button_OK;
        private Button button_Exit;
        private Image backgroundImage = Properties.Resources.bg;

        public event Action<string, string> CollectionUp;

        public formcustom2(int collectorId, int? collectionId = null)
        {
            InitializeComponent();
            this.collectorId = collectorId;
            this.collectionId = collectionId;

            if (collectionId.HasValue)
            {
                LoadCollectionData();
                Text = "Редактировать коллекцию";
            }
            else Text = "Добавить коллекцию";
            
        }

        private void InitializeComponent()
        {
            Size = new Size(400, 400);

            textBox_newName = CreateTextBox(89, 94, false, 219, 20);
            textBox_newInformation = CreateTextBox(89, 164, true, 219, 90);

            button_OK = CreateButton("Применить", 12, 319, 171, 30);
            button_OK.Click += (sender, e) => ApplyChanges(textBox_newName.Text, textBox_newInformation.Text);


            button_Exit = CreateButton("Отмена", 201, 319, 171, 30);
            button_Exit.Click += (sender, e) => Close();



            Controls.Add(textBox_newName);
            Controls.Add(textBox_newInformation);
            Controls.Add(button_OK);
            Controls.Add(button_Exit);
            CreateLabel("Коллекция", 103, 26);
            CreateLabel("Название", 12, 94);
            CreateLabel("Описание", 12, 165);

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(backgroundImage, ClientRectangle);
        }
        private void CreateLabel(string text, int x, int y, int width = 100)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 20),
                Font = new Font("Arial", 9.75F, FontStyle.Bold | FontStyle.Italic),
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            Controls.Add(label);
        }

        private TextBox CreateTextBox(int x, int y, bool multiline = false, int width = 100, int height = 20)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Multiline = multiline,
                Size = new Size(width, height),
                Font = new Font("Arial", 9.75F, FontStyle.Bold | FontStyle.Italic)
            };
        }

        private Button CreateButton(string text, int x, int y, int width, int height)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Arial", 9.75F, FontStyle.Bold | FontStyle.Italic)
            };
        }

        private void LoadCollectionData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT название, описание FROM Коллекции WHERE коллекция_id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", collectionId.Value);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox_newName.Text = reader["название"].ToString();
                                textBox_newInformation.Text = reader["описание"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка");
            }
        }

        private void ApplyChanges(string collectionName, string collectionInfo)
        {
            DateTime creationDate = DateTime.Now;

            if (collectionId.HasValue)
            {
                UpdateCollection(collectionId.Value, collectionName, collectionInfo);
            }
            else
            {
                int newCollectionId = GetNextCollectionId();
                if (AddNewCollection(newCollectionId, collectorId, collectionName, collectionInfo, creationDate))
                {
                    MessageBoxFactory.CreateMessageBox("info").Show("Коллекция успешно добавлена.", "Информация");
                    CollectionUp?.Invoke(collectionName, collectionInfo);
                    Close();
                }
                else
                {
                    MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при добавлении коллекции.", "Ошибка");
                }
            }
        }

        private int GetNextCollectionId()
        {
            int maxId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT MAX(коллекция_id) FROM Коллекции";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            maxId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
            }

            return maxId + 1;
        }

        private bool AddNewCollection(int collectionId, int collectorId, string name, string info, DateTime creationDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "INSERT INTO Коллекции (коллекция_id, коллекционер_id, название, описание, дата_создания) VALUES (@collectionId, @collectorId, @name, @info, @creationDate)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@collectionId", collectionId);
                        command.Parameters.AddWithValue("@collectorId", collectorId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@info", info);
                        command.Parameters.AddWithValue("@creationDate", creationDate);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
                return false;
            }
        }

        private void UpdateCollection(int id, string newName, string newInformation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE Коллекции SET название = @newName, описание = @newInformation WHERE коллекция_id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newName", newName);
                        command.Parameters.AddWithValue("@newInformation", newInformation);
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            MessageBoxFactory.CreateMessageBox("info").Show("Данные успешно обновлены.", "Информация");
                            CollectionUp?.Invoke(newName, newInformation);
                            Close();
                        }
                        else
                        {
                            MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при обновлении данных.", "Ошибка");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }
    }
}
