using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    public class FileIOService
    {
        private readonly string _path;

        public FileIOService(string fileName)
        {

            string path = Environment.CurrentDirectory + "\\" + fileName + ".csv";

            _path = path;
        }

        public List<Employee> LoadDataToListEmployee()
        {
            List<Employee> ListEmployees = new List<Employee>();

            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Employee employee = new Employee(line);

                        ListEmployees.Add(employee);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ListEmployees;
        }

        public void AddStringToFile(string line)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_path, true))
                {
                    sw.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Перезаписать список отработанных часов сотрудников в файл.
        /// </summary>        
        public void OverwriteListOfHoursWorkedToFile(List<string> listHoursWorked)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_path, false))
                {
                    foreach (var line in listHoursWorked)
                    {
                        sw.WriteLine(line);
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<string> LoadListOfHoursWorked()
        {
            List<string> listHoursWorked = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listHoursWorked.Add(line);
                    }

                    return listHoursWorked;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new List<string>();
        }

        /// <summary>
        /// Загрузка списка отработанных часов по конкретному сотруднику из файла.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Список строк.</returns>
        public List<string> LoadListOfWorkingHoursForSpecificEmployee(Employee employee)
        {
            List<string> listHoursWorked = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    string line;
                    int hours = 0;
                    // Считываем строку из файла пока строки не закончатся.
                    while ((line = sr.ReadLine()) != null)          
                    {
                        // Делим строки на подстроки на основании запятой и записываем в массив строк.
                        string[] stringArray = line.Split(',');

                        // Если вторая строка не пустая (там лежит имя и фамилия в одной строке)
                        if (stringArray[1] != null)             
                        {
                            // Делим вторую строку в массиве по пробелу и записываем в новый массив, теперь там 2 элемента (Имя и Фамилия)
                            string[] stringArrayNameAndSurname = stringArray[1].Split(' ');

                            // Если имя и фамилия в массиве совпадают с именем и фамилией в объекте employee
                            if (stringArrayNameAndSurname[0] == employee.Name && stringArrayNameAndSurname[1] == employee.Surname)      
                            {
                                // Формируем строку отчета в формате: дд.мм.гггг, (часы) часов, что делал сотрудник
                                line = stringArray[0] + ", " + stringArray[2] + " час(а/ов)" + ", " + stringArray[3];

                                // Добавляем строку отчета в лист строк                                
                                listHoursWorked.Add(line);              

                                if (stringArray[2] != null)
                                    // Третий элемент массива - кол-во отработанных часов.
                                    hours += int.Parse(stringArray[2]);         
                            }
                        }                        
                    }

                    if (hours == 0)
                        return new List<string>();

                    // В последний элемент массива записываем общее кол-во отработанных часов.
                    listHoursWorked.Add(hours.ToString());          

                    return listHoursWorked;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new List<string>();
        }


        //public void SaveData(List<Employee> e)
        //{

        //}
    }
}
