using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.CommonFunctions
{
    public class ConvertDataTypes
    {
        public static string ConvertDateToDayOfWeek(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }
    }
}
