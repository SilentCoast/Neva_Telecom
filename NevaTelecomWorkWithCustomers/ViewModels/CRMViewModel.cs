
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
{//TODO: FIO and number real time search
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
            //end test data

        }
        private string txtFIO;
        private string txtPhoneNumber;
        private Заявки current;
        public Заявки Current
        {
            get => current; set => current = value;
        }

        private bool stackPanelIsEnabled;
        public bool StackPanelIsEnabled { get => stackPanelIsEnabled; set => stackPanelIsEnabled = value; }

        public List<string> СтатусыЗаявки { get => WorkWithDB.GetСтатусыЗаявки(); }
        public List<string> TypesOfProblem { get=> WorkWithDB.GetTypesOfProblem(); }
        public List<string> KindsOfService { get => WorkWithDB.GetKindsOfService(); }
        private string currentKindOfService;
        public string CurrentKindOfService { get { return currentKindOfService; } 
            set { 
                currentKindOfService = value; 
                Current.Вид_услуги = value;
            } }
        public List<string> TypesOfService { get => WorkWithDB.GetTypesOfService(CurrentKindOfService); }
        public List<string> TypesOfОборудование { get => WorkWithDB.GetОборудование(); }
        public List<string> CustomersFIOs { get => WorkWithDB.GetCustomersFIOs(); }




        private RelayCommand clickSearch;
        public RelayCommand ClickSearch
        {
            get
            {
                return clickSearch ?? (clickSearch = new RelayCommand(obj =>
                {
                        
                        //get info about current customer
                        Current.Абоненты = WorkWithDB.GetCustomer(TxtFIO,TxtPhoneNumber);
                    if (Current.Абоненты == null)   
                    {
                        MessageBox.Show("Абонент не найден");
                        return;
                    }
                        //auto generate номер_заявки
                        Current.Номер_заявки = Current.Абоненты.Лицевой_счет + "/" + DateTime.Now.Day.ToString()
                        + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                        //auto generated date
                        Current.Дата_создания = DateTime.Now;
                        //now can change data in заявка
                        StackPanelIsEnabled = true;
                    
                        
                    
                }));
            }
        }

        private RelayCommand saveЗаявку;
        public RelayCommand SaveЗаявку
        {
            get => saveЗаявку ?? (saveЗаявку = new RelayCommand(obj =>
            {

                if (Current.Статус == "Закрыта")
                {
                    Current.Дата_закрытия = DateTime.Now;
                }
                if (WorkWithDB.SendЗаявка(Current))
                {
                    MessageBox.Show("Заявка сохранена");
                }
                StackPanelIsEnabled = false;
            }));
        }
        public string TxtFIO
        {
            get => txtFIO; set
            {

                txtFIO = value;
                Current.Абоненты.ФИО = value;
            }
        }
        public string TxtPhoneNumber
        {
            get => txtPhoneNumber; set
            {
                txtPhoneNumber = value;
                Current.Абоненты.Номер_телефона = value;
            }
        }
        
    }
}
