using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInsuranceAPP.Interfaces
{
    public interface ICRUD
    {
        List<object> List();

        int Create();

        void Save();

        void Load(int Id);
    }
}
