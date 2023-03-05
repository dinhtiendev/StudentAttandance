using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.CommonFunctions
{
    public class Calculation
    {
        public static int NumberAbsent(int totalSession, int totalAbsent)
        {
            return (totalAbsent * 100) / totalSession;
        }
    }
}
