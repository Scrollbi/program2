using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace program
{
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        private void registration_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Name.Text) ||
                string.IsNullOrWhiteSpace(textBox_mail.Text) ||
                string.IsNullOrWhiteSpace(textBox_phone_number.Text) ||
                string.IsNullOrWhiteSpace(textBox_password.Text) ||
                string.IsNullOrWhiteSpace(textBox_password_2.Text))
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Не все поля заполнены", "Ошибка");
            }
            else if (textBox_password.Text != textBox_password_2.Text)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Пароли не совпадают", "Ошибка");
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=database; Integrated Security=True"))
                    {
                        connection.Open();
                        int newCollectorId = GetNextCollectorId(connection); 

                        string query = "INSERT INTO Коллекционеры (коллекционер_id, имя, электронная_почта, пароль, номер_телефона, дата_регистрации, права) " +
                                       "VALUES (@CollectorId, @Name, @Email, @Password, @PhoneNumber, @RegistrationDate, 'user')";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@CollectorId", newCollectorId); 
                            command.Parameters.AddWithValue("@Name", textBox_Name.Text);
                            command.Parameters.AddWithValue("@Email", textBox_mail.Text);
                            command.Parameters.AddWithValue("@Password", textBox_password.Text);
                            command.Parameters.AddWithValue("@PhoneNumber", textBox_phone_number.Text);
                            command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                            if (command.ExecuteNonQuery() > 0)
                            {
                                this.Hide();
                                MessageBoxFactory.CreateMessageBox("info").Show("Регистрация успешна!", "Информация");

                                authorization form = new authorization();
                                form.ShowDialog();
                                Close();
                            }
                            else
                            {
                                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при регистрации", "Ошибка");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxFactory.CreateMessageBox("error").Show("Произошла ошибка: " + ex.Message, "Ошибка");
                }
            }
        }

        private int GetNextCollectorId(SqlConnection connection)
        {
            int maxId = 0;

            try
            {
                string query = "SELECT MAX(коллекционер_id) FROM Коллекционеры";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        maxId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Ошибка при получении ID: " + ex.Message, "Ошибка");
            }

            return maxId + 1; 
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 window = new Form1();
            window.ShowDialog();
            this.Close();
        }
    }
}
