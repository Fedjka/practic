using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Data;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DateTime start1 = DateTime.Now;
        DateTime finish = DateTime.Now;
        public static string Uzverzzz;
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        DataTable dataJournal;             // таблица бд
        SqlConnection connection1;       // для создания канала связи между программой и источником данных 
        SqlCommand command1;

        SqlDataAdapter adapter2;        // для заполнения DataSet (образ бд) и обновления БД 
                                        //SqlDataAdapter adapter2;
        public Window1()
        {
            InitializeComponent();
        }
        public Window1(string s,string d,string e)
        {
            InitializeComponent();
            NumberOfAuto.Text = s;
            PaymentDay.Text = d;
            Uzver.Text = ""+e.ToString();
            Uzverzzz = e; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                connection1 = new SqlConnection(connectionString);
                connection1.Open();

                string sqlExpression = "exec ArendForUser @Uzv=N'" + Uzverzzz + "'";
                dataJournal = new DataTable();
                command1 = new SqlCommand(sqlExpression, connection1);
                adapter2 = new SqlDataAdapter(command1);
                adapter2.Fill(dataJournal);
                dataGridJournal.ItemsSource = dataJournal.DefaultView;
                string sqlExpression2 = "exec SumForUser @Uzv=N'" + Uzverzzz + "'";
                SqlCommand command2 = new SqlCommand(sqlExpression2, connection1);
                Do.Text ="Израсходованно всего: "+ command2.ExecuteScalar().ToString();
                command2.CommandText = "exec UsersBalance @Uzv=N'" + Uzverzzz + "'";
                Balance.Text = command2.ExecuteScalar().ToString();
                FillCombobox();

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

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter2); 
            adapter2.Update(dataJournal);
        }

        public void eventhandler_1(object sender, SelectionChangedEventArgs e)
        {
            start1 = Start.SelectedDate.Value;
            eventhandler_3();
        }
        public void eventhandler_2(object sender, SelectionChangedEventArgs e)
        {
            finish = DateEnd.SelectedDate.Value;
            eventhandler_3();
        }
        private void eventhandler_3()
        {//Реквизиты формы по аренде
            Days.Text = "";
            ToPay.Text = "";
            if (finish < start1)
            {
                MessageBox.Show("Дата начала превышает дату окончания");
                return;
            }
            TimeSpan difference = finish - start1;
            string format = difference.ToString("dd");
            Days.Text = format.ToString();
            int a;
            Int32.TryParse(PaymentDay.Text, out a);
            int b;
            Int32.TryParse(Days.Text, out b);
            int c = a * b;

            ToPay.Text = c.ToString();
            int d;
            Int32.TryParse(ToPay.Text, out d);
            int f;
            Int32.TryParse(Balance.Text, out f);
            
                int j;
                j = f - d;
            if (Uzverzzz != null)
            {
                if (j > 0)
                {
                    Dockkk.Text = "Остаток: " + j.ToString();
                }
                else
                {
                    Dockkk.Text = "Пополните баланс1" + j;
                }
            }
        }
        private void FillCombobox()
        {
            connection1 = new SqlConnection(connectionString);

            try 
            {
                DataTable dr = new DataTable();
                connection1.Open();
                string sqlExpression = "exec PlaceSelectUser";
                command1 = new SqlCommand(sqlExpression, connection1);
                SqlDataReader reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    //int name123 = reader.GetInt32(1);
                    //string name1 = name123.ToString();
                    string name2 = name;
                    Placement.Items.Add(name2);


                }
                //SqlDataAdapter da = new SqlDataAdapter("Select * from Placement", connection);
                //DataSet ds = new DataSet();
                //adapter1.Fill(ds, "Placement");
                //comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
                //comboBoxName.SelectedValuePath = ds.Tables[0].Columns["CarPlacement"].ToString();
                //comboBoxName.DisplayMemberPath = ds.Tables[0].Columns["PlacementID"].ToString();


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
        private void Write_Click(object sender, RoutedEventArgs e)
        {
           

            if ((Days.Text == "") || (Days.Text == "00"))
                {
                MessageBox.Show("Нельзя арендовать машину меньше чем на сутки");
                return;
            }
            if (Placement.Text == "")
            {
                MessageBox.Show("Выберите место парковки");
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlExpression3 = "exec UsersBalance @Uzv=N'" + Uzverzzz + "'";
                    SqlCommand command2 = new SqlCommand(sqlExpression3, con);
                    SqlCommand command5 = new SqlCommand(sqlExpression3, con);
                    int balance = (int)command2.ExecuteScalar();
                   int balance1 = balance - int.Parse(ToPay.Text);
                    command5.CommandText = "exec AutoSearch @NumberAuto=N'" + NumberOfAuto.Text + "'";
                    command2.CommandText= "exec CarSelectArend @StartTime=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd")+ "',@EndTime=N'"+ DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@Number= N'"
                        + NumberOfAuto.Text+ "'";




                    int place1 = 0;
                    string place = Placement.Text;

                    if (place != "")
                    {
                        connection1 = new SqlConnection(connectionString);
                        connection1.Open();
                        string sqlExpression7 = "exec PlacementSearch @CarPlacement=N'" + place + "'";
                        SqlCommand command4 = new SqlCommand(sqlExpression7, connection1);
                        place1 = Convert.ToInt32(command4.ExecuteScalar());

                        //char ch = '.';
                        //int IndexOfChar = place.IndexOf(ch);
                        //place = place.Substring(0,place.Length-(place.Length-IndexOfChar));
                        con.Close();

                    }
                    con.Open();

                    string sqlExpression5 = "exec CountPlace @Place1=N'" + place1 + "'";
                    string sqlExpression6 = "exec AllPlace @PlaceAll=N'" + place1 + "'";

                    SqlCommand command6 = new SqlCommand(sqlExpression5, con);
                    SqlCommand command7 = new SqlCommand(sqlExpression6, con);
                    int a=Convert.ToInt32(command6.ExecuteScalar());
                       int b= Convert.ToInt32(command7.ExecuteScalar());
                   if(a >= b)
                    {
                        MessageBox.Show("Мест нет");
                        return;
                    }
                    if (balance >= 0)
                    {
                        if (command2.ExecuteScalar() == null || command5.ExecuteScalar().ToString() == "0")
                        {
                            string sqlExpression4 = "exec UpdateBalance @balance1=N'" + balance1 + "',@Uzv='N" + Uzverzzz + "'";
                            SqlCommand command3 = new SqlCommand(sqlExpression4, con);
                            command3.ExecuteNonQuery();


                            string sql = "exec AddNewOrder @Start=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "', @DateEnd=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@Days=N'" + Convert.ToInt32(Days.Text) + "',@ToPay=N'" + Convert.ToInt32(ToPay.Text) + "'," +
                                "@NumberOfAuto=N'" + Convert.ToInt32(NumberOfAuto.Text) + "',@DataOfDock=N'" + DataOfDock.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@Users=N'" + Uzver.Text + "',@PlacementID=N'" + place1 + "'";
                            SqlCommand cmd = new SqlCommand(sql, con);



                            cmd.ExecuteNonQuery();
                            Balance.Text = balance.ToString();

                            

                        }
                        else
                        {
                            MessageBox.Show("Автомобиль занят");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пополните баланс");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void NumberOfAuto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
