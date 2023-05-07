using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bakery_kr.Entities
{
    public partial class Pastrys
    {
        public string AdminControlsVisibility //Видимость кнопок для администратора
        {
            get
            {
                if (App.CurrentUser.Roled == 1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        public string CostRub //Вид цены товара на главной странице
        {
            get
            {
                    return $"{Cost:N2} ₽";
            }
        }
    }
}
