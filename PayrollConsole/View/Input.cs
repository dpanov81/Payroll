using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollConsole
{
    /// <summary>
    /// Обслуживающий класс.
    /// Ввод информации с консоли.
    /// </summary>
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

        /// <summary>
        /// Ввод даты.
        /// </summary>
        /// <returns>дата в string</returns>
        public static string EnterDate()
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

        /// <summary>
        /// Ввод даты для начала отчета.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime EnterDateForReport()
        {
            Console.WriteLine("Введите дату начала отчета в формате дд.мм.гггг:");

            string strDate = Console.ReadLine();
            DateTime startDate;

            while (!DateTime.TryParse(strDate, out startDate))
            {
                Console.WriteLine("формат даты не верный");
                Console.WriteLine("Введите дату начала отчета в формате дд.мм.гггг:");
                strDate = Console.ReadLine();
            }            

            return startDate;
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

        /// <summary>
        /// Ввод периода отчета.
        /// </summary>
        /// <returns></returns>
        public static byte EnterReportForPeriod()
        {
            string period = Console.ReadLine();

            switch (period)
            {
                case "Д":
                    return 1;

                case "Н":
                    return 2;

                case "М":
                    return 3;

                default:                    
                    return 4;
            }
        }
    }
}
