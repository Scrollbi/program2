using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace program
{
    public class formcustom1 : Form
    {
        public event Action<string, string, string, decimal, string, int, string> ItemUpdated;
        private int id;
        private string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";
        private string mode;

        private TextBox textBox_newName;
        private TextBox textBox_newInformation;
        private TextBox textBox_newPrice;
        private TextBox textBox_newValue;
        private TextBox textBox_newQuality;
        private TextBox textBox_newPath;
        private TextBox textBox_newDate;
        private Button button_OK;
        private Button button_Exit;

        private Image backgroundImage = Properties.Resources.bg;

        public formcustom1(int id_, string mode_)
        {
            id = id_;
            mode = mode_;
            InitializeComponent();
            if (mode == "edit") LoadCollectionData();
        }

        private void InitializeComponent()
        {
            Size = new Size(400, 400);
            Text = mode == "add" ? "Добавить предмет" : "Редактировать предмет";

            textBox_newName = CreateTextBox(126, 63);
            textBox_newInformation = CreateTextBox(126, 101, true, 230, 90);
            textBox_newPrice = CreateTextBox(12, 227, false, 151, 22);
            textBox_newValue = CreateTextBox(215, 227, false, 141, 22);
            textBox_newQuality = CreateTextBox(15, 271, false, 151, 22);
            textBox_newPath = CreateTextBox(215, 271, false, 141, 22);
            textBox_newDate = CreateTextBox(12, 169);

            button_OK = CreateButton("Применить", 12, 319, 171, 30);
            button_OK.Click += Button_OK_Click;

            button_Exit = CreateButton("Отмена", 201, 319, 171, 30);
            button_Exit.Click += Button_Exit_Click;

            Controls.Add(textBox_newName);
            Controls.Add(textBox_newInformation);
            Controls.Add(textBox_newPrice);
            Controls.Add(textBox_newValue);
            Controls.Add(textBox_newQuality);
            Controls.Add(textBox_newPath);
            Controls.Add(textBox_newDate);
            Controls.Add(button_OK);
            Controls.Add(button_Exit);

            CreateLabel(Text, 103, 26, 200);
            CreateLabel("Название", 12, 63);
            CreateLabel("Описание", 12, 101);
            CreateLabel("Дата", 12, 150);
            CreateLabel("Стоимость", 12, 208);
            CreateLabel("Состояние", 12, 252);
            CreateLabel("Количество", 228, 208);
            CreateLabel("Фотография", 222, 252);

        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(backgroundImage, ClientRectangle);
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

        private void LoadCollectionData()
        {
            try
            {
                textBox_newName.Clear();
                textBox_newInformation.Clear();
                textBox_newPrice.Clear();
                textBox_newValue.Clear();
                textBox_newDate.Clear();
                textBox_newQuality.Clear();
                textBox_newPath.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT название, путь_к_изображению, описание, оценочная_стоимость, дата_приобретения, состояние, количество FROM Предметы WHERE предмет_id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox_newName.Text = reader["название"].ToString();
                                textBox_newInformation.Text = reader["описание"].ToString();
                                textBox_newPrice.Text = reader["оценочная_стоимость"].ToString();
                                textBox_newValue.Text = reader["количество"].ToString();
                                textBox_newDate.Text = reader["дата_приобретения"].ToString();
                                textBox_newQuality.Text = reader["состояние"].ToString();
                                textBox_newPath.Text = reader["путь_к_изображению"].ToString();
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

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (mode == "add") AddNewItem();
            else if (mode == "edit") UpdateItem();
        }

        private void AddNewItem()
        {
            string newName = textBox_newName.Text;
            string newInformation = textBox_newInformation.Text;
            decimal newPrice = Convert.ToDecimal(textBox_newPrice.Text);
            int newValue = Convert.ToInt32(textBox_newValue.Text);
            string newQuality = textBox_newQuality.Text;
            string newPath = textBox_newPath.Text;
            string newDate = textBox_newDate.Text;

            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newInformation) ||
                string.IsNullOrWhiteSpace(Convert.ToString(newPrice)) || string.IsNullOrWhiteSpace(Convert.ToString(newValue)) ||
                string.IsNullOrWhiteSpace(newQuality) || string.IsNullOrWhiteSpace(newPath))
            {
                MessageBoxFactory.CreateMessageBox("warning").Show("Пожалуйста, заполните все поля.", "Предупреждение");
                return;
            }

            int newItemId = GetNextItemId();

            if (ExecuteNonQuery($"INSERT INTO Предметы (предмет_id, коллекция_id, название, путь_к_изображению, описание, оценочная_стоимость, дата_приобретения, состояние, количество) VALUES (@itemId, @collectionId, @name, @path, @info, @price, @date, @quality, @value)",
                new SqlParameter("@itemId", newItemId),
                new SqlParameter("@collectionId", id),
                new SqlParameter("@name", newName),
                new SqlParameter("@path", newPath),
                new SqlParameter("@info", newInformation),
                new SqlParameter("@price", newPrice),
                new SqlParameter("@date", newDate),
                new SqlParameter("@quality", newQuality),
                new SqlParameter("@value", newValue))
            )
            {
                MessageBoxFactory.CreateMessageBox("success").Show("Предмет успешно добавлен.", "Информация");
                ItemUpdated?.Invoke(newName, newPath, newInformation, newPrice, newDate, newValue, newQuality);
                Close();
            }
            else
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при добавлении предмета.", "Ошибка");
            }
        }

        private int GetNextItemId()
        {
            int maxId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MAX(предмет_id) FROM Предметы";
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

        private void UpdateItem()
        {
            string newName = textBox_newName.Text;
            string newInformation = textBox_newInformation.Text;
            decimal newPrice = Convert.ToDecimal(textBox_newPrice.Text);
            int newValue = Convert.ToInt32(textBox_newValue.Text);
            string newQuality = textBox_newQuality.Text;
            string newPath = textBox_newPath.Text;
            string newDate = textBox_newDate.Text;

            if (ExecuteNonQuery($"UPDATE Предметы SET название = @newName, путь_к_изображению = @newPath, описание = @newInformation, оценочная_стоимость = @newPrice, дата_приобретения = @newDate, состояние = @newQuality, количество = @newValue WHERE предмет_id = @id",
                new SqlParameter("@newName", newName),
                new SqlParameter("@newPath", newPath),
                new SqlParameter("@newInformation", newInformation),
                new SqlParameter("@newPrice", newPrice),
                new SqlParameter("@newDate", newDate),
                new SqlParameter("@newQuality", newQuality),
                new SqlParameter("@newValue", newValue),
                new SqlParameter("@id", id))
            )
            {
                MessageBoxFactory.CreateMessageBox("success").Show("Данные успешно обновлены.", "Информация");
                ItemUpdated?.Invoke(newName, newPath, newInformation, newPrice, newDate, newValue, newQuality);
                Close();
            }
            else
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при обновлении предмета.", "Ошибка");
            }
        }

        private bool ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);
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

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
