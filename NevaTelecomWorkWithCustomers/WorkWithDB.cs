using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NevaTelecomWorkWithCustomers
{
    internal static class WorkWithDB
    {
        private static readonly NEVA_TELECOM_DBEntities db = new NEVA_TELECOM_DBEntities();

        internal static List<string> GetEmployeesFIOs()
        {
            return  db.Employees.Select(pr=>pr.FIO).ToList();
        }

        internal static List<string> GetAvailableModulesForFIO(string FIO)
        {
            try
            {
                var positionID = (from p in db.Employees where p.FIO == FIO select p.Position_Id).First();
                var AcessCODE = (from p in db.Positions where p.Id == positionID select p.Access_Code).First();
                return (from p in db.AvailableModules where p.Access_Code == AcessCODE select p.Available_Module).ToList();
            }
            catch { return null; }

        }

        //TODO: connect following 3 methods to 1(issue: code duplicating), add parameter isActive(represents whether select active,inactive or all customers)

        internal static List<Абоненты> GetCustomers(string FIO, string adress , string personalAccount  )
        {
            
            return (from p in db.Абоненты 
                    where( p.ФИО.Contains(FIO) && p.Адрес_прописки.Contains(adress) && p.Лицевой_счет.Contains(personalAccount)) 
                    select p).ToList();
        }

        internal static List<Абоненты> GetActiveCustomers(string FIO, string adress, string personalAccount)
        {
            return (from p in db.Абоненты 
                    where p.Дата_расторжения_договора==null 
                    where (p.ФИО.Contains(FIO) && p.Адрес_прописки.Contains(adress) && p.Лицевой_счет.Contains(personalAccount)) 
                    select p).ToList();
        }
        internal static List<Абоненты> GetNotActiveCustomers(string FIO, string adress, string personalAccount)
        {
            return (from p in db.Абоненты 
                    where p.Дата_расторжения_договора != null
                    where (p.ФИО.Contains(FIO) && p.Адрес_прописки.Contains(adress) && p.Лицевой_счет.Contains(personalAccount))
                    select p).ToList();
        }
        //endOf TODO
        //internal static List<Абоненты> getCustomersTest(string FIO, string adress, string personalAccount, bool? isActive=null)
        //{
        //    return (from p in db.Абоненты
        //        where p.Дата_расторжения_договора != null
        //     where (p.ФИО.Contains(FIO) && p.Адрес_прописки.Contains(adress) && p.Лицевой_счет.Contains(personalAccount))
        //     select p).ToList();
        //}
        internal static List<Event> GetEventsForFIO(string FIO)
        {
            var positionId = (from p in db.Employees where p.FIO == FIO select p.Position_Id).FirstOrDefault();
            var positionName = (from p in db.Positions where p.Id == positionId select p.Name).FirstOrDefault();
            return (from p in db.Events where p.Position_Name == positionName select p).ToList();
        }


    }
    

}
