using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace program
{
    public partial class window : Form
    {
        private string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";
        private int collectorId;

        public window(int collectorId)
        {
            InitializeComponent();
            this.collectorId = collectorId;
            LoadCollections();
        }

        private void LoadCollections()
        {
            flowLayoutPanel.Controls.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT коллекция_id, название, описание, дата_создания FROM Коллекции WHERE коллекционер_id = @CollectorId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CollectorId", collectorId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_ = Convert.ToInt32(reader["коллекция_id"]);
                                string title = reader["название"].ToString();
                                string description = reader["описание"].ToString();
                                DateTime creationDate = Convert.ToDateTime(reader["дата_создания"]);

                                element itemControl = new element(collectorId, id_, title, description, creationDate);
                                flowLayoutPanel.Controls.Add(itemControl);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка базы данных: " + sqlEx.Message, "Database Error");
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Произошла ошибка: " + ex.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formcustom2 collectionForm = new formcustom2(collectorId);
            collectionForm.CollectionUp += (s, args) => LoadCollections(); 
            collectionForm.ShowDialog(); 
        }
    }
}
