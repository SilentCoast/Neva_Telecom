
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NevaTelecomWorkWithCustomers
{
    [AddINotifyPropertyChangedInterface]  
    public class CRMViewModel
    {
        public CRMViewModel()
        {

            Current = new Заявки();
            Current.Абоненты = new Абоненты();
            //test data
            Current.Абоненты.ФИО = "Фастера Клара Герасимовна";
            Current.Абоненты.Номер_телефона = "+7 (909) 182-89-26";
            // Current.Абоненты = WorkWithDB.GetCustomer(Current.Абоненты.ФИО, Current.Абоненты.Номер_телефона);

        }

        private Заявки current;
        public Заявки Current
        {
            get => current; set => current = value;
        }

        private bool stackPanelIsEnabled;
        public bool StackPanelIsEnabled { get => stackPanelIsEnabled; set => stackPanelIsEnabled = value; }

        

        


        private RelayCommand clickSearch;
        public RelayCommand ClickSearch
        {
            get
            {
                return clickSearch ?? (clickSearch = new RelayCommand(obj =>
                {
                    Current.Абоненты = WorkWithDB.GetCustomer(Current.Абоненты.ФИО, Current.Абоненты.Номер_телефона);
                    Current.Номер_заявки = Current.Абоненты.Лицевой_счет + "/" + DateTime.Now.Day.ToString()
                    + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                    Current.Дата_создания = DateTime.Now;
                    StackPanelIsEnabled = true;
                }));
            }
        }

        private RelayCommand saveЗаявку;
        public RelayCommand SaveЗаявку
        {
            get => saveЗаявку ?? (saveЗаявку = new RelayCommand(obj =>
            {
                WorkWithDB.SendЗаявка(Current);
                MessageBox.Show("gg");
            }));
        }

    }
}
