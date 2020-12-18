using Models;
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

        public List<Employee> LoadData()
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

        //public void SaveData(List<Employee> e)
        //{

        //}
    }
}
