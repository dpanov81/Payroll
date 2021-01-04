using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollConsole
{
    /// <summary>
    /// Проверка вводных данных.
    /// </summary>
    public static class InputValidation
    {
        public static bool ValidationNameOrSurname(string name)
        {
            if (name.Length > 20)
            {
                Output.AttentionError();
                Console.WriteLine("Имя или фамилия не должна превышать 20 символов.\n");
                return false;
            }

            if (name.Length < 3)
            {
                Output.AttentionError();
                Console.WriteLine("Имя или фамилия не должна состоять из менее 3-х символов.\n");
                return false;
            }

            for (int i = 0; i < name.Length; i++)
            {
                if (Char.IsDigit(name[i]))
                {
                    Output.AttentionError();
                    Console.WriteLine("Имя или фамилия не должна состоять из цифр.\n");
                    return false;
                }
            }

            return true;
        }

        public static bool ValidationRole(string role)
        {
            switch (role)
            {
                case "руководитель":
                    return true;                    

                case "сотрудник":
                    return true;                    

                case "фрилансер":
                    return true;

                default:
                    Console.WriteLine("Пожалуйста введите должность: руководитель, сотрудник или фрилансер");
                    return false;
            }
        }

        public static bool ValidationDate(string strDate)
        {
            DateTime dateToday = DateTime.Today;
            DateTime date = Convert.ToDateTime(strDate);
                        
            if (dateToday.CompareTo(date) >= 0)
                return true;
            else
                return false;
        }
    }
}
