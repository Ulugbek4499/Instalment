using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installment.States
{
    internal interface Interface1<T>
    {
        bool Write(T value);
        T? Read(string PassportId);
    }
}
