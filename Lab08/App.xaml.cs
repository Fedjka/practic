using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab08
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string connectionString1 = ConfigurationManager.ConnectionStrings["Connection1"].ConnectionString;
        public static string BackupConnect = ConfigurationManager.ConnectionStrings["Connection2"].ConnectionString;

    }

}
