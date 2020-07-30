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
    /// Логика взаимодействия для Admin1.xaml
    /// </summary>
    [Serializable]
    public class Placements
    {
        public List<Placement> PlacementsList { get; set; } = new List<Placement>();

    }
    public class MainWindowViewModel : List<string>
    {
        public void MainWindowViewModel1()
        {

            SqlConnection connection1;       // для создания канала связи между программой и источником данных 
            SqlCommand command1;
            DataTable dr1 = new DataTable();
            connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString);
            connection1.Open();
            string sqlExpression = "exec PlaceSelectMark";
            command1 = new SqlCommand(sqlExpression, connection1);
            SqlDataReader reader = command1.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                this.Add(name);

            }
            this.Add("");
        }
    }
    [Serializable]
    public class Placement
    {
        public string CarPlacement { get; set; }
        public int PlacementID { get; set; }

        // стандартный конструктор без параметров
        public Placement()
        { }

        public Placement(string Place, int ID)
        {
            CarPlacement = CarPlacement;
            PlacementID = PlacementID;
        }
    }
    
    public partial class Admin1 : Window
    {
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Worksheet xlSheet;
        Microsoft.Office.Interop.Excel.Range xlSheetRange;
        public static string Uzverzzz;
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
        public static string BackupString = ConfigurationManager.ConnectionStrings["Connection2"].ConnectionString;

        DataTable dataJournal;             // таблица бд
        DataTable dataComp;             // таблица бд
        DataTable dataPlace = new DataTable();

        SqlDataAdapter adapter1;        // для заполнения DataSet (образ бд) и обновления БД 
                                        //SqlDataAdapter adapter2;
        SqlConnection connection1;       // для создания канала связи между программой и источником данных 
        SqlConnection BackupCon;
        SqlCommand command1;
        SqlDataAdapter adapter2;
        SqlDataAdapter adapter3;

        public Admin1()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMark form = new AddMark();
            form.Show();

        }
        private void BtnGraph_Click(object sender, RoutedEventArgs e)
        {
            Window2 form = new Window2();
            form.Show();

        }
        private void Admin1_Closed(object sender, EventArgs e)
        {

            Authoriz form = new Authoriz();
            form.Show();

        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                string sqlExpression = "exec ArendationAll";
                dataJournal = new DataTable();
                command1 = new SqlCommand(sqlExpression, connection1);
                adapter2 = new SqlDataAdapter(command1);
                adapter2.Fill(dataJournal);
                dataGridJournal.ItemsSource = dataJournal.DefaultView;
                string sqlExpression2 = "exec ArendationSum";
                SqlCommand command2 = new SqlCommand(sqlExpression2, connection1);
                Do.Text = "Прибыль: " + command2.ExecuteScalar().ToString();
                string sqlExpression3 = "exec CarSelectAdmin";
                dataComp = new DataTable();
                command1 = new SqlCommand(sqlExpression3, connection1);
                adapter1 = new SqlDataAdapter(command1);
                adapter1.Fill(dataComp);
                dataGridComp.ItemsSource = dataComp.DefaultView;

                string sqlExpression4 = "exec PlaceSelectAdmin";
                dataPlace.TableName = "Placement";

                command1 = new SqlCommand(sqlExpression4, connection1);
                adapter3 = new SqlDataAdapter(command1);
                adapter3.Fill(dataPlace);
                dataGridPlacement.ItemsSource = dataPlace.DefaultView;
                connection1.Close();
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
        public void btn_serialize_Click(object sender, RoutedEventArgs e)
        {

            XmlSerializer xml = new XmlSerializer(typeof(DataTable));
            if (System.IO.File.Exists("D:\\TestData.xml"))
            {
                System.IO.File.Delete("D:\\TestData.xml");
            }

                using (FileStream fs = new FileStream("D:\\TestData.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, dataPlace);
            }
            MessageBox.Show("xml выгружен в D:\\TestData.xml");
        }

        public void btn_deserialize_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists("D:\\TestData.xml"))
            {
                string path = @"d:\TestData.xml";
                using (StreamReader fs = new StreamReader(path))
                {
                    DataTable Des = new DataTable();

                    Des.Clear();
                    XmlSerializer xml = new XmlSerializer(typeof(DataTable));
                    Des = (DataTable)xml.Deserialize(fs);
                    dataGridPlacement.ItemsSource = Des.DefaultView;
                    IList rows2 = dataGridPlacement.Items;
                    connection1 = new SqlConnection(connectionString);
                    connection1.Open();

                    foreach (DataRow row in Des.Rows)
                    {
                        var cells = row.ItemArray;
                        string Place = cells[0].ToString();
                        string ID = cells[1].ToString();
                        string NumOfCars = cells[2].ToString();
                        string Phone = cells[3].ToString();
                        string Address = cells[4].ToString();

                        string sqlExpression = "exec SelectFTable @CarPlacement=N'" + Place + "',@NumOfCars=N'" + NumOfCars + "',@Phone=N'" + Phone + "',@Address=N'" + Address + "'";
                        command1 = new SqlCommand(sqlExpression, connection1);
                        adapter3 = new SqlDataAdapter(command1);
                        adapter3.Fill(dataPlace);
                    }
                    connection1.Close();
                }
                dataPlace = new DataTable();

                string sqlExpression4 = "exec PlaceSelectAdmin";
                dataPlace.TableName = "Placement";

                command1 = new SqlCommand(sqlExpression4, connection1);
                adapter3 = new SqlDataAdapter(command1);
                adapter3.Fill(dataPlace);
                dataGridPlacement.ItemsSource = dataPlace.DefaultView;
                connection1.Close();
            }

            else
            {
                MessageBox.Show("файл отсутствует");
                return;
            }
           
        }

        private void FillCombobox()
        {
            connection1 = new SqlConnection(connectionString);

            try
            {
                DataTable dr = new DataTable();
                connection1.Open();
                string sqlExpression = "exec PlaceSelectMark";
                command1 = new SqlCommand(sqlExpression, connection1);
                SqlDataReader reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    //int name123 = reader.GetInt32(1);
                    //string name1 = name123.ToString();
                    string name2 = name;
                    Mark.Items.Add(name2);
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
            if (Mark.Text == "")
            {
                MessageBox.Show("Выберите марку авто");
                return;
            }
            if(Convert.ToInt32(Number.Text)<0)
            {
                MessageBox.Show("Для номера авто используются символы 1,2,3,4,5,6,7,8,9,0");
                return;
            }
            if (Convert.ToInt32(Number.Text) == 0)
            {
                MessageBox.Show("введите номер авто");
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "exec NewAutoAdd @CarNumber=N'" + Convert.ToInt32(Number.Text) + "'";
                    SqlCommand command2 = new SqlCommand(sql, con);
                    if (command2.ExecuteScalar() == null)
                    {
                        string mark = Mark.Text;

                        string autom = "exec AutoSelectCombo @Marktab=N'" + mark + "'";
                        SqlCommand command3 = new SqlCommand(autom, con);
                        int IDMARK = Convert.ToInt32(command3.ExecuteScalar());

                        string insertDat = "exec InsertCarTab @ID=N'" + IDMARK + "', @Num=N'" + Convert.ToInt32(Number.Text) + "'";
                        SqlCommand command4 = new SqlCommand(insertDat, con);
                        command4.ExecuteScalar();

                        string sqlExpression3 = "exec CarSelectAdmin";
                        dataComp = new DataTable();
                        command1 = new SqlCommand(sqlExpression3, connection1);
                        adapter1 = new SqlDataAdapter(command1);
                        adapter1.Fill(dataComp);
                        dataGridComp.ItemsSource = dataComp.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Номер автомобиля не уникален");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



        }
        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            BackupCon = new SqlConnection(BackupString); 
            BackupCon.Open();
            string sqlExpressionBack = "exec BackupData";
            SqlCommand command4 = new SqlCommand(sqlExpressionBack, BackupCon);
            command4.ExecuteNonQuery();
            BackupCon.Close();
            MessageBox.Show("Резервная копия в D:\\CurseWork1.bak");

        }
        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Действующая база данных будет замещена базой из резервной копии");

            connection1.Close();

            BackupCon = new SqlConnection(BackupString);
            BackupCon.Open();
            string sqlExpressionDrop = "exec DropTable";

            SqlCommand command4 = new SqlCommand(sqlExpressionDrop, BackupCon);

            command4.ExecuteNonQuery();

            BackupCon.Close();

            BackupCon = new SqlConnection(BackupString);
            BackupCon.Open();
            string sqlExpressionRest = "exec RestoreData";

            SqlCommand command5 = new SqlCommand(sqlExpressionRest, BackupCon);
            command5.CommandTimeout = 120;

            command5.ExecuteNonQuery();

            BackupCon.Close();

        }
       


        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridComp.SelectedItems != null)
            {

                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                IList rows = dataGridComp.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                string sqlExpression4 = "exec InsertInUse @Number=N'" + d["NumberOfAuto"] + "'";

                SqlCommand command4 = new SqlCommand(sqlExpression4, connection1);
                int a = (int)command4.ExecuteScalar();
                connection1.Close();


                //DataRowView d =(DataRowView) dataGridComp.SelectedItems;

                if (a == 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            string sqlExpression5 = "exec CountPlace @Place1=N'" + 1 + "'";
                            string sqlExpression6 = "exec AllPlace @PlaceAll=N'" + 1 + "'";

                            SqlCommand command6 = new SqlCommand(sqlExpression5, con);
                            SqlCommand command7 = new SqlCommand(sqlExpression6, con);
                            int ab = Convert.ToInt32(command6.ExecuteScalar());
                            int bc = Convert.ToInt32(command7.ExecuteScalar());
                            if (ab >= bc)
                            {
                                MessageBox.Show("Мест нет");
                                return;
                            }


                            string sql = "exec AddNewOrder @Start=N'" + DateTime.Now.ToString("yyyy-MM-dd") + "', @DateEnd=N'" + DateTime.Now.ToString("yyyy-MM-dd") + "',@Days=N'" + 1 + "',@ToPay=N'" + 0 + "'," +
                                "@NumberOfAuto=N'" + d["NumberOfAuto"] + "',@DataOfDock=N'" + DateTime.Now.ToString("yyyy-MM-dd") + "',@Users=N'" + "Admin" + "',@PlacementID=N'" + 1 + "'";

                            SqlCommand cmd = new SqlCommand(sql, con);

                            cmd.ExecuteNonQuery();
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
                }
                else
                {
                    MessageBox.Show("В системе существует движение по автомобилю");
                    return;
                }
            }
        }

        private void BtnUpdate_Click1(object sender, RoutedEventArgs e)
        {

            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter3);
            adapter3.Update(dataPlace);

        }
        private void UpdateDBPlacement()
        {

            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter3);
            adapter3.Update(dataPlace);
            
        }



        private void dataGridPlacement_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            try
            {
                if (headername == "Число мест")
                {
                    int newValue = Convert.ToInt32(((TextBox)e.EditingElement).Text);
                    if (newValue < 0)
                    {
                        MessageBox.Show("Число мест должно быть больше ноля");
                        int val = newValue * (-1);
                        ((TextBox)e.EditingElement).Text = val.ToString();
                        return;
                    }
                    if (newValue == 0)
                    {
                        MessageBox.Show("Число мест должно быть больше ноля");
                        int val = newValue + 1;
                        ((TextBox)e.EditingElement).Text = val.ToString();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите число, а не букву");
                return;
            }
        }





        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridComp.SelectedItems != null)
            {

                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                IList rows = dataGridComp.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                string sqlExpression4 = "exec DeleteAuto @CarNum=N'"+ d["NumberOfAuto"] + "'";
                    
                SqlCommand command4 = new SqlCommand(sqlExpression4, connection1);
                 int a = (int)command4.ExecuteScalar();
                connection1.Close();

               
                //DataRowView d =(DataRowView) dataGridComp.SelectedItems;

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
                    MessageBox.Show("В системе существует движение по автомобилю");
                    return;
                }
            }

            UpdateDBPlacement();
        }

        private void BtnDel_Click_Place(object sender, RoutedEventArgs e)
        {
            if (dataGridPlacement.SelectedItems != null)
            {
                connection1 = new SqlConnection(connectionString);
                connection1.Open();
                IList rows = dataGridPlacement.SelectedItems;
                DataRowView d = rows[0] as DataRowView;
                string sqlExpression4 = "exec DeletePlacement @PlacementID=N'" + d["PlacementID"] + "'";

                SqlCommand command4 = new SqlCommand(sqlExpression4, connection1);
                int a = (int)command4.ExecuteScalar();
                connection1.Close();
                if (a == 0)
                {
                    for (int i = 0; i < dataGridPlacement.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = dataGridPlacement.SelectedItems[i] as DataRowView;
                        if (datarowView != null)
                        {
                            DataRow dataRow = (DataRow)datarowView.Row;
                            dataRow.Delete();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить стоянку так как на ней размещены автомобили");
                    return;
                }

            }

            UpdateDBPlacement();
        }
        private void UpdateByDate_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlExpression = "exec PartForDate @Begin=N'"+Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" +DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                    dataJournal = new DataTable();
                    command1 = new SqlCommand(sqlExpression, con);
                    adapter2 = new SqlDataAdapter(command1);
                    adapter2.Fill(dataJournal);
                    dataGridJournal.ItemsSource = dataJournal.DefaultView;
                    string sqlExpression1 = "exec SumForDate @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                    SqlCommand command2 = new SqlCommand(sqlExpression1, con);
                    Do.Text = "Прибыль: " + command2.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();

            }
        }
        private DataTable GetData()
        {

            //строка соединения
            SqlConnection connection8 = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {
                string sqlExpression8 = "exec PartForDate @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command8 = new SqlCommand(sqlExpression8, connection8);
                connection8.Open();
                SqlDataAdapter da = new SqlDataAdapter(command8);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                connection8.Close();
                connection8.Dispose();
            }
            return dt;
        }
        //Освобождаем ресуры (закрываем Excel)
        void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Ошибка!");
            }
            finally
            {
                GC.Collect();
            }
        }
        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            xlApp = new Excel.Application();

            try
            {
                //добавляем книгу

                xlApp.Workbooks.Add(Type.Missing);
                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;

                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Данные";

                //Выгрузка данных
                DataTable dt = GetData();

                int collInd = 0;
                int rowInd = 0;
                string data = "";

                //называем колонки
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data = dt.Columns[i].ColumnName.ToString();
                    xlSheet.Cells[1, i + 1] = data;

                    //выделяем первую строку
                    xlSheetRange = xlSheet.get_Range("A1:Z1", Type.Missing);

                    //делаем полужирный текст и перенос слов
                    xlSheetRange.WrapText = true;
                    xlSheetRange.Font.Bold = true;
                }

                //заполняем строки
                for (rowInd = 0; rowInd < dt.Rows.Count; rowInd++)
                {
                    for (collInd = 0; collInd < dt.Columns.Count; collInd++)
                    {
                        data = dt.Rows[rowInd].ItemArray[collInd].ToString();
                        xlSheet.Cells[rowInd + 2, collInd + 1] = data;
                    }
                }

                //выбираем всю область данных
                xlSheetRange = xlSheet.UsedRange;

                //выравниваем строки и колонки по их содержимому
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;

                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;

                //Отсоединяемся от Excel
                releaseObject(xlSheetRange);
                releaseObject(xlSheet);
                releaseObject(xlApp);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }

    }
}

  

