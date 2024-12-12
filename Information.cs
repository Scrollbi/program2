using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace program
{
    public partial class Information : Form
    {
        int id;
        private string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";

        public Information(int id_)
        {
            InitializeComponent();
            id = id_;
            LoadCollections();
        }

        public void LoadCollections()
        {
            flowLayoutPanel.Controls.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT предмет_id, коллекция_id, название, путь_к_изображению, описание, оценочная_стоимость, дата_приобретения, состояние, количество FROM Предметы WHERE коллекция_id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int _id = Convert.ToInt32(reader["предмет_id"]);
                                string name = reader["название"].ToString();
                                string path_ = reader["путь_к_изображению"].ToString();
                                string information = reader["описание"].ToString();
                                string price = reader["оценочная_стоимость"].ToString();
                                DateTime date = Convert.ToDateTime(reader["дата_приобретения"]);
                                string quality = reader["состояние"].ToString();
                                string value = reader["количество"].ToString();

                                element2 itemControl = new element2(id, _id, name, path_, price, quality, information, date, value);
                                flowLayoutPanel.Controls.Add(itemControl);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка базы данных: " + sqlEx.Message, "Ошибка базы данных");
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            var addForm = new formcustom1(id, "add");
            addForm.ItemUpdated += (newName, newPath, newInfo, newPrice, newDate, newValue, newQuality) =>
            {
                LoadCollections();
            };
            addForm.Show();
        }
    }
}
