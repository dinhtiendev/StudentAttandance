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

        public static DateTime GetDateForSession(DateTime dateStart, DateTime currentDate, DateTime newDate, int numberClass, int index)
        {
            if (index == 0)
            {
                if (numberClass % 2 == 0)
                {
                    currentDate = dateStart.AddDays(1);
                }
                else
                {
                    currentDate = dateStart;
                }
                return currentDate;
            }

            //if (currentDate.DayOfWeek.ToString().Equals("Saturday"))
            //{
            //    newDate = currentDate.AddDays(2);
            //}
            //else

            if (currentDate.DayOfWeek.ToString().Equals("Friday"))
            {
                newDate = currentDate.AddDays(3);
            }
            else
            {
                newDate = currentDate.AddDays(2);
            }

            return newDate;
        }

        public static int GetTimeSlot(int numberClass)
        {
            //var slot1 = new int[] { 1, 2 };
            //var slot2 = new int[] { 3, 4 };
            //var slot3 = new int[] { 5, 6 };
            //var slot4 = new int[] { 7, 8 };
            //var slot5 = new int[] { 9, 10 };
            //var slot6 = new int[] { 11, 12 };
            switch (numberClass)
            {
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                    return 2;
                case 5:
                case 6:
                    return 3;
                case 7:
                case 8:
                    return 4;
                case 9:
                case 10:
                    return 5;
                case 11:
                case 12:
                    return 6;
                default:
                    return 0;
            }
        }

    }
}
