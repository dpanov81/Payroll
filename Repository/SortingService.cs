using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SortingService
    {
        /// <summary>
        /// Определяет нужна ли сортировка по дате, списка отработанных часов сотрудников.
        /// </summary>        
        /// <returns>true если сортировка нужна, false если не нужна.</returns>
        public bool NeedSorting(List<string> listOfHoursWorked)
        {
            // Если один элемент в списке, то сортировка не требуется.
            if (listOfHoursWorked.Count == 1)
                return false;
            else
            {
                DateTime date = new DateTime();
                DateTime nextDate = new DateTime();

                for (int i = 0; i < listOfHoursWorked.Count; i++)
                {
                    if (i != (listOfHoursWorked.Count - 1))
                    {
                        date = GetDateFromString(listOfHoursWorked[i]);
                        int nextIndex = i + 1;
                        nextDate = GetDateFromString(listOfHoursWorked[nextIndex]);

                        if (date > nextDate)
                            return true;
                    }
                }                
            }

            return false;
        }

        /// <summary>
        /// Сортировка списка строк отработанных часов сотрудников по дате.        
        /// </summary>        
        /// <returns>Отсортированный List<string>.</returns>
        public List<string> SortingList(List<string> listOfHoursWorked)
        {
            List<string> sortingList = new List<string>();

            DateTime date = new DateTime();
            DateTime nextDate = new DateTime();

            for (int i = 0; i < listOfHoursWorked.Count; i++)
            {
                for (int y = 0; y < (listOfHoursWorked.Count - 1); y++)
                {
                    // Если не последний элемент в списке строк.
                    if (y != (listOfHoursWorked.Count - 1))
                    {                        
                        date = GetDateFromString(listOfHoursWorked[y]);
                        int nextIndex = y + 1;
                        nextDate = GetDateFromString(listOfHoursWorked[nextIndex]);

                        // Если дата больше следующей даты, меняем строки в списке строк местами.
                        if (date > nextDate)
                        {
                            string line = listOfHoursWorked[y];
                            string nextline = listOfHoursWorked[nextIndex];

                            listOfHoursWorked[y] = nextline;
                            listOfHoursWorked[nextIndex] = line;
                        }
                    }
                }
            }             

            return listOfHoursWorked;
        }

        /// <summary>
        /// Получает дату из строки.
        /// </summary>        
        /// <returns>DateTime</returns>
        private DateTime GetDateFromString (string line)
        {
            DateTime date = new DateTime();

            string[] stringArray = line.Split(',');

            date = Convert.ToDateTime(stringArray[0]);

            return date;
        }
    }
}
