using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Leader : Employee
    {
        public Leader(string name, string surname) : base(name, surname)
        {
            Name = name;
            Surname = surname;
        }

        public Leader(Employee employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
        }

        public void ActionsOfLeader(byte action)
        {
            switch (action)
            {
                case 1:
                    AddEmployee();
                    break;

                case 2:
                    ViewReportForAllEmployees();
                    break;

                case 3:
                    ViewEmployeeReport();
                    break;

                case 4:
                    AddWorkHours();
                    break;

                case 5:
                    ExitProgramm();
                    break;                
            }
        }

        private void AddEmployee()
        {
            Console.WriteLine("Добавляю сотрудника");
        }

        private void ViewReportForAllEmployees()
        {
            Console.WriteLine("Смотрю отчет по всем сотрудникам");
        }

        private void ViewEmployeeReport()
        {
            Console.WriteLine("Смотрю отчет по конкретному сотруднику");
        }

        private void AddWorkHours()
        {
            Console.WriteLine("Добавляю рабочие часы");
        }
                
        private void ExitProgramm()
        {
            Console.WriteLine("Выхожу из программы");
        }

    }
}
