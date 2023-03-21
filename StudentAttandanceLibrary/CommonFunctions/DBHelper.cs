using StudentAttandanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttandanceLibrary.CommonFunctions
{
    public class DBHelper
    {

        public static List<string> GetAllK(List<Student> students)
        {
            List<string> listK = new List<string>();
            var k = "";
            foreach (Student student in students)
            {
                k = "K" + student.StudentId.Substring(2, 2).ToString();
                if (!listK.Contains(k))
                {
                    listK.Add(k);
                }
            }
            return listK.OrderBy(x => Int32.Parse(x.Substring(1, 2))).ToList();
        }

    }
}
