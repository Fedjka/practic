using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для Authoriz.xaml
    /// </summary>
    public partial class Authoriz : Window
    {
        public Authoriz()
        {
            InitializeComponent();
        }
        string GetRole(string Users, string Password)
        {
            string role = "Failed";
            using (var connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                var command = new SqlCommand("exec RoleChoose @Users=N'"+Users+"', @Password=N'"+Password+"'", connection);
                command.Parameters.AddWithValue("@Users", textboxLogin.Text);
                command.Parameters.AddWithValue("@Password", passwordbox_password.Text.GetHashCode().ToString());
                object result = command.ExecuteScalar();
                string kod = Convert.ToString(result);

                if (kod != "")
                {
                    role = kod;
                }
                connection.Close();
            }
            return role;
        }

        private void login_Click(object sender, EventArgs e)
        {
            string role = GetRole(textboxLogin.Text, passwordbox_password.Text.GetHashCode().ToString());
            if (role == "Failed")
            {
                MessageBox.Show("Неверный логин или пароль");
            }
            else
            {
                if (role == "admin")
                {
                    Admin1 form = new Admin1();
                    form.Show();
                    this.Close();
                }
                else if (role == "User")
                {
                    MainWindow form = new MainWindow(textboxLogin.Text);
                    form.Show();
                    this.Close();
                }
            }
        }
        private void reg_Click(object sender, EventArgs e)
        {
            Register form = new Register();
            form.Show();
            this.Close();
        }

        private void textboxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
    }
}


