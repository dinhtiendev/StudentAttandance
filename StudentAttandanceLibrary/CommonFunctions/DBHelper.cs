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

        public static DateTime GetDateForSession(DateTime dateStart, DateTime currentDate, DateTime newDate, int numberClass, int index, int status)
        {
            if (index == 0)
            {
                if (numberClass % 2 == 0)
                {
                    currentDate = (status == 1 || status == 2) ? dateStart.AddDays(1) : dateStart;
                }
                else
                {
                    currentDate = (status == 3 || status == 4) ? dateStart : dateStart.AddDays(1);
                }
                return currentDate;
            }

            if (currentDate.DayOfWeek.ToString().Equals("Friday") || currentDate.DayOfWeek.ToString().Equals("Saturday"))
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

        public static int NumberGroupsInTerm(int number)
        {
            switch (number)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 4;
                default:
                    return 0;
            }
        }

        public static int GetTimeSlot(int numberClass, int status)
        {
            var slot1 = new List<int>();
            var slot2 = new List<int>();
            var slot3 = new List<int>();
            var slot4 = new List<int>();
            var slot5 = new List<int>();
            var slot6 = new List<int>();

            ClearList(slot1, slot2, slot3, slot4, slot5, slot6);

            if (status == 1 || status == 3)
            {
                AddFirst(slot1, slot2, slot3, slot4, slot5, slot6);
            }

            if (status == 2 || status == 4)
            {
                AddSecond(slot1, slot2, slot3, slot4, slot5, slot6);
            }

            if (slot1.Contains(numberClass))
            {
                return 1;
            }
            if (slot2.Contains(numberClass))
            {
                return 2;
            }
            if (slot3.Contains(numberClass))
            {
                return 3;
            }
            if (slot4.Contains(numberClass))
            {
                return 4;
            }
            if (slot5.Contains(numberClass))
            {
                return 5;
            }
            if (slot6.Contains(numberClass))
            {
                return 6;
            }
            return 0;
        }

        public static void AddFirst(List<int> list1, List<int> list2, List<int> list3, List<int> list4, List<int> list5, List<int> list6)
        {
            AddElements(list1, 1, 2);
            AddElements(list2, 3, 4);
            AddElements(list3, 5, 6);
            AddElements(list4, 7, 8);
            AddElements(list5, 9, 10);
            AddElements(list6, 11, 12);
        }

        public static void AddSecond(List<int> list1, List<int> list2, List<int> list3, List<int> list4, List<int> list5, List<int> list6)
        {
            AddElements(list1, 3, 4);
            AddElements(list2, 1, 2);
            AddElements(list3, 7, 8);
            AddElements(list4, 5, 6);
            AddElements(list5, 11, 12);
            AddElements(list6, 9, 10);
        }

        public static void AddElements(List<int> list, int param1, int param2)
        {
            list.Add(param1);
            list.Add(param2);
        }

        public static void ClearList(List<int> list1, List<int> list2, List<int> list3, List<int> list4, List<int> list5, List<int> list6)
        {
            list1.Clear();
            list2.Clear();
            list3.Clear();
            list4.Clear();
            list5.Clear();
            list6.Clear();
        }
    }


}
