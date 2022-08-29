using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NevaTelecomWorkWithCustomers
{
    internal static partial class WorkWithDB
    {
        internal static Абоненты GetCustomer(string FIO, string mobileNumber)
        {
            return (from z in db.Абоненты
                    where (z.ФИО == FIO && z.Номер_телефона == mobileNumber)
                    select z).FirstOrDefault();

        }
        internal static Заявки GetЗаявка()
        {
            return (from z in db.Заявки
                    select z).FirstOrDefault();

        }
        internal static bool SendЗаявка(Заявки zai)
        {
            db.Заявки.Add(zai);

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\"" +
                        $" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch(Exception e)
            {
                
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        internal static List<string> GetСтатусыЗаявки()
        {
            return (from p in db.СтатусыЗаявки select p.Статус).ToList();
        }

        internal static List<string> GetTypesOfProblem()
        {
            return (from p in db.Types_of_Problem select p.Type_of_problem).ToList();
        }
        internal static List<string> GetKindsOfService()
        {
            return (from p in db.Kinds_of_service select p.Kind_of_service).ToList();
        }
        internal static List<string> GetTypesOfService(string kind_of_service)
        {
            var id = (from h in db.Kinds_of_service where h.Kind_of_service == kind_of_service select h.Id).FirstOrDefault();
            return (from p in db.TypesOfServices where (p.KindOfServiceId == id) select p.TypeOfService).ToList();

        }
        internal static List<string> GetОборудование()
        {
            return (from p in db.Оборудование select p.Name).ToList();

        }
        internal static List<string> GetCustomersFIOs()
        {
            return (from p in db.Абоненты select p.ФИО).ToList();

        }

    }
}
