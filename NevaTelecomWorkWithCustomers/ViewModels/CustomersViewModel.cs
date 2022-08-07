
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NevaTelecomWorkWithCustomers
{
    public partial class CustomersViewModel: ObservableObject
    {
        public CustomersViewModel(string employeeFIO)
        {
            events = WorkWithDB.GetEventsForFIO(employeeFIO);
        }
        [ObservableProperty]
        private List<Event> events;
    }
}
