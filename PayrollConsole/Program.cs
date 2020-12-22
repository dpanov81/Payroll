using Models;
using Repository;
using System;
using System.Collections.Generic;

namespace PayrollConsole
{
    class Program
    {        
        static void Main(string[] args)
        {
            List<Employee> listEmployees = new List<Employee>();                            // Лист всех сотрудников        
            Employee employee = new Employee(Input.InputName(), Input.InputSurname());     // При входе в программу вводим имя и фамилию       

            FileIOService file = new FileIOService("Список сотрудников");   // Для загрузки данных из файла, в констр. указываем название файла(файл лежит рядом с exe-шником)
            listEmployees = file.LoadDataToListEmployee();            // Загружаем список всех сотрудников из файла в лист

            if (employee.FindEmployeeInList(listEmployees).Role == null)                // Если не нашли, то выводим на экран что нет такого сотрудника
                Output.SurnameNotOnList(employee.Surname);
            else
            {     
                Output.SurnameAndRole(employee.Surname, employee.Role);                     
            
                switch (employee.Role)
                {
                    case "руководитель":
                        Leader leader = new Leader(employee);                        
                            leader.EventAddEmployeeToFile += Leader_AddEmployeeToFile;
                            leader.EventAddWorkHours += Leader_EventAddWorkHours;
                            leader.EventExit += Leader_EventExit;
                            leader.ActionsOfLeader(Output.MenuForLeader());                        
                        break;
                        
                    case "сотрудник":
                        Staff staff = new Staff(employee);
                        while(true)
                            staff.ActionsOfStaff(Output.MenuForStaff());                       

                    case "фрилансер":
                        Freelancer freelancer = new Freelancer(employee);
                        while(true)
                            freelancer.ActionsOfFreelancer(Output.MenuForFreelancer());                        
                }
            }
        }

        private static void Leader_EventAddWorkHours()
        {
            Console.WriteLine("Добавление часов сотруднику, для этого необходимо ввести имя и фамилию сотрудника");
            Employee employee = new Employee(Input.InputName(), Input.InputSurname());
            List<Employee> listEmployees = new List<Employee>();                            // Лист всех сотрудников  

            FileIOService file = new FileIOService("Список сотрудников");   
            listEmployees = file.LoadDataToListEmployee();

            if (employee.FindEmployeeInList(listEmployees).Role == null)                // Если не нашли, то выводим на экран что нет такого сотрудника
                Output.SurnameNotOnList(employee.Surname);
            else
            {
                FileIOService file1;
                Console.WriteLine("Добавление обработанных часов ");
                switch (employee.Role)
                {

                    case "руководитель":
                        Console.WriteLine("записываем отработанные часы руководителю");
                        //Leader leader = new Leader(employee);
                         
                        file1 = new FileIOService("Список отработанных часов руководителей");
                        file1.AddStringToFile(Output.AddHoursWorked(employee));                        
                        break;

                    case "сотрудник":
                        Console.WriteLine("записываем отработанные часы сотруднику на зарплате");
                        //Staff staff = new Staff(employee);

                        file1 = new FileIOService("Список отработанных часов сотрудников на зарплате");
                        file1.AddStringToFile(Output.AddHoursWorked(employee));
                        break;

                    case "фрилансер":
                        Console.WriteLine("записываем отработанные часы фрилансеру");
                        file1 = new FileIOService("Список отработанных часов внештатных сотрудников");
                        file1.AddStringToFile(Output.AddHoursWorked(employee));
                        break;
                }
            }
        }

        private static void Leader_EventExit()
        {
            Console.WriteLine("Завершаю приложение");
            Environment.Exit(0);
        }

        public static void Leader_AddEmployeeToFile()
        {
            Console.WriteLine("Вызов из Programm.cs");

            Employee employee = new Employee(Input.InputName(), Input.InputSurname(), Input.InputRole());
            string line = employee.Name + "," + employee.Surname + "," + employee.Role;
            FileIOService file = new FileIOService("Список сотрудников");
            file.AddStringToFile(line);
        }        
    }
}
