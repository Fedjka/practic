using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для AddMark.xaml
    /// </summary>
    /// 
    public partial class AddMark : Window
    {
        public AddMark()
        {

            InitializeComponent();
        }
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;

        DataTable DataType = new DataTable();             // таблица бд
        DataTable DataMark = new DataTable();             // таблица бд

        SqlConnection connection1;       // для создания канала связи между программой и источником данных 
        SqlCommand command1;
        SqlCommand command3;

        SqlDataAdapter adapter2;
        SqlDataAdapter adapter3;

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                string sqlExpression = "exec CarSelectMark";
                DataMark = new DataTable();
                command1 = new SqlCommand(sqlExpression, connection1);
                adapter2 = new SqlDataAdapter(command1);
                adapter2.Fill(DataMark);
                dataComp.ItemsSource = DataMark.DefaultView;


                string sqlExpression1 = "exec CarSelectType";
                DataType = new DataTable();
                command3 = new SqlCommand(sqlExpression1, connection1);
                adapter3 = new SqlDataAdapter(command3);
                adapter3.Fill(DataType);
                dataGridComp.ItemsSource = DataType.DefaultView;
                FillCombobox();
                connection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection1.Close();
            }
        }
        private void BtnUpdate_Click1(object sender, RoutedEventArgs e)
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter3);
            adapter3.Update(DataType);

        }

        private void BtnDel_Click_type(object sender, RoutedEventArgs e)
        {
            if (dataGridComp.SelectedItems != null)
            {
                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                IList rows = dataGridComp.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                string sqlExpression4 = "exec DeleteType @Type=N'" + d["AutoType"] + "'";

                SqlCommand command5 = new SqlCommand(sqlExpression4, connection1);
                int a = (int)command5.ExecuteScalar();
                connection1.Close();
                if (a == 0)
                {
                    for (int i = 0; i < dataGridComp.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = dataGridComp.SelectedItems[i] as DataRowView;
                        if (datarowView != null)
                        {
                            DataRow dataRow = (DataRow)datarowView.Row;
                            dataRow.Delete();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить тип, так как автомобили этого типа используются");
                    return;
                }

            }

        }


        private void BtnDel_Click_Mark(object sender, RoutedEventArgs e)
        {
            if (dataComp.SelectedItems != null)
            {
                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                IList rows = dataComp.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                string sqlExpression4 = "exec DeleteMark @Mark=N'" + d["Mark"] + "'";

                SqlCommand command4 = new SqlCommand(sqlExpression4, connection1);
                int a = (int)command4.ExecuteScalar();
                connection1.Close();
                if (a == 0)
                {
                    for (int i = 0; i < dataComp.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = dataComp.SelectedItems[i] as DataRowView;
                        if (datarowView != null)
                        {
                            connection1 = new SqlConnection(connectionString);
                            connection1.Open();
                            string sqlExpression41 = "exec Easydelete @MarkName=N'" + d["Mark"].ToString() + "'";
                            SqlCommand command45 = new SqlCommand(sqlExpression41, connection1);
                            command45.ExecuteNonQuery();
                            connection1.Close();

                            DataRow dataRow = (DataRow)datarowView.Row;
                            dataRow.Delete();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Нельзя удалить модель, так как существуют автомобили этой модели");
                    return;
                }

            }
        }

        private void UpdateDBPlacement()
        {

            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter2);
            adapter2.Update(DataMark);

        }

        private void FillCombobox()
        {
            connection1 = new SqlConnection(connectionString);

            try
            {
                DataTable dr = new DataTable();
                connection1.Open();
                string sqlExpression = "exec PlaceSelectTypeAutoType";
                command1 = new SqlCommand(sqlExpression, connection1);
                SqlDataReader reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    //int name123 = reader.GetInt32(1);
                    //string name1 = name123.ToString();
                    string name2 = name;
                    TypeAuto.Items.Add(name2);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection1.Close();
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (TypeAuto.Text == "")
            {
                MessageBox.Show("Выберите марку авто");
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    if (Convert.ToInt32(PaymentSum.Text) < 0)
                    {
                        MessageBox.Show("Сумма должны быть больше ноля");
                        return;
                    }
                    if (Convert.ToInt32(PaymentSum.Text) == 0)
                    {
                        MessageBox.Show("Введите цену");
                        return;
                    }
                    string sql = "exec MarkSelectDebug @Mark=N'" + MarkAuto.Text + "'";

                    SqlCommand command2 = new SqlCommand(sql, con);
                    if (command2.ExecuteScalar() == null)
                    {
                        string type = TypeAuto.Text;

                        string autom = "exec TypeSelectCombo @Typetab=N'" + type + "'";
                        SqlCommand command3 = new SqlCommand(autom, con);
                        int IDMARK = Convert.ToInt32(command3.ExecuteScalar());

                        string insertDat = "exec InsertMarkTable @type=N'" + IDMARK + "', @MarkName=N'" + MarkAuto.Text + "', @payment=N'" + Convert.ToInt32(PaymentSum.Text) + "'";
                        SqlCommand command4 = new SqlCommand(insertDat, con);
                        command4.ExecuteScalar();

                        string sqlExpression = "exec CarSelectMark";
                        DataMark = new DataTable();
                        command1 = new SqlCommand(sqlExpression, connection1);
                        adapter2 = new SqlDataAdapter(command1);
                        adapter2.Fill(DataMark);
                        dataComp.ItemsSource = DataMark.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Марка не уникальна");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


    }
}
