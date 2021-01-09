using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Freelancer : Employee
    {
        public delegate void FreelancerHandler(Freelancer freelancer);

        public event FreelancerHandler EventAddWorkHours;
        public event FreelancerHandler EventViewReport;
        public event FreelancerHandler EventExit;
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
