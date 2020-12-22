using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollConsole
{
    public static class Input
    {
        public static string InputName()
        {
            string name;

            do
            {
                Output.EnterYourName();
                name = Console.ReadLine();
            }
            while (!InputValidation.ValidationNameOrSurname(name));

            return name;
        }
        public static string InputSurname()
        {
            string surname;

            do
            {
                Output.EnterYourSurname();
                surname = Console.ReadLine();
            }
            while (!InputValidation.ValidationNameOrSurname(surname));

            return surname;
        }

        public static string InputRole()
        {
            string role;

            do
            {
                Output.EnterRole();
                role = Console.ReadLine();
            }
            while (!InputValidation.ValidationRole(role));

            return role;
        }
        public static byte InputMenuLeader()
        {
            string key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                case "4":
                    return 4;

                case "5":
                    return 5;

                default:
                    Console.WriteLine("Введите цифру от 1 до 5");
                    return 6;
            }
        }

        public static byte InputMenuFreelancer()
        {
            string key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                default:
                    Console.WriteLine("Введите цифру от 1 до 3");
                    return 4;
            }
        }

        public static byte InputMenuStaff()
        {
            string key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                default:
                    Console.WriteLine("Введите цифру от 1 до 3");
                    return 4;
            }
        }

        public static string InputDate()
        {
            string strDate = Console.ReadLine();
            DateTime date;

            while (!DateTime.TryParse(strDate, out date))
            {
                Console.WriteLine("формат даты не верный");
                Output.EnterDate();
                strDate = Console.ReadLine();
            }          
            
            strDate = date.ToShortDateString();                       

            return strDate;
        }

        public static byte InputNumberOfHoursWorked()
        {
            string hours = Console.ReadLine();
            byte h;

            if (!byte.TryParse(hours, out h))
            {                
                do
                {
                    Console.WriteLine("ОШИБКА ВВОДА! Введите число:");
                    hours = Console.ReadLine();
                }
                while (!byte.TryParse(hours, out h));
            }           

            return h;   
        }

        public static string InputTask()
        {
            string task = Console.ReadLine();
            return task;
        }
    }
}
