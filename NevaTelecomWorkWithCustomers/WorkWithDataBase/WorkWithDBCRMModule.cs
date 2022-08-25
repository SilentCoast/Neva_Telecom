using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        internal static void SendЗаявка(Заявки zai)
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

    }
}
