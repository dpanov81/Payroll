using System;
using System.Collections.Generic;

namespace Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }

        public Employee()
        {

        }
        public Employee(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public Employee(string name, string surname, string role)
        {
            Name = name;
            Surname = surname;
            Role = role;
        }

        public Employee(string line)    // Конструктор для строки считанной из файла
        {
            string[] stringArray = line.Split(',');

            if (stringArray[0] != null)
                Name = stringArray[0];

            if (stringArray[1] != null)
                Surname = stringArray[1];

            if (stringArray[2] != null)
                Role = stringArray[2];
        }

        public Employee FindEmployeeInList(List<Employee> listEmployees)
        {
            // В листе ищем есть ли такой сотрудник (который ввел свои данные при входе в программу)
            foreach (var item in listEmployees)         
            {
                if (item.Name == this.Name && item.Surname == this.Surname)
                {
                    // Если нашли, то присваиваем ему роль
                    this.Role = item.Role;               
                    
                    return this;
                }
            }

            return this;
        }
    }
}
