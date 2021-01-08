using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollConsole
{
    /// <summary>
    /// Обслуживающий класс.
    /// Вывод информации на консоль.
    /// </summary>
    public static class Output
    {
        static public void AttentionError()
        {
            Console.WriteLine("\nВнимание!!! Ошибка Ввода!!!");
        }

        static public void EnterYourName()
        {
            Console.WriteLine("Пожалуйста введите имя:");
        }

        static public void EnterYourSurname()
        {
            Console.WriteLine("Пожалуйста введите фамилию:");
        }

        static public void EnterRole()
        {
            Console.WriteLine("Пожалуйста введите должность:");
        }

        static public void SurnameNotOnList(string surname)
        {
            Console.WriteLine("\nСотрудника с фамилией " + surname + " нет в списке сотрудников.");
        }

        static public void SurnameAndRole(string surname, string role)
        {
            Console.WriteLine("\nЗдравствуйте, " + surname + "!");
            Console.WriteLine("Ваша роль: " + role);
        }

        static public byte MenuForLeader()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить сотрудника\n(2). Посмотреть отчет по всем сотрудникам\n(3). Посмотреть отчет по конкретному сотруднику\n(4). Добавить часы работы\n(5). Выход из программы");

            byte choice = Input.InputMenuLeader();

            while (choice == 6)
                choice = Input.InputMenuLeader();

            return choice;
        }

        static public byte MenuForStaff()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить часы работы\n(2). Посмотреть свои отработанные часы и зарплату за период\n(3). Выход из программы");

            byte choice = Input.InputMenuStaff();

            while (choice == 4)
                choice = Input.InputMenuStaff();

            return choice;
        }

        static public byte MenuForFreelancer()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить часы работы\n(2). Посмотреть свои отработанные часы и зарплату за период\n(3). Выход из программы");

            byte choice = Input.InputMenuFreelancer();

            while (choice == 4)
                choice = Input.InputMenuFreelancer();

            return choice;
        }        

        static public Employee AddEmployee()
        {
            Console.WriteLine("Добавление сотрудника:");
            Employee employee = new Employee(Input.InputName(), Input.InputSurname(), Input.InputRole());

            return employee;
        }

        static public void EnterDate()
        {
            Console.WriteLine("Введите дату в формате дд.мм.гггг:");
        }

        static public string AddHoursWorked(Employee employee)
        {
            string line;

            EnterDate();
            string date = Input.EnterDate();
            if (!InputValidation.ValidationDate(date))
            {
                do
                {
                    Console.WriteLine("ОШИБКА ВВОДА: Введеная дата больше текущей!!!");
                    EnterDate();
                    date = Input.EnterDate();
                }
                while (!InputValidation.ValidationDate(date));
            }

            line = date + "," + employee.Name + " " + employee.Surname + ",";

            byte hours;
            do
            {
                Console.WriteLine("Введите количество отработанных часов от 1 до 24:");
                hours = Input.InputNumberOfHoursWorked();
            }
            while (hours > 24 && hours <= 0);

            string strHours = Convert.ToString(hours);
            line += strHours + ",";

            Console.WriteLine("Опишите задачу над которой работал сотрудник за указанное время:");

            string task = Input.InputTask();

            line += task;

            return line;
        }

        /// <summary>
        /// Формирование и вывод на консоль отчета по конкретному сотруднику за период.
        /// </summary>        
        public static void EmployeeListOfHoursWorked(List<string> listReport, Employee employee, ReportingService repServ)
        {
            if (listReport.Count == 0)
            {
                NoDataForThisEmployee(employee);
            }
            else
            {
                switch (repServ.period)
                {
                    // Отчет за день.
                    case 1:
                        Console.WriteLine($"\n\nОтчёт по сотруднику: {employee.Name} {employee.Surname}({employee.Role}) \nза день {repServ.startDate.ToShortDateString()}\n");
                        foreach (var str in listReport)
                            Console.WriteLine(str);

                        switch (employee.Role)
                        {
                            case "руководитель":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {10000} руб.");
                                break;
                            case "сотрудник":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {6000} руб.");
                                break;
                            case "фрилансер":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {1000 * repServ.totalHoursWorked} руб.");
                                break;
                        }   
                        break;

                    // Отчет за неделю.
                    case 2:
                        Console.WriteLine($"\n\nОтчёт по сотруднику: {employee.Name} {employee.Surname}({employee.Role}) \nза неделю с {repServ.startDate.ToShortDateString()} по {repServ.endDate.ToShortDateString()}\n");
                        foreach (var str in listReport)
                            Console.WriteLine(str);

                        switch (employee.Role)
                        {
                            case "руководитель":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {listReport.Count * 10000} руб.");
                                break;
                            case "сотрудник":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {listReport.Count * 6000} руб.");
                                break;
                            case "фрилансер":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {repServ.totalHoursWorked * 1000} руб.");
                                break;
                        }                                            
                        break;

                    // Отчет за месяц.
                    case 3:
                        Console.WriteLine($"\n\nОтчёт по сотруднику: {employee.Name} {employee.Surname}({employee.Role}) \nза месяц с {repServ.startDate.ToShortDateString()} по {repServ.endDate.ToShortDateString()}\n");
                        foreach (var str in listReport)
                            Console.WriteLine(str);

                        switch (employee.Role)
                        {
                            case "руководитель":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {listReport.Count * 10000} руб.");
                                break;
                            case "сотрудник":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {listReport.Count * 6000} руб.");
                                break;
                            case "фрилансер":
                                Console.WriteLine($"Итого: {repServ.totalHoursWorked} часов, заработанно: {repServ.totalHoursWorked * 1000} руб.");
                                break;
                        }                       
                        break;                    
                }                
            }
        }

        public static void NoDataForThisEmployee(Employee employee)
        {
            Console.WriteLine($"\nОтчет по сотруднику {employee.Name} {employee.Surname}({employee.Role})");
            Console.WriteLine("По этому сотруднику нет данных за указанный период.");
        }

        /// <summary>
        /// Отчет за период.
        /// </summary>
        /// <returns>Byte Период за который необходимо сделать отчет. День, Неделя, Месяц.</returns>
        public static byte ReportForPeriod()
        {            
            byte period;

            do
            {
                Console.WriteLine("Введите период за который вы хотите увидеть отчет:\nДень - (Д)\nHеделя - (Н)\nМесяц - (М)");
                period = Input.EnterReportForPeriod();
            }
            while (period == 4);

            return period;
        }
    }
}
