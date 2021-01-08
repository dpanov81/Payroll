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

            FileExist(fileName);
        }

        /// <summary>
        /// Проверяет существует ли файл, если нет создает необходимый файл.
        /// </summary>
        private void FileExist(string fileName)
        {
            SortingService sortSrv = new SortingService();

            if (!File.Exists(_path))
            {
                using (File.Create(_path)) ;

                RandomDataGeneratorService dataFileCreation = new RandomDataGeneratorService();

                switch (fileName)
                {
                    case "Список сотрудников":
                        SaveDataInFile(dataFileCreation.ListOfEmployees());
                        break;

                    case "Список отработанных часов руководителей":
                        SaveDataInFile(sortSrv.SortingList(dataFileCreation.ListOfHoursWorkedByLeaders()));
                        break;

                    case "Список отработанных часов сотрудников на зарплате":
                        SaveDataInFile(sortSrv.SortingList(dataFileCreation.ListOfEmployeesWorkedHours()));
                        break;

                    case "Список отработанных часов внештатных сотрудников":
                        SaveDataInFile(sortSrv.SortingList(dataFileCreation.ListOfFreelancersWorkedHours()));
                        break;
                }
            }
        }

        /// <summary>
        /// Запись в пустой файл списка строк.
        /// </summary>       
        public void SaveDataInFile(List<string> listString)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_path, false))
                {
                    foreach (var line in listString)
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
    }
}
