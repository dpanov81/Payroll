using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    /// <summary>
    /// Служба отчетности, для формирования отчетов.
    /// </summary>
    public class ReportingService
    {
        // Период за который нужно сформировать отчет: за день = 1, за неделю = 2, за месяц = 3  
        public byte period;
        // Дата начала отчета.
        public DateTime startDate;
        // Общее число отработанных часов.
        public int totalHoursWorked;
        // Список строк для отчета.
        private List<string> _listReport;
        
        public ReportingService(byte period, DateTime date)
        {
            this.period = period;
            startDate = date;
            _listReport = new List<string>();
        }        

        /// <summary>
        /// Создание отчета.
        /// </summary>
        /// <param name="listHoursWorked">Список часов отработанных сотрудником или сотрудниками.</param>
        /// <returns>Отчет за период в списке строк.</returns>
        public List<string>CreateReport(List<string> listHoursWorked, Employee employee)
        {
            if (period == 1)
            {                
                return DailyReport(listHoursWorked);
            }
            else
                return WeeklyOrMonthlyReport(listHoursWorked, employee);
        }

        /// <summary>
        /// Формирование строк списка дневного отчета по конкретному сотруднику.
        /// </summary>        
        /// <returns>Список строк дневного отчета по конкретному сотруднику.</returns>
        private List<string> DailyReport(List<string> listHoursWorked)
        {
            ReportLine reportLine = new ReportLine();

            foreach (var str in listHoursWorked)
            {
                if (startDate == reportLine.GetDateFromString(str))
                {
                    totalHoursWorked += reportLine.GetHoursWorkedFromString(str);
                    _listReport.Add(CreateReportLine(str));
                }
            }            
            return _listReport;
        }

        private List<string> WeeklyOrMonthlyReport(List<string> listHoursWorked, Employee employee)
        {

            return _listReport;
        }

        /// <summary>
        /// Создать строку отчета.
        /// </summary>        
        /// <returns>Строка отчета.</returns>
        private string CreateReportLine(string line)
        {
            ReportLine reportLine = new ReportLine();            

            var dataFormString = reportLine.GetDataFromString(line);
            string strReport = $"{dataFormString.Item1.ToShortDateString()}, {dataFormString.Item4} часов, {dataFormString.Item5}";

            return strReport;
        }
    }
}
