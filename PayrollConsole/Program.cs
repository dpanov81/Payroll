﻿using Models;
using Repository;
using System;
using System.Collections.Generic;

namespace PayrollConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> listEmployees = new List<Employee>();        // Лист всех сотрудников
            Employee employee = new Employee(Input.InputName(), Input.InputSurname());     // При входе в программу вводим имя и фамилию       

            FileIOService file = new FileIOService("Список сотрудников");   // Для загрузки данных из файла, в констр. указываем название файла(файл лежит рядом с exe-шником)
            listEmployees = file.LoadDataToListEmployee();            // Загружаем список всех сотрудников из файла в лист

            foreach (var item in listEmployees)         // В листе ищем есть ли такой сотрудник (который ввел свои данные при входе в программу)
            {
                if (item.Name == employee.Name && item.Surname == employee.Surname)
                {
                    employee.Role = item.Role;          // Если нашли, то присваиваем ему роль и выводим на экран
                    Output.OutputSurnameAndRole(employee.Name, employee.Surname, employee.Role);
                    break;
                }
            }

            if (employee.Role == null)                                   // Если не нашли, то выводим на экран что нет такого сотрудника
                Output.OutputSurname(employee.Name, employee.Surname);
            else                                                         // Иначе смотрим какую Роль нашли                 
            {
                switch (employee.Role)
                {
                    case "руководитель":
                        Leader leader = new Leader(employee);
                        while (true)
                            leader.ActionsOfLeader(Output.OutputMenuForLeader());
                        
                    case "сотрудник":
                        Staff staff = new Staff(employee);
                        while(true)
                            staff.ActionsOfStaff(Output.OutputMenuForStaff());                       

                    case "фрилансер":
                        Freelancer freelancer = new Freelancer(employee);
                        while(true)
                            freelancer.ActionsOfFreelancer(Output.OutputMenuForFreelancer());                        
                }
            }
        }
    }
}
