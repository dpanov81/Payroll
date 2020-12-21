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
            foreach (var item in listEmployees)         // В листе ищем есть ли такой сотрудник (который ввел свои данные при входе в программу)
            {
                if (item.Name == this.Name && item.Surname == this.Surname)
                {
                    this.Role = item.Role;          // Если нашли, то присваиваем ему роль и выводим на экран
                    //Output.OutputSurnameAndRole(employee.Name, employee.Surname, employee.Role);
                    return this;
                }
            }

            return this;

            //if (employee.Role == null)                                   // Если не нашли, то выводим на экран что нет такого сотрудника
            //    Output.OutputSurname(employee.Name, employee.Surname);

        }
    }
}
