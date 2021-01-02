using Models;
using Services;
using System;
using System.Collections.Generic;

namespace PayrollConsole
{
    class Program
    {        
        static void Main(string[] args)
        {            
            Employee employee = new Employee();     

            employee = FindEmployeeInFile();
         
            if (employee.Role != null)                
            {     
                Output.SurnameAndRole(employee.Surname, employee.Role);                     
            
                switch (employee.Role)
                {
                    case "руководитель":
                        Leader leader = new Leader(employee);                        
                            leader.EventAddEmployeeToFile += Leader_AddEmployeeToFile;
                            leader.EventViewReportForAllEmployees += Leader_ViewReportForAllEmployees;
                            leader.EventViewEmployeeReport += Leader_ViewEmployeeReport;
                            leader.EventAddWorkHours += Leader_AddWorkHours;
                            leader.EventExit += Leader_Exit;
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

        /// <summary>
        /// Найти сотрудника в файле "Список сотрудников"
        /// </summary>
        /// <returns>объект класса Employee</returns>
        public static Employee FindEmployeeInFile()
        {
            Employee employee = new Employee(Input.InputName(), Input.InputSurname());     
                        
            List<Employee> listEmployees = new List<Employee>();
            listEmployees = LoadListOfEmployeesFromFile();

            if (employee.FindEmployeeInList(listEmployees).Role == null)
            {
                Output.SurnameNotOnList(employee.Surname);
                return employee;
            }
            else
                return employee;
        }
        /// <summary>
        /// Загрузить список сотрудников из файла.
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public static List<Employee> LoadListOfEmployeesFromFile()
        {
            FileIOService file = new FileIOService("Список сотрудников");
            List<Employee> listEmployees = new List<Employee>();
            listEmployees = file.LoadDataToListEmployee();

            return listEmployees;
        }

        /// <summary>
        /// Добавить сотрудника в файл "список сотрудников", вызов метода происходит из объекта класса Leader
        /// </summary>
        public static void Leader_AddEmployeeToFile()
        {            
            Employee employee = new Employee(Input.InputName(), Input.InputSurname(), Input.InputRole());
            string line = employee.Name + "," + employee.Surname + "," + employee.Role;
            FileIOService file = new FileIOService("Список сотрудников");
            file.AddStringToFile(line);
        }

        /// <summary>
        /// Посмотреть отчет по всем сотрудникам, вызов метода происходит из объекта класса Leader
        /// </summary>
        private static void Leader_ViewReportForAllEmployees()
        {
            List<Employee> listEmployees = new List<Employee>();
            listEmployees = LoadListOfEmployeesFromFile();

            FileIOService file;
            List<string> listHoursWorked = new List<string>(); ; 

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "руководитель")
                {
                    file = new FileIOService("Список отработанных часов руководителей");                    
                    listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);
                    Output.EmployeeListOfHoursWorked(listHoursWorked, employee);
                }
            }

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "сотрудник")
                {
                    file = new FileIOService("Список отработанных часов сотрудников на зарплате");                    
                    listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);
                    Output.EmployeeListOfHoursWorked(listHoursWorked, employee);
                }
            }

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "фрилансер")
                {
                    file = new FileIOService("Список отработанных часов внештатных сотрудников");                    
                    listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);                    
                    Output.EmployeeListOfHoursWorked(listHoursWorked, employee);
                }
            }
        }

        /// <summary>
        /// Посмотреть отчет по конкретному сотруднику, вызов метода происходит из объекта класса Leader
        /// </summary>
        private static void Leader_ViewEmployeeReport()
        {
            Employee employee = new Employee();
            employee = FindEmployeeInFile();

            if (employee != null)
            {
                FileIOService file;
                List<string> listHoursWorked = new List<string>(); 
                switch (employee.Role)
                {
                    case "руководитель":
                        file = new FileIOService("Список отработанных часов руководителей");
                        listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);
                        Output.EmployeeListOfHoursWorked(listHoursWorked, employee);                        
                        break;

                    case "сотрудник":
                        file = new FileIOService("Список отработанных часов сотрудников на зарплате");
                        listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);
                        Output.EmployeeListOfHoursWorked(listHoursWorked, employee);
                        break;

                    case "фрилансер":
                        file = new FileIOService("Список отработанных часов внештатных сотрудников");
                        listHoursWorked = file.LoadListOfWorkingHoursForSpecificEmployee(employee);
                        Output.EmployeeListOfHoursWorked(listHoursWorked, employee);
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление рабочих часов сотруднику, вызов метода происходит из объекта класса Leader
        /// </summary>
        private static void Leader_AddWorkHours()
        {
            Console.WriteLine("Добавление часов сотруднику, для этого необходимо ввести имя и фамилию сотрудника");
            Employee employee = new Employee();
            employee = FindEmployeeInFile();

            List<string> listHoursWorked = new List<string>();
            SortingService sortSrv = new SortingService();

            if (employee != null)            
            {
                FileIOService file;                
                switch (employee.Role)
                {
                    case "руководитель":
                        Console.WriteLine("Добавляем отработанные часы руководителю");                        
                         
                        file = new FileIOService("Список отработанных часов руководителей");
                        file.AddStringToFile(Output.AddHoursWorked(employee));
                        
                        listHoursWorked = file.LoadListOfHoursWorked();

                        if (sortSrv.NeedSorting(listHoursWorked))
                        {                            
                            file.OverwriteListOfHoursWorkedToFile(sortSrv.SortingList(listHoursWorked));
                        }
                        break;

                    case "сотрудник":
                        Console.WriteLine("Добавляем отработанные часы сотруднику на зарплате");                        

                        file = new FileIOService("Список отработанных часов сотрудников на зарплате");
                        file.AddStringToFile(Output.AddHoursWorked(employee));

                        listHoursWorked = file.LoadListOfHoursWorked();

                        if (sortSrv.NeedSorting(listHoursWorked))
                        {
                            file.OverwriteListOfHoursWorkedToFile(sortSrv.SortingList(listHoursWorked));
                        }
                        break;

                    case "фрилансер":
                        Console.WriteLine("Добавляем отработанные часы фрилансеру");

                        file = new FileIOService("Список отработанных часов внештатных сотрудников");
                        file.AddStringToFile(Output.AddHoursWorked(employee));

                        listHoursWorked = file.LoadListOfHoursWorked();

                        if (sortSrv.NeedSorting(listHoursWorked))
                        {
                            file.OverwriteListOfHoursWorkedToFile(sortSrv.SortingList(listHoursWorked));
                        }
                        break;
                }
            }           
        }

        private static void Leader_Exit()
        {            
            Console.WriteLine("Приложение завершено.");
            Environment.Exit(0);
        }        
    }
}
