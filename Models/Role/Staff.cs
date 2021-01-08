using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Staff : Employee
    {
        public delegate void StaffHandler(Staff staff);        

        public event StaffHandler EventAddWorkHours;
        public event StaffHandler EventViewReport;
        public event StaffHandler EventExit;  

        public Staff(string name, string surname) : base(name, surname)
        {
            Name = name;
            Surname = surname;
        }

        public Staff(Employee employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
        }

        public void ActionsOfStaff(byte action)
        {
            switch (action)
            {
                case 1:
                    EventAddWorkHours(this);
                    break;

                case 2:
                    EventViewReport(this);
                    break;

                case 3:
                    EventExit(this);
                    break;
            }                
        }        
    }
}
