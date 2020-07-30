using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Configuration;
using LiveCharts;
using LiveCharts.Wpf;

namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Window2()
        {
            InitializeComponent();
        }
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
       
        
        private void UpdateByDate_Click(object sender, RoutedEventArgs e)
        {

            string choose = Choose.Text;

            if (choose=="Доход по марке")
            {

                List<double> allValues1 = new List<double>();
                List<string> allValues21 = new List<string>();
                SqlConnection connection8 = new SqlConnection(connectionString);
                
                connection8.Open();
                string sqlExpression8 = "exec Practmark @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command9 = new SqlCommand(sqlExpression8, connection8);
                SqlDataReader RDR = command9.ExecuteReader();
                while (RDR.Read())
                {
                    allValues1.Add(Convert.ToDouble(RDR["alias_name"]));
                    allValues21.Add(Convert.ToString(RDR["Mark"]));

                }
                RDR.Close();

                TEST.Series = new SeriesCollection
            {

                new ColumnSeries
                {
                Title = "Марки",
                    Values = new ChartValues<double>(allValues1)
                }

            };

                Labels = allValues21.ToArray();
                DataContext = this;
                connection8.Close();

            }
            else if(choose == "Доход по типу")
            {
                
                List<double> allValues11 = new List<double>();
                List<string> allValues211 = new List<string>();

                SqlConnection connection8 = new SqlConnection(connectionString);

                connection8.Open();
                string sqlExpression8 = "exec PractType @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
               SqlCommand command91 = new SqlCommand(sqlExpression8, connection8);
                SqlDataReader RDR1 = command91.ExecuteReader();
                while (RDR1.Read())
                {
                    allValues11.Add(Convert.ToDouble(RDR1["alias_name"]));
                    allValues211.Add(Convert.ToString(RDR1["AutoType"]));

                }
                RDR1.Close();

               
                TEST.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Типы",
                    Values = new ChartValues<double>(allValues11)
                }
            };
                MessageBox.Show("wwwww");
                Labels = allValues211.ToArray();
                DataContext = this;
                connection8.Close();


            }
            else if (choose == "Доход по авто")
            {

                List<double> allValues = new List<double>();
                List<double> allValues123 = new List<double>();

                List<string> allValues2 = new List<string>();

                SqlConnection connection8 = new SqlConnection(connectionString);


                connection8.Open();
                string sqlExpression8 = "exec testingPractise @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command8 = new SqlCommand(sqlExpression8, connection8);
                SqlDataReader RDR2 = command8.ExecuteReader();
                while (RDR2.Read())
                {
                    allValues.Add(Convert.ToDouble(RDR2["alias_name"]));
                    allValues2.Add(Convert.ToString(RDR2["NumberOfAuto"]));

                }
                RDR2.Close();



                TEST.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Выручка",
                    Values = new ChartValues<double>(allValues)
                }
            };

                Labels = allValues2.ToArray();
                DataContext = this;
                connection8.Close();

            }
            else if (choose == "Расходы по авто")
            {

                List<double> allValues = new List<double>();
                List<double> allValues123 = new List<double>();

                List<string> allValues2 = new List<string>();

                SqlConnection connection8 = new SqlConnection(connectionString);


                connection8.Open();
                string sqlExpression8 = "exec testingPractise @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command8 = new SqlCommand(sqlExpression8, connection8);
                SqlDataReader RDR4 = command8.ExecuteReader();
                while (RDR4.Read())
                {
                    allValues.Add(Convert.ToDouble(RDR4["alias_name"]));
                    allValues2.Add(Convert.ToString(RDR4["NumberOfAuto"]));

                }
                RDR4.Close();

                string sqlExpression911 = "exec Spendings @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command911 = new SqlCommand(sqlExpression911, connection8);
                SqlDataReader RDR5 = command911.ExecuteReader();
                while (RDR5.Read())
                {
                    allValues123.Add(Convert.ToDouble(RDR5["SummName"]));

                }
                TEST.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Выручка",
                    Values = new ChartValues<double>(allValues)
                }
            };

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Расходы",
                    Values = new ChartValues<double>(allValues123)
                });

                Labels = allValues2.ToArray();
                DataContext = this;
                connection8.Close();

            }
            else if (choose == "Прибыль по авто")
            {

                List<double> allValues1 = new List<double>();
                List<string> allValues21 = new List<string>();

                SqlConnection connection8 = new SqlConnection(connectionString);

                connection8.Open();
                string sqlExpression8 = "exec Result @Begin=N'" + Start.SelectedDate.Value.ToString("yyyy-MM-dd") + "',@End=N'" + DateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                SqlCommand command9 = new SqlCommand(sqlExpression8, connection8);
                SqlDataReader RDR6 = command9.ExecuteReader();
                while (RDR6.Read())
                {
                    allValues1.Add(Convert.ToDouble(RDR6["SummName"]));
                    allValues21.Add(Convert.ToString(RDR6["NumberOfAuto"]));

                }

                TEST.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Итого",
                    Values = new ChartValues<double>(allValues1)
                }
            };

                Labels = allValues21.ToArray();
                DataContext = this;
                connection8.Close();

            }

        }



    }
}
