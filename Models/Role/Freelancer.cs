using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Freelancer : Employee
    {
        public Freelancer(string name, string surname) : base (name, surname)
        {
            Name = name;
            Surname = surname;
        }

        public Freelancer(Employee employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
        }

        public void ActionsOfFreelancer(byte action)
        {
            switch (action)
            {
                case 1:
                    AddWorkHours();
                    break;

                case 2:
                    ViewHoursWorkedAndIncomeForPeriod();
                    break;

                case 3:
                    ExitProgramm();
                    break;
            }
        }

        private void AddWorkHours()
        {
            Console.WriteLine("Добавляю свои отработанные часы");
        }

        private void ViewHoursWorkedAndIncomeForPeriod()
        {
            Console.WriteLine("Смотрю свои отработанные часы и доход за период");
        }

        private void ExitProgramm()
        {
            Console.WriteLine("Выхожу из программы");
        }
    }
}
