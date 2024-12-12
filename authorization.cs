using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace program
{
    public partial class authorization : Form
    {
        public authorization()
        {
            InitializeComponent();
        }

        private void button_authorization_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_email.Text) || string.IsNullOrWhiteSpace(textBox_password.Text))
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Пожалуйста, заполните все поля", "Ошибка");
                return;
            }

            try
            {
                string path =@"Data Source=dbsrv\dub2024; Initial Catalog=oshkinng207b2; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(path))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Коллекционеры WHERE электронная_почта = @Email AND пароль = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", textBox_email.Text);
                        command.Parameters.AddWithValue("@Password", textBox_password.Text);

                        int userCount = (int)command.ExecuteScalar();
                        var (name, id, permission,status) = GetNameAndId(path, textBox_email.Text);

                        if (userCount > 0)
                        {
                            if (status)
                            {
                                MessageBoxFactory.CreateMessageBox("info").Show($"Авторизация прошла успешно!\n Здравствуйте, {name}.", "Информация");
                            
                                if (permission == "admin")
                                {
                                    AdminPanel window = new AdminPanel();
                                    window.Show();
                                }
                                else {
                                    window Window = new window(id);
                                    Window.ShowDialog();
                                }
                            }
                            else MessageBoxFactory.CreateMessageBox("error").Show($"Аккаунт - {name} был заблокирован", "Ошибка");



                        }
                        else MessageBoxFactory.CreateMessageBox("error").Show("Неверная электронная почта или пароль", "Ошибка");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxFactory.CreateMessageBox("error").Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }

        static (string Name, int Id, string Permission, bool Status) GetNameAndId(string connectionString, string email)
        {
            string name = null;
            string permission = null;
            bool status = true;
            int id = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT имя, коллекционер_id, права, доступ FROM Коллекционеры WHERE электронная_почта = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader["имя"].ToString();
                            id = Convert.ToInt32(reader["коллекционер_id"]);
                            status = Convert.ToBoolean(reader["доступ"]);
                            permission = reader["права"].ToString();
                        }
                    }
                }
            }
            return (name, id, permission,status);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }
    }
}
