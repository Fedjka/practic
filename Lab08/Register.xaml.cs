using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public Register()
        {
            InitializeComponent();
        }
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textbox_Login.Text = "";
        }

        private void button_Registration_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = "exec Register @Users=N'" + textbox_Login.Text + "',@Password=N'" + passwordbox_Password.Text.GetHashCode().ToString() + "',@Role=N'" + "User" + "',@Balancez=N'" + 0 + "'";
                    conn.Open();
                    SqlCommand command2 = new SqlCommand(sql, conn);
                    command2.CommandText = "exec selectUser @Username=N'" + textbox_Login.Text+"'";
                    if (command2.ExecuteScalar() == null)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("New Client was successfully added!", "");
                        Authoriz form = new Authoriz();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким именем уже зарегстрирован");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);// Лучше обработать все возможные исключения
                }
                finally
                {
                    conn.Close();
                }
            }
        }


    }
}
