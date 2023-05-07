using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace bakery_kr
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.bakeryEntities Context
        { get; } = new Entities.bakeryEntities();

        public static Entities.User CurrentUser = null;
        public static Entities.Pastrys CurrentPastry = null;
    }
}
