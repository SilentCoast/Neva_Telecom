
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NevaTelecomWorkWithCustomers
{

    public class CRMViewModel:INotifyPropertyChanged
    {
        private Заявки current;

        public CRMViewModel()
        {
            
        }

        public Заявки Current
        {
            get => current; set
            {
                current = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
