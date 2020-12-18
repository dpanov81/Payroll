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

        static public void OutputSurname(string name, string surname)
        {
            Console.WriteLine("\nЗдравствуйте, " + surname + "!");
            Console.WriteLine("Вас нет в списке сотрудников.");
        }

        static public void OutputSurnameAndRole(string name, string surname, string role)
        {
            Console.WriteLine("\nЗдравствуйте, " + surname + "!");
            Console.WriteLine("Ваша роль: " + role);
        }

        static public byte OutputMenuForLeader()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить сотрудника\n(2). Посмотреть отчет по всем сотрудникам\n(3). Посмотреть отчет по конкретному сотруднику\n(4). Добавить часы работы\n(5). Выход из программы");

            byte choice = Input.InputMenuLeader();

            while (choice == 6)
                choice = Input.InputMenuLeader();

            return choice;
        }

        static public byte OutputMenuForFreelancer()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить часы работы\n(2). Посмотреть свои отработанные часы и зарплату за период\n(3). Выход из программы");

            byte choice = Input.InputMenuFreelancer();

            while (choice == 4)
                choice = Input.InputMenuFreelancer();

            return choice;
        }

        static public byte OutputMenuForStaff()
        {
            Console.WriteLine("\nВыберите желаемое действие:\n(1). Добавить часы работы\n(2). Посмотреть свои отработанные часы и зарплату за период\n(3). Выход из программы");

            byte choice = Input.InputMenuStaff();

            while (choice == 4)
                choice = Input.InputMenuStaff();

            return choice;
        }
    }
}
