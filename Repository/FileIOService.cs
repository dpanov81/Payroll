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
            // Список строк отработанных часов по конкретному сотруднику.
            List<string> listHoursWorked = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    string line;
                    
                    // Считываем строку из файла пока строки не закончатся.
                    while ((line = sr.ReadLine()) != null)
                    {
                        ReportLine reportLine = new ReportLine();

                        if (reportLine.CompareNameAndSurname(line, employee))
                            listHoursWorked.Add(line);                        
                    }

                    return listHoursWorked;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listHoursWorked;
        }


        //public void SaveData(List<Employee> e)
        //{

        //}
    }
}
