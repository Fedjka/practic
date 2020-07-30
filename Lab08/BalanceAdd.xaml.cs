using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для BalanceAdd.xaml
    /// </summary>
    public partial class BalanceAdd : Window
    {
        public static string Uzverzzz;
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public BalanceAdd()
        {
            InitializeComponent();
        }
        public BalanceAdd(string e)
        {
            InitializeComponent();
            Uzverzzz = e;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    if (Convert.ToInt32(AddMoney.Text) < 0)
                    {
                        MessageBox.Show("Сумма не может быть отрицательной");
                        return;
                    }
                    if (Convert.ToInt32(AddMoney.Text) == 0)
                    {
                        MessageBox.Show("Введите сумму");
                        return;
                    }
                    con.Open();
                    string sqlExpression3 = "exec Balances @Uzverzzz=N'" + Uzverzzz + "'";
                    SqlCommand command2 = new SqlCommand(sqlExpression3, con);
                    int balance = (int)command2.ExecuteScalar();
                    balance += int.Parse(AddMoney.Text);
                    command2.CommandText = "exec UpdateBalance @balance1=N'" + balance + "',@Uzv='N" + Uzverzzz + "'";
                    command2.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
