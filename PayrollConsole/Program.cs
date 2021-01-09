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

            while (true)
            {
                Output.Greeting();

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
                            staff.EventAddWorkHours += Staff_EventAddWorkHours;
                            staff.EventViewReport += Staff_EventViewReport;
                            staff.EventExit += Staff_EventExit;
                            staff.ActionsOfStaff(Output.MenuForStaff());
                            break;

                        case "фрилансер":
                            Freelancer freelancer = new Freelancer(employee);
                            freelancer.EventAddWorkHours += Freelancer_EventAddWorkHours;
                            freelancer.EventViewReport += Freelancer_EventViewReport;
                            freelancer.EventExit += Freelancer_EventExit;
                            freelancer.ActionsOfFreelancer(Output.MenuForFreelancer());
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Добавить отработанные часы, вызов метода происходит из объекта класса Freelancer.
        /// </summary>        
        private static void Freelancer_EventAddWorkHours(Freelancer freelancer)
        {
            List<string> listHoursWorked = new List<string>();
            SortingService sortSrv = new SortingService();

            FileIOService file = new FileIOService("Список отработанных часов внештатных сотрудников");

            file.AddStringToFile(Output.AddHoursWorked((Employee)freelancer));

            listHoursWorked = file.LoadListOfHoursWorked();

            if (sortSrv.NeedSorting(listHoursWorked))
            {
                listHoursWorked = sortSrv.SortingList(listHoursWorked);
            }

            file.OverwriteListOfHoursWorkedToFile(sortSrv.FindDuplicateLines(listHoursWorked));
        }

        /// <summary>
        /// Посмотреть отчет за период, вызов метода происходит из объекта класса Freelancer.
        /// </summary>
        private static void Freelancer_EventViewReport(Freelancer freelancer)
        {
            FileIOService file = new FileIOService("Список отработанных часов внештатных сотрудников");
            List<string> listReport = new List<string>();
            ReportingService repService = new ReportingService(Output.ReportForPeriod(), Input.EnterDateForReport());
            Employee employee = new Employee(freelancer.Name, freelancer.Surname, "фрилансер");

            listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee((Employee)freelancer));

            Output.EmployeeListOfHoursWorked(listReport, employee, repService);
        }        

        /// <summary>
        /// Добавить отработанные часы, вызов метода происходит из объекта класса Staff.
        /// </summary>
        private static void Staff_EventAddWorkHours(Staff staff)
        {
            List<string> listHoursWorked = new List<string>();
            SortingService sortSrv = new SortingService();

            FileIOService file = new FileIOService("Список отработанных часов сотрудников на зарплате");   
            
            file.AddStringToFile(Output.AddHoursWorked((Employee)staff));

            listHoursWorked = file.LoadListOfHoursWorked();

            if (sortSrv.NeedSorting(listHoursWorked))
            {
                listHoursWorked = sortSrv.SortingList(listHoursWorked);                
            }               

            file.OverwriteListOfHoursWorkedToFile(sortSrv.FindDuplicateLines(listHoursWorked));
        }

        /// <summary>
        /// Посмотреть отчет за период, вызов метода происходит из объекта класса Staff.
        /// </summary>
        private static void Staff_EventViewReport(Staff staff)
        {            
            FileIOService file = new FileIOService("Список отработанных часов сотрудников на зарплате");
            List<string> listReport = new List<string>();
            ReportingService repService = new ReportingService(Output.ReportForPeriod(), Input.EnterDateForReport());
            Employee employee = new Employee(staff.Name, staff.Surname, "сотрудник");

            listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee((Employee)staff));

            Output.EmployeeListOfHoursWorked(listReport, employee, repService);            
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
            List<string> listReport = new List<string>();
            ReportingService repService = new ReportingService(Output.ReportForPeriod(), Input.EnterDateForReport());

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "руководитель")
                {
                    file = new FileIOService("Список отработанных часов руководителей");
                    listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                    Output.EmployeeListOfHoursWorked(listReport, employee, repService);
                }
                listReport.Clear();
            }

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "сотрудник")
                {
                    file = new FileIOService("Список отработанных часов сотрудников на зарплате");
                    listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                    Output.EmployeeListOfHoursWorked(listReport, employee, repService);
                }
                listReport.Clear();
            }

            foreach (var employee in listEmployees)
            {
                if (employee.Role == "фрилансер")
                {
                    file = new FileIOService("Список отработанных часов внештатных сотрудников");
                    listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                    Output.EmployeeListOfHoursWorked(listReport, employee, repService);
                }
                listReport.Clear();
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
                ReportingService repService = new ReportingService(Output.ReportForPeriod(), Input.EnterDateForReport());
                List<string> listReport = new List<string>();

                switch (employee.Role)
                {
                    case "руководитель":
                        file = new FileIOService("Список отработанных часов руководителей");
                        listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                        Output.EmployeeListOfHoursWorked(listReport, employee, repService);
                        break;

                    case "сотрудник":
                        file = new FileIOService("Список отработанных часов сотрудников на зарплате");
                        listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                        Output.EmployeeListOfHoursWorked(listReport, employee, repService);
                        break;

                    case "фрилансер":
                        file = new FileIOService("Список отработанных часов внештатных сотрудников");
                        listReport = repService.CreateReport(file.LoadListOfWorkingHoursForSpecificEmployee(employee));
                        Output.EmployeeListOfHoursWorked(listReport, employee, repService);
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
                            listHoursWorked = sortSrv.SortingList(listHoursWorked);
                        }
                        file.OverwriteListOfHoursWorkedToFile(sortSrv.FindDuplicateLines(listHoursWorked));
                        break;

                    case "сотрудник":
                        Console.WriteLine("Добавляем отработанные часы сотруднику на зарплате");

                        file = new FileIOService("Список отработанных часов сотрудников на зарплате");
                        file.AddStringToFile(Output.AddHoursWorked(employee));

                        listHoursWorked = file.LoadListOfHoursWorked();

                        if (sortSrv.NeedSorting(listHoursWorked))
                        {
                            listHoursWorked = sortSrv.SortingList(listHoursWorked);
                        }
                        file.OverwriteListOfHoursWorkedToFile(sortSrv.FindDuplicateLines(listHoursWorked));
                        break;

                    case "фрилансер":
                        Console.WriteLine("Добавляем отработанные часы фрилансеру");

                        file = new FileIOService("Список отработанных часов внештатных сотрудников");
                        file.AddStringToFile(Output.AddHoursWorked(employee));

                        listHoursWorked = file.LoadListOfHoursWorked();

                        if (sortSrv.NeedSorting(listHoursWorked))
                        {
                            listHoursWorked = sortSrv.SortingList(listHoursWorked);
                        }
                        file.OverwriteListOfHoursWorkedToFile(sortSrv.FindDuplicateLines(listHoursWorked));
                        break;
                }
            }
        }        

        private static void Leader_Exit()
        {
            Console.WriteLine("Приложение завершено.");
            Environment.Exit(0);
        }

        private static void Staff_EventExit(Staff staff)
        {
            Leader_Exit();
        }

        private static void Freelancer_EventExit(Freelancer freelancer)
        {
            Leader_Exit();
        }
    }
}
