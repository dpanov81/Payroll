using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollConsole
{
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

        static public byte MenuForFreelancer()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить часы работы\n(2). Посмотреть свои отработанные часы и зарплату за период\n(3). Выход из программы");

            byte choice = Input.InputMenuFreelancer();

            while (choice == 4)
                choice = Input.InputMenuFreelancer();

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
            string date = Input.InputDate();
            if (!InputValidation.ValidationDate(date))
            {
                do
                {
                    Console.WriteLine("ОШИБКА ВВОДА: Введеная дата больше текущей!!!");
                    EnterDate();
                    date = Input.InputDate();
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

        public static void EmployeeListOfHoursWorked(List<string> listHoursWorked, Employee employee)
        {
            if (listHoursWorked == null)
                NoDataForThisEmployee();
            else
            {                
                int index = listHoursWorked.Count - 1;          // Индекс последнего элемента в списке, там лежит общее кол-во отработанных часов сотрудника
                int hours = int.Parse(listHoursWorked[index]);      
                listHoursWorked.RemoveAt(index);        // Удаляем из списка последний элемент в котором содержится кол-во отработанных часов.

                    Console.WriteLine($"\nОтчет по сотруднику {employee.Name} {employee.Surname}\nДолжность: {employee.Role}");
                foreach (var str in listHoursWorked)
                    Console.WriteLine(str);

                Console.WriteLine($"Итого: {hours} часов");
            }
        }

        public static void NoDataForThisEmployee()
        {
            Console.WriteLine("По этому сотруднику нет данных.");
        }
    }
}
