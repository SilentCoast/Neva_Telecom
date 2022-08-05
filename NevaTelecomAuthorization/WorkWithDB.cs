using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NevaTelecomAuthorization
{
    internal static class WorkWithDB
    {
        private static readonly NEVA_TELECOM_DBEntities db = new NEVA_TELECOM_DBEntities();

        internal static bool IsEmployeeNumberExists(string number)
        {
            try
            {
                if ( (from p in db.Employees where p.Number==number select p.Number).First()  == number)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }
        
        internal static bool IsPasswordMatch(string number,string password)
        {
            try
            {
                if ((from p in db.Employees where p.Number == number select p.Number).First() == number)
                {
                    if ((from p in db.Employees where p.Number == number select p.Password).First() == password)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        internal static string GetEmployeePosition(string number)
        {
            var positionID = (from p in db.Employees where p.Number == number select p.Position_Id).First();
            var AcessCODE = (from p in db.Positions where p.Id == positionID select p.Name).First();
            
            return AcessCODE;
        }
        

    }
}
