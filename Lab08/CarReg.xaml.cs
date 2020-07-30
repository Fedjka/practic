using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Lab08
{
    public partial class MainWindow : Window
    {
        public static string Uzverz;
        public static string str;
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        SqlConnection connection;       // для создания канала связи между программой и источником данных 
        SqlCommand command;
        SqlDataAdapter adapter1;        // для заполнения DataSet (образ бд) и обновления БД 
                                        //SqlDataAdapter adapter2;
        DataTable dataComp;             // таблица бд
        //DataTable dataProc;

        public MainWindow()
        {
            InitializeComponent();
            FillCombobox();
            FillComboboxType();
            FillComboboxMark();


        }
        public MainWindow(string e)
        {
            InitializeComponent();
            Uzverz = e;
            FillCombobox();
            FillComboboxType();
            FillComboboxMark();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlExpression7 = "exec TableOrdersSum";
                SqlCommand command6 = new SqlCommand(sqlExpression7, connection);
                command6.ExecuteNonQuery();
                string sqlExpression8 = "exec TableOrder";
                SqlCommand command7 = new SqlCommand(sqlExpression8, connection);
                command7.ExecuteNonQuery();
                string sqlExpression9 = "exec AnotherTableOrder";
                SqlCommand command8 = new SqlCommand(sqlExpression9, connection);
                command8.ExecuteNonQuery();
                string sqlExpression10 = "exec InsertTablePlacement";
                SqlCommand command9 = new SqlCommand(sqlExpression10, connection);
                command9.ExecuteNonQuery();
                string sqlExpression2 = "exec NewSelect";
                SqlCommand command3 = new SqlCommand(sqlExpression2, connection);
                command3.ExecuteNonQuery();
                string sqlExpression3 = "exec SmallOrderSelect";
                SqlCommand command4 = new SqlCommand(sqlExpression3, connection);
                command4.ExecuteNonQuery();
                string sqlExpression4 = "exec Inner1";
                SqlCommand command5 = new SqlCommand(sqlExpression4, connection);
                command5.ExecuteNonQuery();
                string sqlExpression = "exec Window1";
                dataComp = new DataTable();
                command = new SqlCommand(sqlExpression, connection);
                adapter1 = new SqlDataAdapter(command);
                adapter1.Fill(dataComp);
                dataGridComp.ItemsSource = dataComp.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter1);  // автоматич генер-т команды, кот позволяют согласовать изменения, 
                                                                                // SqlCommandBuilder comandbuilder2 = new SqlCommandBuilder(adapter2); // вносимые в объект dataset, со связанной бд
            adapter1.Update(dataComp);
            // adapter2.Update(dataProc);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridComp.SelectedItems != null)
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

            UpdateDB();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Authoriz form = new Authoriz();
            form.Show();
        }
        private void Arendation_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridComp.SelectedItem != null)
            {
                IList rows = dataGridComp.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                //DataRowView d =(DataRowView) dataGridComp.SelectedItems;
                Window1 form2 = new Window1(d["NumberOfAuto"].ToString(), d["Payment"].ToString(), Uzverz.ToString());
                form2.Show();
            }
            else
            {
                MessageBox.Show("Выберите автомобиль из списка");
                return;
            }
        }
        private void Script_Click(object sender, RoutedEventArgs e)
        {

            string place = Placement.Text;
            string type = Type.Text;
            string mark = Mark.Text;
            int place1 = 0;

            if (place != "")
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sqlExpression3 = "exec PlacementSearch @CarPlacement=N'" + place + "'";
                SqlCommand command4 = new SqlCommand(sqlExpression3, connection);
                place1 = Convert.ToInt32(command4.ExecuteScalar());

                connection.Close();

            }


            try
            {
                string sqlExpression = "";

                if ((type != "") || (mark != "") || (place1 != 0))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();


                            if ((type != "") && (mark == ""))
                        {
                            if (place1 == 0)
                            {
                                sqlExpression += "exec Search @Script=N'" + type + "'";
                            }
                            else
                            {
                                sqlExpression += "exec searchComboPlace @Script=N'" + type + "',@Combobox=N'" + place1 + "'";
                            }
                        }
                        else
                        {
                            if ((type == "") && (mark != ""))
                            {
                                if (place1 == 0)
                                {
                                    sqlExpression += "exec SearchAutoMark @Script1=N'" + mark + "'";
                                }
                                else
                                {
                                    sqlExpression += "exec searchComboPlace1 @Script1=N'" + mark + "',@Combobox=N'" + place1 + "'";

                                }
                            }
                            else
                            {
                                if ((place1 == 0) && (mark != "") && (type != ""))
                                {
                                    sqlExpression += "exec SearchAll @Script=N'" + type + "',@Script1=N'" + mark + "'";
                                }
                                else
                                {
                                    if ((place1 != 0) && (mark != "") && (type != ""))
                                        sqlExpression += "exec SearchAllCombo @Script=N'" + type + "',@Script1=N'" + mark + "',@Combobox=N'" + place1 + "'";
                                    else
                                    {
                                        if ((type == "") && (mark == "") && (place1 != 0))
                                            sqlExpression += "exec OnlyPlacements @Combobox=N'" + place1 + "'";

                                    }
                                }
                                
                            }
                        }


                        //WHERE CarTable.TypeOfAuto like '%"+script+"%'";
                        dataComp = new DataTable();
                        command = new SqlCommand(sqlExpression, connection);
                        adapter1 = new SqlDataAdapter(command);
                        adapter1.Fill(dataComp);
                        dataGridComp.ItemsSource = dataComp.DefaultView;
                    }
                }
                else
                {
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        connection.Open();

                        string sqlExpression1 = "exec Window1";
                        dataComp = new DataTable();
                        command = new SqlCommand(sqlExpression1, connection);
                        adapter1 = new SqlDataAdapter(command);
                        adapter1.Fill(dataComp);
                        dataGridComp.ItemsSource = dataComp.DefaultView;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }



        private void Balance_Click(object sender, RoutedEventArgs e)
        {
            BalanceAdd s = new BalanceAdd(Uzverz);
            s.Show();
        }

        private void FillCombobox()
        {
            connection = new SqlConnection(connectionString);

            try
            {
                DataTable dr = new DataTable();
                connection.Open();
                string sqlExpression = "exec PlaceSelectUser";
                command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string name2 = name;
                    Placement.Items.Add(name2);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        private void FillComboboxMark()
        {
            connection = new SqlConnection(connectionString);

            try
            {
                DataTable dr1 = new DataTable();
                connection.Open();
                string sqlExpression = "exec PlaceSelectMark";
                command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    Mark.Items.Add(name);

                }
                Mark.Items.Add("");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        private void FillComboboxType()
        {
            connection = new SqlConnection(connectionString);

            try
            {
                DataTable dr2 = new DataTable();
                connection.Open();
                string sqlExpression = "exec PlaceSelectType";
                command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    Type.Items.Add(name);

                }
                Type.Items.Add("");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

    }
}
