﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
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
                    while ((line = sr.ReadLine()) != null)          // Считываем строку из файла пока строки не закончатся.
                    {                        
                        string[] stringArray = line.Split(',');     // Делим строки на подстроки на основании запятой и записываем в массив строк.

                        if (stringArray[1] != null)             // Если вторая строка не пустая (там лежит имя и фамилия в одной строке)
                        {                            
                            string[] stringArrayNameAndSurname = stringArray[1].Split(' '); // Делим вторую строку в массиве по пробелу и записываем в новый массив, теперь там 2 элемента (Имя и Фамилия)

                            if (stringArrayNameAndSurname[0] == employee.Name && stringArrayNameAndSurname[1] == employee.Surname)      // Если имя и фамилия в массиве совпадают с именем и фамилией в объекте employee
                            {
                                line = stringArray[0] + ", " + stringArray[2] + " час(а/ов)" + ", " + stringArray[3];  // Формируем строку отчета в формате: дд.мм.гггг, (часы) часов, что делал сотрудник
                                
                                listHoursWorked.Add(line);              // Добавляем строку отчета в лист строк                                

                                if (stringArray[2] != null)
                                    hours += int.Parse(stringArray[2]);         // Третий элемент массива - кол-во отработанных часов.
                            }
                        }                        
                    }

                    if (hours == 0)
                        return new List<string>();

                    listHoursWorked.Add(hours.ToString());          // В последний элемент массива записываем кол-во отработанных часов.

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
