using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// Класс для создания рандомной даты, эта дата используется для заполнения файлов рандомной информацией.
    /// </summary>
    public class RandomDate
    {
        // Начальная дата.
        DateTime startDate;
        // Диапазон между датами.
        int range;
        Random rnd;       

        public RandomDate()
        {
            // Интервал времени равный 90 дней.
            TimeSpan timeSpan = new TimeSpan(90, 0, 0, 0);
           
            // Начальная дата = Текущая дата - интервал времени(90 дней).
            startDate = DateTime.Now.Subtract(timeSpan);
            rnd = new Random();

            // Диапазон между датами = текущая дата - начальная дата.
            range = (DateTime.Today - startDate).Days;
        }

        public DateTime Next()
        {
            return startDate.AddDays(rnd.Next(range)).AddHours(0).AddMinutes(0).AddSeconds(0);
        }
    }
}
