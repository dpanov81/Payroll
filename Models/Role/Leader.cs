using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Models
{
    public class Leader : Employee
    {
        public delegate void LeaderHandler();

        public event LeaderHandler EventAddEmployeeToFile;
        public event LeaderHandler EventViewReportForAllEmployees;
        public event LeaderHandler EventViewEmployeeReport;
        public event LeaderHandler EventExit;
        public event LeaderHandler EventAddWorkHours;

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
                    EventAddEmployeeToFile();
                    break;

                case 2:
                    EventViewReportForAllEmployees();
                    break;

                case 3:
                    EventViewEmployeeReport();
                    break;

                case 4:
                    EventAddWorkHours();
                    break;

                case 5:
                    EventExit(); 
                    break;             
            }
        }
    }
}
