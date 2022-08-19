
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
            Current.Абоненты.ФИО = "Фастера Клара Герасимовна";
            Current.Абоненты.Номер_телефона = "+7 (909) 182-89-26";
            Current.Абоненты.Лицевой_счет="1";
            Current.Абоненты.Услуги="1";
            // Current.Абоненты = WorkWithDB.GetCustomer(Current.Абоненты.ФИО, Current.Абоненты.Номер_телефона);

        }

        private Заявки current;
        public Заявки Current
        {
            get => current; set => current = value;
        }

        private RelayCommand clickSearch;
        public RelayCommand ClickSearch
        {
            get
            {
                return clickSearch ?? (clickSearch = new RelayCommand(obj =>
                {
                    Current.Абоненты = WorkWithDB.GetCustomer(Current.Абоненты.ФИО, Current.Абоненты.Номер_телефона);

                    Current.Дата_создания = DateTime.Today;
                    StackPanelIsEnabled = true;
                    //MessageBox.Show(Current.Абоненты.Номер_абонента);
                    //MessageBox.Show(Current.Абоненты.Лицевой_счет);
                    //MessageBox.Show(Current.Абоненты.Услуги);
                }));
            }
        }

        public bool StackPanelIsEnabled { get => stackPanelIsEnabled; set => stackPanelIsEnabled = value; }

        private bool stackPanelIsEnabled;

    }
}
