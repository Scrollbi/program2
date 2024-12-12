using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace program
{
    public partial class element2 : UserControl
    {
        int id_c, id_p;

        public element2(int id_c, int id_p, string name, string path_, string price, string quality, string information, DateTime date, string value)
        {
            InitializeComponent();
            this.id_c = id_c;
            this.id_p = id_p;
            textBox_Name.Text = name;
            textBox_Price.Text = price;
            textBox_Quality.Text = quality;
            textBox_Information.Text = information;
            textBox_Value.Text = value;
            textBox_Data.Text = date.ToString("dd/MM/yyyy");
            pictureBox1.Image = Image.FromFile(path_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var editForm = new formcustom1(id_p, "edit");
            editForm.ItemUpdated += (newName, newPath, newInfo, newPrice, newDate, newValue, newQuality) =>
            {
                OnUpdated(newName, newPath, newInfo, newPrice, newDate, newValue, newQuality);
            };
            editForm.Show();
        }

        private void OnUpdated(string newName, string newPath, string newInfo, decimal newPrice, string newDate, int newValue, string newQuality)
        {
            textBox_Name.Text = newName;
            textBox_Data.Text = newDate;
            textBox_Information.Text = newInfo;
            textBox_Price.Text = Convert.ToString(newPrice);
            textBox_Value.Text = Convert.ToString(newValue);
            textBox_Quality.Text = newQuality;
            pictureBox1.ImageLocation = newPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void DeleteItem()
        {
            string connectionString =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Предметы WHERE (коллекция_id = @id_c AND предмет_id = @id_p)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_c", id_c);
                        command.Parameters.AddWithValue("@id_p", id_p);
                        command.ExecuteNonQuery();
                    }
                }
                Parent.Controls.Remove(this);
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
    }
}
