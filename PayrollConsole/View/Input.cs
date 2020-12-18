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
    }
}
