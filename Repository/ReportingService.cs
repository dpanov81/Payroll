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
        // Дата конца отчета.
        public DateTime endDate;
        // Общее число отработанных часов.
        public int totalHoursWorked;
        // Общее число отработанных дней.
        public int totalNumberOfDaysWorked;

        private List<string> _listReport;
        
        public ReportingService(byte p, DateTime date)
        {
            period = p;
            startDate = date;
            _listReport = new List<string>();
        }        

        /// <summary>
        /// Создание списка строк для отчета.
        /// </summary>
        /// <param name="listHoursWorked">Список часов отработанных сотрудником или сотрудниками.</param>
        /// <returns>Список строк для отчета.</returns>
        public List<string>CreateReport(List<string> listHoursWorked)
        {
            if (period == 1)
            {
                DailyReport(listHoursWorked);
                return _listReport;
            }
            else
            {
                WeeklyOrMonthlyReport(listHoursWorked);
                return _listReport;
            }                
        }

        /// <summary>
        /// Формирование строк списка отчета за день по конкретному сотруднику.
        /// </summary>                
        private void DailyReport(List<string> listHoursWorked)
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
        }

        /// <summary>
        /// Формирование строк списка отчета за неделю или месяц по конкретному сотруднику.
        /// </summary>                
        private void WeeklyOrMonthlyReport(List<string> listHoursWorked)
        {
            switch (period)
            {
                // Формируем список строк для отчета за неделю.
                case 2:
                    endDate = startDate.AddDays(6);
                    SetLinesForPeriod(listHoursWorked);                    
                    break;

                // Формируем список строк для отчета за месяц.
                case 3:
                    endDate = startDate.AddMonths(1);
                    SetLinesForPeriod(listHoursWorked);
                    break;
            } 
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

        /// <summary>
        /// Установить строки за период в переменную _listReport.
        /// </summary>        
        private void SetLinesForPeriod(List<string> listHoursWorked)
        {
            // Считываемая дата.
            DateTime readDate;
            ReportLine reportLine = new ReportLine();

            foreach (var line in listHoursWorked)
            {
                readDate = reportLine.GetDateFromString(line);

                if (readDate >= startDate && readDate <= endDate)
                {
                    totalHoursWorked += reportLine.GetHoursWorkedFromString(line);
                    _listReport.Add(CreateReportLine(line));
                }
            }
        }
    }
}
