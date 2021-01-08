using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    /// <summary>
    /// Класс сортировки списка по дате.
    /// </summary>
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
        /// Находит дублирующие строки в списке по дате имени и фамилии, из двух строк делает одну, вторую удаляет.
        /// </summary>
        /// <param name="listHoursWorked"></param>
        /// <returns></returns>
        public List<string> FindDuplicateLines(List<string> listHoursWorked)
        {
            ReportLine repLine = new ReportLine();
            DateTime date = new DateTime();
            DateTime nextDate = new DateTime();
            int nextItem;

            for (int i = 0; i < listHoursWorked.Count; i++)
            {
                date = repLine.GetDateFromString(listHoursWorked[i]);

                if (i < listHoursWorked.Count - 1)
                {
                    nextItem = i + 1;
                    nextDate = repLine.GetDateFromString(listHoursWorked[nextItem]);

                    if (date == nextDate)
                    {
                        var nameAndSurname = repLine.GetNameAndSurnameFromString(listHoursWorked[i]);
                        var nextNameAndSurname = repLine.GetNameAndSurnameFromString(listHoursWorked[nextItem]);

                        // Если имя и фамилия в строке, равна имени и фамилии в селдующей строке списка.
                        if (nameAndSurname.Item1 == nextNameAndSurname.Item1 && nameAndSurname.Item2 == nextNameAndSurname.Item2)
                        {
                            // Получаем все данные из строки.
                            var data = repLine.GetDataFromString(listHoursWorked[i]);
                            // Получаем рабочие часы и задачу из следующей строки.
                            var nextData = repLine.GetHoursAndTaskFromString(listHoursWorked[nextItem]);

                            // Складываем рабочие часы из двух строк.
                            data.Item4 += nextData.Item1;
                            // Создаем новую строку с задачами из текущей строки и следующей строки.
                            data.Item5 = $"{data.Item5}, {nextData.Item2}";

                            // Создаем новую строку.
                            string newLine = repLine.CreateReportLine(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5);
                            // Удаляем следующую строку.
                            listHoursWorked.RemoveAt(nextItem);
                            // Записываем новую строку по текущему индексу.
                            listHoursWorked[i] = newLine;

                            i--;
                        }
                    }
                }
            }
            return listHoursWorked;
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
