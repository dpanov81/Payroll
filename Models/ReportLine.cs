﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// Класс для работы с данными из строки отчета.    
    /// </summary>
    public class ReportLine
    {
        private DateTime _date;
        private string _name;
        private string _surname;
        private byte _workingHours;
        private string _task;

        /// <summary>
        /// Получить данные из строки.
        /// </summary>        
        /// <returns>Данные из строки ввиде кортежа.</returns>
        public (DateTime, string, string, byte, string) GetDataFromString(string line)
        {        
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');
                _date = Convert.ToDateTime(stringArray[0]);

                if (!String.IsNullOrWhiteSpace(stringArray[1]))
                {
                    // Делим вторую строку в массиве по пробелу и записываем в новый массив, теперь там 2 элемента (Имя и Фамилия)
                    string[] stringArrayNameAndSurname = stringArray[1].Split(' ');

                    _name = stringArrayNameAndSurname[0];
                    _surname = stringArrayNameAndSurname[1];    
                }                

                if (!String.IsNullOrWhiteSpace(stringArray[2]))
                    _workingHours = Convert.ToByte(stringArray[2]);

                if (!String.IsNullOrWhiteSpace(stringArray[3]))
                    _task = stringArray[3];                
            }            

            return (_date, _name, _surname, _workingHours, _task);
        }

        /// <summary>
        /// Получить имя и фамилию из строки.
        /// </summary>        
        /// <returns>Данные из строки ввиде кортежа.</returns>
        public (string, string) GetNameAndSurnameFromString(string line)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');                

                if (!String.IsNullOrWhiteSpace(stringArray[1]))
                {
                    // Делим вторую строку в массиве по пробелу и записываем в новый массив, теперь там 2 элемента (Имя и Фамилия)
                    string[] stringArrayNameAndSurname = stringArray[1].Split(' ');

                    _name = stringArrayNameAndSurname[0];
                    _surname = stringArrayNameAndSurname[1];
                }
            }
            return (_name, _surname);
        }

        /// <summary>
        /// Получить кол-во отработанных часов и задачу из строки.
        /// </summary>        
        /// <returns>Данные из строки ввиде кортежа.</returns>
        public (byte, string) GetHoursAndTaskFromString(string line)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');                

                if (!String.IsNullOrWhiteSpace(stringArray[2]))
                    _workingHours = Convert.ToByte(stringArray[2]);

                if (!String.IsNullOrWhiteSpace(stringArray[3]))
                    _task = stringArray[3];
            }

            return (_workingHours, _task);
        }

        /// <summary>
        /// Получить дату из строки
        /// </summary>        
        /// <returns>Дата.</returns>
        public DateTime GetDateFromString(string line)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');
                _date = Convert.ToDateTime(stringArray[0]);
            }
            return _date;
        }
        
        /// <summary>
        /// Получить отработанные часы из строки.
        /// </summary>        
        /// <returns>Отработанные часы.</returns>
        public byte GetHoursWorkedFromString(string line)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');
                _workingHours = Convert.ToByte(stringArray[2]);
            }
            return _workingHours;
        }        

        /// <summary>
        /// Сравнить имя и фамилию из строки с именем и фамилией из объекта класс Employee.
        /// </summary>        
        /// <returns>True если равны, false если не равны.</returns>
        public bool CompareNameAndSurname(string line, Employee employee)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                string[] stringArray = line.Split(',');

                if (!String.IsNullOrWhiteSpace(stringArray[1]))
                {
                    // Делим вторую строку в массиве по пробелу и записываем в новый массив, теперь там 2 элемента (Имя и Фамилия)
                    string[] stringArrayNameAndSurname = stringArray[1].Split(' ');
                                        
                    if (stringArrayNameAndSurname[0] == employee.Name && stringArrayNameAndSurname[1] == employee.Surname)
                        return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Создает строку данных для записи в файл.
        /// </summary>        
        /// <returns>Строка данных.</returns>
        public string CreateReportLine(DateTime date, string name, string surname, byte workingHours, string task)
        {
            string line = $"{date.ToShortDateString()},{name} {surname},{workingHours},{task}";

            return line;
        }
    }
}
