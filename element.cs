using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace program
{
    public partial class element : UserControl
    {
        int id, collectorId;
        public element(int collectorId, int id_, string name, string info, DateTime creationDate)
        {
            InitializeComponent();
            textBox_name.Text = name;
            textBox_info.Text = info;
            textBox_data.Text = creationDate.ToString("dd/MM/yyyy");
            id = id_;
            this.collectorId = collectorId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Information form = new Information(id);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check(id))
            {
                if (DeleteItems(id))
                {
                    if (DeleteCollection(id)) Parent.Controls.Remove(this);
                    else MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при удалении коллекции.", "Ошибка");
                }
                else MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при удалении предметов.", "Ошибка");
            }
            else MessageBoxFactory.CreateMessageBox("error").Show("Запись не найдена.", "Ошибка");
        }

        private bool check(int collectionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Коллекции WHERE коллекция_id = @collectionId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@collectionId", collectionId);
                        return (int)command.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
                return false;
            }
        }

        private bool DeleteItems(int collectionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "DELETE FROM Предметы WHERE коллекция_id = @collectionId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@collectionId", collectionId);
                        return command.ExecuteNonQuery() >= 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
                return false;
            }
        }

        private bool DeleteCollection(int collectionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True"))
                {
                    connection.Open();
                    string query = "DELETE FROM Коллекции WHERE коллекция_id = @collectionId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@collectionId", collectionId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show($"Ошибка: {ex.Message}", "Ошибка");
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            formcustom2 collectionForm = new formcustom2(collectorId, id);
            collectionForm.CollectionUp += OnCollectionUpdated; 
            collectionForm.ShowDialog(); 


        }

        private void OnCollectionUpdated(string newName, string newInfo)
        {
            textBox_name.Text = newName;
            textBox_info.Text = newInfo;
        }
    }
}
